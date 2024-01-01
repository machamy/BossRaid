using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Script.Global
{
    [System.Serializable]
    public class SaveData
    {
        public Dictionary<string, string[]> DBData = new Dictionary<string, string[]>();
    }
    public class JsonLoader : MonoBehaviour
    {
        private string path;

        private void Start()
        {
            path = Path.Combine(Application.persistentDataPath, "database");
        }

        public void SaveJson()
        {
            SaveData saveData = new SaveData();
            saveData.DBData = DB.Instance.Data;
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path,json);
        }

        public void LoadJson()
        {
            SaveData saveData = new SaveData();
            if (!File.Exists(path))
            {
                //데이터 없을경우
            }
            else
            {
                string rawJson = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<SaveData>(rawJson);
                DB.Instance.SetData(saveData.DBData);
                DB.Instance.OnDBUpdateEvent.Invoke();
            }
        }
    }
}