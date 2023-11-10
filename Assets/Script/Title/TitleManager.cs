using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ClickStart()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    /// <summary>
    /// DB 변경 리스너.
    /// 버튼의 텍스트를 DB버전으로 바꾼다.
    /// </summary>
    public void OnDBUpdate()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DB : ");
        foreach (var s in DB.DB_VERSION)
        {
            sb.Append(s);
            sb.Append('_');
        }
        sb.Remove(sb.Length - 1, 1);
        DB_BTN_TEXT.text = sb.ToString();
    }
}
