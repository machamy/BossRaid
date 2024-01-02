using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Script.Game
{
    // TODO : 대부분 요소를 UIManager로 옮겨야함... 하지만 게임 크기 커지지 않는 이상 그냥 둘 예정.
    public class GameSceneInputManager : BaseInputManager
    {
        [SerializeField] public GameObject UI;
        [SerializeField] public GameObject PauseUI;
        [SerializeField] public GameObject PauseMenu;
        [SerializeField] public GameObject OptionUI;
  
        
        public string gameSceneName = "GameScreen";
        
        public bool IsPause { get; private set; } = false;


        // Start is called before the first frame update
        private void Start()
        {
            base.Start();
            OptionUI = Instantiate(OptionUI,PauseUI.transform);
            var manager = OptionUI.GetComponentInChildren<UI.OptionManager>();
            //manager.ReturnObject = PauseMenu;
            // Debug.Log(manager.ReturnObject);
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void OnHome()
        {
            
        }

        protected override void OnEscape()
        {
            if (!IsPause)
            {
                ToPause();
            }
            else
            {
                Resume();
            }
        }

        protected override void OnMenu()
        {
            
        }

        public void ToPause()
        {
            if (IsPause)
                return;
            PauseUI.SetActive(true);
            Time.timeScale = 0f; 
            IsPause = true;
        }

        public void Resume()
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f; 
            IsPause = false;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(gameSceneName);
        }

        public void OptionIn()
        {
            //UI 띄우기
            //PauseMenu.SetActive(false);
            OptionUI.SetActive(true);
        }

        public void Tomain()
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene("Scenes/TitleScreen");
        }
    }
}