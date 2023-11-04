using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GSpreadSheetLoader : MonoBehaviour
{
    // https://docs.google.com/spreadsheets/d/13y3xJdLwsUohL-XTO-2nYJTsXzRg0PQglyOjY2UEQfQ/edit?usp=sharing
    public string key;
    public string range;
    private string rawData;
    private string rawRange;
    private static Dictionary<string, string[]> data;
    void Awake()
    {
        data = new Dictionary<string, string[]>();
        key = key.Split("edit")[0];
        key += "gviz/tq?tqx=out:csv&sheet=DB";
        StartCoroutine(DownloadData("A1:B1",false));
    }
    
    /// <summary>
    /// 구글드라이브에서 데이터를 받아옴
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    IEnumerator DownloadData(string range, bool isData)
    {
        string url = key + "&range=" + range;
        
        Debug.Log("[GSpreadSheetLoader::UpdateData] url : "+url);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
                rawData = www.downloadHandler.text;
            else
                rawData = "";
        }

        if (!isData)
        {
            rawRange = rawData.Split(',')[1];
            StartCoroutine(DownloadData("B3:I"+rawRange.Replace("\"",""),true));
        }
        else
        {
            UpdateData();
        }
    }

    /// <summary>
    /// rawdata를 dict에 정리
    /// </summary>
    void UpdateData()
    {
        string[] rows = rawData.Split('\n');
        for(int i = 0; i < rows.Length; i++)
        {
            var cols = Array.ConvertAll(rows[i].Split(','),(str) => (str.Replace("\"","")));
            if (cols.Count() == 0)
                continue;
            Debug.Log(cols[0]);
            int length = int.Parse(cols[1]);
            string[] arr = new string[length];
            Array.Copy(cols,2,arr,0,length);
            data.Add(cols[0],arr);
        }
        Debug.Log("[GSpreadSheetLoader] Load & Update Data Complete!!!");
    }

    public static bool IsReady()
    {
        return Contains("PlayerSpeed");
    }
    public static bool Contains(string key)
    {
        return data.ContainsKey(key);
    }

    public static string[] Get(string key)
    {
        return data[key];
    }

    public static int GetSize(string key)
    {
        return data[key].Length;
    }
    
    public static string GetOne(string key, int idx = 0)
    {
        return data[key][idx];
    }
}
