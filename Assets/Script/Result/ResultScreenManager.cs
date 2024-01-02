using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Global;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// TODO: UI메니저와 역할이 겹침. 상속관계로 만들수 있을듯?
/// </summary>
public class ResultScreenManager : BaseInputManager, DBUser
{
    [SerializeField] private GameObject UIObject;

    private int hpValue;
    private int scoreValue;
    private int phaseValue;
    
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI msgText;
    private TextMeshProUGUI gradeText;
    
    private Transform score;
    private Transform msg;
    private Transform grade;
    

    private bool isOnProgress;

    private void Awake()
    {
        isOnProgress = false;
    }

    // Start is called before the first frame update
    void Start()
    {
            base.Start();
        score = UIObject.transform.Find("Score");
        msg = UIObject.transform.Find("Msg");
        grade = UIObject.transform.Find("Grade");
        
        scoreText = score.GetComponentInChildren<TextMeshProUGUI>();
        msgText = msg.GetComponentInChildren<TextMeshProUGUI>();
        gradeText = grade.GetComponentInChildren<TextMeshProUGUI>();
        
        scoreText.SetText("dummy");
        msgText.SetText("dummy");
        gradeText.SetText("dummy");
        Debug.Log(scoreText.text);
        score.gameObject.SetActive(false);
        msg.gameObject.SetActive(false);
        grade.gameObject.SetActive(false);
        
        ApplyDBdata();
        
        if (!PlayerPrefs.HasKey("result_score"))
        {
            hpValue = scoreValue = 0;
        }
        else
        {
            hpValue = PlayerPrefs.GetInt("result_hp");
            phaseValue = PlayerPrefs.GetInt("phase_value");
            scoreValue = PlayerPrefs.GetInt("result_score");
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
        SoundManager.Instance.Play("BGM/Clear",SoundManager.SoundType.BGM);
        isOnProgress = true;
        yield return new WaitForSeconds(1.0f);
        
        score.gameObject.SetActive(true);
        scoreText.SetText($"{scoreValue.ToString("D"+UIManager.ZERO_NUM)}");
        yield return new WaitForSeconds(1.0f);
        
        msg.gameObject.SetActive(true);
        msgText.SetText($"{msgs[Math.Min(phaseValue, msgs.Length-1)]}");
        yield return new WaitForSeconds(1.0f);
        
        grade.gameObject.SetActive(true);
        gradeText.SetText($"{GRADE_NAMES[Math.Min(phaseValue, GRADE_NAMES.Length-1)]}");
        yield return new WaitForSeconds(0.5f);
        
        isOnProgress = false;
    }

    /// <summary>
    /// 진행이 끝났을 때 타이틀 화면으로 돌아간다.
    /// </summary>
    private void GoTitle()
    {
        if(isOnProgress)
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
    private string[] GRADE_NAMES = new string[] { "X", "F", "D", "C", "B", "A", "P" };
    private string[] msgs = {"버그! 0",
        "작별이다 학생,\n F가 없는 시대에 태어났을 뿐인 \"범부\"여",
        "\"그런 학점으로 괜찮은가?\"",
        "교수여, 저장된 과제는 충분한가?",
        "\"파이어 학점이 되어줘\"",
        "이제부터는, 내가 하늘에 서겠다",
        "버그! 6"
    } ;
    public void ApplyDBdata()
    {
        if (DB.TextResultMsg != null)
        {
            msgs = (string[])DB.TextResultMsg.Clone();
        }
        if (DB.TextResultScore != null)
        {
            msgs = (string[])DB.TextResultScore.Clone();
        }
        // if (DB.TextResultClearhigh == null)
        //     return;
        // msgs[1] = DB.TextResultFailLowScore[0];
        // msgs[2] = DB.TextResultClearlow[0];
        // msgs[3] = DB.TextResultClearmid[0];
        // msgs[4] = DB.TextResultClearhigh[0];
        // msgs[5] = DB.TextResultClearPer[0];
        // GRADE_NAMES[1] = DB.TextResultFailLowScore[3];
        // GRADE_NAMES[2] = DB.TextResultClearlow[3];
        // GRADE_NAMES[3] = DB.TextResultClearmid[3];
        // GRADE_NAMES[4] = DB.TextResultClearhigh[3];
        // GRADE_NAMES[5] = DB.TextResultClearPer[3];
        for (var i1 = 0; i1 < msgs.Length; i1++)
        {
            msgs[i1] = msgs[i1].Replace("<br>","\n");
        }
    }
}
