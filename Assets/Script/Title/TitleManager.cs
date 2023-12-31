using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

/// <summary>
/// 타이틀 화면 관리자
/// </summary>
public class TitleManager : MonoBehaviour
{
    public string gameSceneName = "GameScreen";
    [FormerlySerializedAs("dbbtn_text")] public Text DB_BTN_TEXT;
    [SerializeField] private GameObject UI;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private Image fadeImg;
    [SerializeField] private Sprite[] backgroundSprite;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private GSpreadSheetLoader _sheetLoader;
    [FormerlySerializedAs("Option")] public GameObject OptionPrefeb;
    public GameObject OptionUI;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        SoundManager.Instance.Play("BGM/Menu",SoundManager.SoundType.BGM);
        var option = Instantiate(OptionPrefeb,parent:UI.transform);
        OptionUI = option;
        
        if(DB.DB_VERSION == null)
            _sheetLoader.StartDownload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartRoutine()
    {
        backgroundImg.sprite = backgroundSprite[1];
        fadeImg.gameObject.SetActive(true);
        
        float time = 0.5f;
        int maxFrame = 16;
        int frame = 1;
        while (frame <= maxFrame)
        {
            Color c = fadeImg.color;
            c.a = (float)frame / maxFrame;
            fadeImg.color = c;
            frame++;
            yield return new WaitForSeconds(time/maxFrame);
        }
        //backgroundImg.sprite = backgroundSprite[0];
        SceneManager.LoadScene(gameSceneName);
        SoundManager.Instance.Clear();
    }
    
    public void ClickStart()
    {
        SoundManager.Instance.Play(clickSound);
        StartCoroutine(StartRoutine());
    }

    public void ClickHowto()
    {
        SoundManager.Instance.Play(clickSound);
    }

    public void ClickCredit()
    {
        SoundManager.Instance.Play(clickSound);
    }

    public void ClickOption()
    {
        SoundManager.Instance.Play(clickSound);
        OptionUI.SetActive(true);
    }

    public void ClickQuit()
    {
        SoundManager.Instance.Play(clickSound);
        Application.Quit();
    }

    /// <summary>
    /// DB 변경 리스너.
    /// 버튼의 텍스트를 DB버전으로 바꾼다.
    /// </summary>
    public void OnDBUpdate()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DB : ");
        sb.Append(string.Join("_", DB.DB_VERSION));
        DB_BTN_TEXT.text = sb.ToString();
    }
}
