using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameSceneInputManager : BaseInputManager
    {
        [SerializeField] public GameObject PauseMenu;
        
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
            //SceneManager.LoadScene("Scenes/TitleScreen");
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

        }

        public void Help()
        {
            //UI 띄우기
        }

        public void Tomain()
        {
            SceneManager.LoadScene("Scenes/TitleScreen");
        }
    }
}