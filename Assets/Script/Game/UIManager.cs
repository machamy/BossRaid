using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class UIManager : MonoBehaviour
{
    private static readonly string[] SCORE_NAMES = new string[] { "F", "C", "B", "A" };

    [SerializeField]
    private GameObject UIObject;

    private TextMeshProUGUI hpText;
    private TextMeshProUGUI scoreText;
    private const int ZERO_NUM = 10;
    void Start()
    {
        scoreText = UIObject.transform.Find("TxtScore").GetComponent<TextMeshProUGUI>();
        hpText = UIObject.transform.Find("TxtHP").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHP(int hp)
    {
        hpText.SetText(SCORE_NAMES[hp]);
    }

    public void UpdateScore(int score)
    {
        scoreText.SetText(score.ToString("D10"));
    }
}
