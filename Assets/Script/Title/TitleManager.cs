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

/// <summary>
/// 타이틀 화면 관리자
/// </summary>
public class TitleManager : MonoBehaviour
{
    public string gameSceneName = "GameScreen";
    [FormerlySerializedAs("dbbtn_text")] public Text DB_BTN_TEXT;
    [SerializeField]
    private GameObject OptionUI;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        SoundManager.Instance.Play("BGM/Menu",SoundManager.SoundType.BGM);
        OptionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ClickStart()
    {
        SceneManager.LoadScene(gameSceneName);
        SoundManager.Instance.Clear();
    }

    public void ClickHowto()
    {
        
    }

    public void ClickCredit()
    {
        
    }

    public void ClickOption()
    {
        OptionUI.SetActive(true);
    }

    public void ClickQuit()
    {
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
