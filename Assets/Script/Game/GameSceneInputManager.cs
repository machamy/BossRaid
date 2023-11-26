using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameSceneInputManager : BaseInputManager
    {
        [SerializeField] public GameObject PauseMenu;
        [SerializeField] public GameObject HelpPanel;

        public string gameSceneName = "GameScreen";
        
        public bool IsPause { get; private set; } = false;


        // Start is called before the first frame update
        private void Start()
        {
            base.Start();
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
            PauseMenu.SetActive(true);
            Time.timeScale = 0f; 
            IsPause = true;
        }

        public void Resume()
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f; 
            IsPause = false;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(gameSceneName);
        }

        public void HelpIn()
        {
            //UI 띄우기
            PauseMenu.SetActive(false);
            HelpPanel.SetActive(true);
        }

        public void HelpOut()
        {
            PauseMenu.SetActive(true);
            HelpPanel.SetActive(false);
        }

        public void Tomain()
        {
            SceneManager.LoadScene("Scenes/TitleScreen");
        }
    }
}