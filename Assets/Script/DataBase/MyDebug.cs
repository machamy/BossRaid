using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Global
{
    public class MyDebug
    {
        private static MyDebug instance;

        public static MyDebug Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyDebug();
                    instance.init(DB.DEBUG);
                }
                return instance;
            }
        }

        private Dictionary<string, string> data = new Dictionary<string, string>();

        public void init(string[] rawData)
        {
            if (rawData == null)
                return;
            foreach (var raw in rawData)
            {
                if(raw.Length < 2)
                    continue;
                if (!raw.Contains(" "))
                {
                    data[raw.ToLower()] = "1";
                    continue;
                }
                var arr = raw.Split(" ");
                data[arr[0].ToLower()] = arr[1];
            }
        }
        
        public int getInt(string name)
        {
            string raw;
            if (!data.TryGetValue(name.ToLower(), out raw))
                return -1;
            return Int32.Parse(raw);
        }

        public bool getBool(string name)
        {
            string raw;
            if (!data.TryGetValue(name.ToLower(), out raw))
                return false;
            return Int32.Parse(raw) != -1;
        }
    }
}