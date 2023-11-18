using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class PauseController : MonoBehaviour
    {
        public static bool IsPause = false;

        public GameObject PauseMenu;

        // Start is called before the first frame update
        void Start()
        {
            IsPause = false;
        }

        // Update is called once per frame
        void Update()
        {
            //PauseOfAndroid();
            TestPause();
        }

        public void TestPause()
        {
            // esc 누르면 퍼즈
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsPause == false)
                {
                    ToPause();
                }
                else
                {
                    Resume();
                }
            }
        }

        public void Resume()
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f; // 일시정지 해제
            IsPause = false;
        }

        public void ToPause()
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f; // 일시정지
            IsPause = true;
        }

        // 안드로이드 뒤로가기 pause
        public void PauseOfAndroid()
        {
            if (Application.platform == RuntimePlatform.Android) {
                // Application.Quit();
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    if (IsPause == false) {
                        ToPause();
                    }
                    else {
                        Resume();
                    }
                }
            }
        }

        public void ToMain()
        {

        }

        public void Quit()
        {
            
        }
        
    }

}
