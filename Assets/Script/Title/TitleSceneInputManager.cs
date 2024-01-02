using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

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

        private float previousTime = -1f;
        protected override void OnEscape()
        {
            if (Math.Abs(Time.time - previousTime) < 0.4)
            {
                Application.Quit();
            }

            previousTime = Time.time;
        }

        protected override void OnMenu()
        {
            
        }
    }
}
