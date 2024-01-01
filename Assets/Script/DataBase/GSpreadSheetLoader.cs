using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GSpreadSheetLoader : MonoBehaviour
{
    // 
    public readonly string origin_key = "https://docs.google.com/spreadsheets/d/13y3xJdLwsUohL-XTO-2nYJTsXzRg0PQglyOjY2UEQfQ/edit?usp=sharing";
    public readonly string gid = "2011285628";
    public readonly string range = "C3:O";
    private string key;
    private string rawData;



    private static Dictionary<string, string[]> data;
    [SerializeField] private List<string[]> ch;
    void Awake()
    {
        data = new Dictionary<string, string[]>();
        key = origin_key.Split("edit")[0];
        
    }

    public void StartDownload()
    {
        StartCoroutine(DownloadData(key,range,gid));
    }
    
    /// <summary>
    /// 구글드라이브에서 데이터를 받아옴
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    IEnumerator DownloadData(string address, string range, string gid)
    {
        string url = $"{address}export?format=tsv&range={range}&gid={gid}";

        yield return new WaitForSeconds(4);
        
        //Debug.Log("[GSpreadSheetLoader::UpdateData] url : "+url);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
                rawData = www.downloadHandler.text;
            else
                rawData = "";
        }
        UpdateData();
    }

    /// <summary>
    /// rawdata를 dict에 정리
    /// </summary>
    void UpdateData()
    {
        string[] rows = rawData.Split('\n');
        for(int i = 0; i < rows.Length; i++)
        {
            var cols = Array.ConvertAll(rows[i].Split('\t'),(str) => (str.Replace("\"","")));
            if (!cols.Any())
                continue;
            
            int length = int.Parse(cols[1]);
            string[] arr = new string[length];
            // StringBuilder sb = new StringBuilder();
            // foreach (var s in cols)
            // {
            //     sb.Append(s).Append(", ");
            // }
            // Debug.Log(sb.ToString());
            Array.Copy(cols,2,arr,0,length);
            data[cols[0]]=arr;
        }
        DB.Instance.SetData(data);
        DB.Instance.OnDBUpdateEvent.Invoke();
        Debug.Log("[GSpreadSheetLoader] Load & Update Data Complete!!!");
    }

}
