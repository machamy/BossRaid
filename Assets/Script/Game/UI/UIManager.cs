using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = System.Random;

/// <summary>
/// UI를 담당함
/// </summary>
public class UIManager : MonoBehaviour
{

    private float[] hp_bar_const = { 0f,0.133f,0.366f,0.699f,1.0f };
    
    
    [SerializeField]
    private GameObject UIObject;

    // private TextMeshProUGUI hpText;
    [SerializeField]
    private Image BarHP;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public const int ZERO_NUM = 10;
    void Start()
    {
        //scoreText = UIObject.transform.Find("TxtScore").GetComponent<TextMeshProUGUI>();
        // hpText = UIObject.transform.Find("TxtHP").GetComponent<TextMeshProUGUI>();
       // BarHP = UIObject.transform.Find("BarHP").GetComponent<Image>();
        Player p = GameObject.Find("Player").GetComponent<Player>();
        p.OnHPUpdateEvent.AddListener((i) => UpdateHP(i));
        p.OnScoreUpdateEvent.AddListener((i) => UpdateScore(i));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void UpdateHP(int hp)
    {
        //hpText.SetText(SCORE_NAMES[Math.Min(3,hp)]);
        BarHP.fillAmount = hp_bar_const[Math.Min(4,hp)];
    }

    public void UpdateScore(int score)
    {
        scoreText.SetText(score.ToString("D"+ZERO_NUM));
    }
}
