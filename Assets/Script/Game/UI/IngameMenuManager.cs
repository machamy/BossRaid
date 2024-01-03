using System;
using System.Collections.Generic;
using Script.Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Script.Game
{
    public class IngameMenuManager : BaseUI
    {
        [SerializeField] public GameObject UI;
        [SerializeField] public GameObject PauseUI;
        [SerializeField] public GameObject PauseMenu;
        [SerializeField] public GameObject OptionUI;

        private Stack<BaseUI> uiStack = new Stack<BaseUI>();
        public BaseUI CurrentUI
        {
            get => uiStack.Peek();
            set => uiStack.Push(value);
        }
        
        public string gameSceneName = "GameScreen";
        public bool IsPause { get; private set; } = false;

        private void Start()
        {
            OptionUI = Instantiate(OptionUI,PauseUI.transform);
        }

        public void Toggle()
        {
            if (!IsPause)
            {
                ToPause();
                return;
            }
            OnExit();
        }

        public void OnExit()
        {
            if (CurrentUI != null)
            {
                while (!uiStack.Peek().isActiveAndEnabled)
                    uiStack.Pop();
                uiStack.Pop().ClickExit();
            }
            else
            {
                Resume();
            }
        }
        
        public void ToPause()
        {
            if (IsPause)
                return;
            PauseUI.SetActive(true);
            Time.timeScale = 0f; 
            IsPause = true;
            CurrentUI = this;
        }

        public void Resume()
        {
            ClickExit();
        }

        public override void OnBeforeExit()
        {
            Time.timeScale = 1f; 
            IsPause = false;
            uiStack.Clear();
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            CurrentUI = null;
            SceneManager.LoadScene(gameSceneName);
        }

        public void OptionIn()
        {
            //UI 띄우기
            //PauseMenu.SetActive(false);
            OptionUI.SetActive(true);
            CurrentUI = OptionUI.GetComponentInChildren<OptionManager>();
        }

        public void Tomain()
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene("Scenes/TitleScreen");
        }
    }
}