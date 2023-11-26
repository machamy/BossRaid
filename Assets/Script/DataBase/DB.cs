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

    private Dictionary<string, string[]> data = new ();
    public static string[] DB_VERSION => Get("DB_VERSION");	//string
    public static string[] PlayerSpeed => Get("PlayerSpeed");	//float
    public static string[] PlayerHP => Get("PlayerHP");	//int
    public static string[] PlayerInvicbleTime => Get("PlayerInvicbleTime");	//float
    public static string[] HeyYoung => Get("HeyYoung");	//float float bool string bool
    public static string[] IPad => Get("IPad");	//float float bool string bool
    public static string[] Laptop => Get("Laptop");	//float float bool string bool
    public static string[] TeamProjectile => Get("TeamProjectile");	//float int int
    public static string[] PracticeProjectile => Get("PracticeProjectile");	//float int int
    public static string[] AttendProjectile => Get("AttendProjectile");	//float int int
    public static string[] AttendDefaultAmount => Get("AttendDefaultAmount");	//int
    public static string[] AttendLeftPosStart => Get("AttendLeftPosStart");	//float
    public static string[] AttendLeftPosEnd => Get("AttendLeftPosEnd");	//float
    public static string[] AttendRightPosStart => Get("AttendRightPosStart");	//float
    public static string[] AttendRightPosEnd => Get("AttendRightPosEnd");	//float
    public static string[] Phase0Probability => Get("Phase0Probability");	//int
    public static string[] Phase1Probability => Get("Phase1Probability");	//int
    public static string[] Phase2Probability => Get("Phase2Probability");	//int
    public static string[] Phase3Probability => Get("Phase3Probability");	//int
    public static string[] Phase4Probability => Get("Phase4Probability");	//int
    public static string[] Phase5Probability => Get("Phase5Probability");	//int
    public static string[] PhaseScores => Get("PhaseScores");	//int (소수는버림처리)
    public static string[] PhaseFrequencies => Get("PhaseFrequencies");	//float
    public static string[] TextResultFailLowScore => Get("TextResultFailLowScore");	//string
    public static string[] TextResultClearNoHit => Get("TextResultClearNoHit");	//string
    


    public void SetData(string key, string[] value)
    {
        data[key]= (value);
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

    /// <summary>
    /// key를 이용해 DB데이터를 가져옴. 없을경우 null반환
    /// </summary>
    /// <param name="key">가져올 데이터</param>
    /// <returns>데이터, 없으면 null</returns>
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
