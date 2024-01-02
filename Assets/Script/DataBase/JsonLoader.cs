using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Script.Global
{
    [Serializable]
    public class DictionaryPair<U, V>
    {
        public U key;
        public V value;
    }
    
    [Serializable]
    public class DictionaryPairList<U,V>
    {
        public List<DictionaryPair<U,V>> data;
    }
    public class JsonLoader : MonoBehaviour
    {
        private string path;

        private void Awake()
        {
            path = Path.Combine(Application.persistentDataPath, "database");
        }

        public void SaveJson()
        {
            DictionaryPairList<string,string[]> saveData = new DictionaryPairList<string, string[]>();
            saveData.data = new List<DictionaryPair<string, string[]>>();
            foreach (var keyValuePair in  DB.Instance.Data)
            {
                var pair = new DictionaryPair<string, string[]>();
                pair.key = keyValuePair.Key;
                pair.value = keyValuePair.Value;
                saveData.data.Add(pair);
            }
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path,json);
        }

        public void LoadJson()
        {
            DictionaryPairList<string,string[]> saveData = new DictionaryPairList<string, string[]>();
            if (!File.Exists(path))
            {
                //데이터 없을경우
                DB.Instance.SetData("DB_VERSION",null);
                Debug.Log($"No File :{path}");
            }
            else
            {
                string rawJson = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<DictionaryPairList<string,string[]>>(rawJson);
                foreach (var pair in saveData.data)
                {
                    DB.Instance.SetData(pair.key,pair.value);
                }
                DB.Instance.SetData("DB_VERSION",new []{string.Join("_", DB.DB_VERSION) + " (Saved Data)"});
                // Debug.Log(DB.DB_VERSION_TEXT);
                DB.Instance.OnDBUpdateEvent.Invoke();
            }
        }
    }
}