using System;
using UnityEngine.Device;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class TitleSceneInputManager : BaseInputManager
    {
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
            Application.Quit();
        }

        protected override void OnMenu()
        {
            
        }
    }
}