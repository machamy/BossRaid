using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TitleManager : MonoBehaviour
{
    public string gameSceneName = "GameScreen";
    public Text dbbtn_text;
    
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
        dbbtn_text.text = sb.ToString();
    }
}
