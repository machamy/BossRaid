using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// TODO: UI메니저와 역할이 겹침. 상속관계로 만들수 있을듯?
/// </summary>
public class ResultScreenManager : BaseInputManager
{
    private int hp;
    private int score;
    [SerializeField] private GameObject UIObject;
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI scoreText;

    private bool isOnProgress;

    private void Awake()
    {
        isOnProgress = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = UIObject.transform.Find("TxtScore").GetComponent<TextMeshProUGUI>();
        hpText = UIObject.transform.Find("TxtHP").GetComponent<TextMeshProUGUI>();
        scoreText.enabled = false;
        hpText.enabled = true;
        if (!PlayerPrefs.HasKey("result_score"))
        {
            hp = score = 0;
        }
        else
        {
            hp = PlayerPrefs.GetInt("result_hp");
            score = PlayerPrefs.GetInt("result_score");
        }

        StartCoroutine(ShowResultPage());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.touchCount > 0)
        {
            GoTitle();
        }
    }
    
    /// <summary>
    /// 결과창 출력 루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowResultPage()
    {
        isOnProgress = true;
        scoreText.SetText(score.ToString("D"+10));
        hpText.SetText(hp.ToString());
        yield return new WaitForSeconds(1.5f);
        scoreText.enabled = true;
        yield return new WaitForSeconds(3.8f);
        hpText.enabled = true;
        yield return new WaitForSeconds(2.0f);
        isOnProgress = false;
    }

    /// <summary>
    /// 진행이 끝났을 때 타이틀 화면으로 돌아간다.
    /// </summary>
    private void GoTitle()
    {
        if(!isOnProgress)
            return;
        SceneManager.LoadScene("Scenes/TitleScreen");
    }

    protected override void OnHome()
    {
        
    }

    protected override void OnEscape()
    {
        GoTitle();
    }

    protected override void OnMenu()
    {
        
    }


}
