using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Game.Player;
using UnityEngine;

/// <summary>
/// 게임 DB가 담기는 싱글턴 클래스
/// </summary>
public class DB
{
    private static DB instance;
    
    public static DB Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = new DB();
            return instance;
        }
    }

    private Dictionary<string, string[]> data = new Dictionary<string, string[]>();
    
    public static string[] PlayerSpeed => Get("PlayerSpeed");	//float
    public static string[] PlayerHP => Get("PlayerHP");	//int
    public static string[] HeyYoung => Get("HeyYoung");	//float float bool string
    public static string[] IPad => Get("IPad");	//float float bool string
    public static string[] Laptop => Get("Laptop");	//float float bool string
    public static string[] TeamProjectile => Get("TeamProjectile");	//float int int
    public static string[] PracticeProjectile => Get("PracticeProjectile");	//float int int
    public static string[] AttendProjectile => Get("AttendProjectile");	//float int int
    public static string[] Phase0Probablity => Get("Phase0Probablity");	//int
    public static string[] Phase1Probablity => Get("Phase1Probablity");	//int
    public static string[] Phase2Probablity => Get("Phase2Probablity");	//int
    public static string[] Phase3Probablity => Get("Phase3Probablity");	//int
    public static string[] Phase4Probablity => Get("Phase4Probablity");	//int
    public static string[] Phase5Probablity => Get("Phase5Probablity");	//int
    public static string[] PhaseScores => Get("PhaseScores");	//int
    public static string[] PhaseFrequencies => Get("PhaseFrequencies");	//float


    public void SetData(string key, string[] value)
    {
        data.Add(key,value);
    }

    public void SetData(Dictionary<string, string[]> datadict)
    {
        foreach (var pair in datadict)
        {
            SetData(pair.Key,pair.Value);
        }
    }
    
    public static bool IsReady()
    {
        return Contains("PlayerSpeed");
    }
    public static bool Contains(string key)
    {
        return Instance.data.ContainsKey(key);
    }

    [CanBeNull]
    public static string[] Get(string key)
    {
        if(Contains(key)) return Instance.data[key];
        return null;
    }

    public static int GetSize(string key)
    {
        return Instance.data[key].Length;
    }
    
    public static string GetOne(string key, int idx = 0)
    {
        return Instance.data[key][idx];
    }
}
