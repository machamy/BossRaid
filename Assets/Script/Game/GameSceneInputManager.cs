using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Script.Game
{
    
    /// <summary>
    /// 안드로이드 입력 관리중
    /// </summary>
    public class GameSceneInputManager : BaseInputManager
    {
        public IngameMenuManager PauseMenu;
        
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
            PauseMenu.Toggle();
        }

        protected override void OnMenu()
        {
            
        }


    }
}