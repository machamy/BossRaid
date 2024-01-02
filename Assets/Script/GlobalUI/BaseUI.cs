using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace Script.Game.UI
{
    
    public class BaseUI: MonoBehaviour
    {
        public GameObject ReturnObject;
        public bool isChild;
        
        public virtual void OnBeforeExit()
        {
            
        }
        
        /// <summary>
        /// X 버튼과 연동 필요
        /// </summary>
        public void ClickExit()
        {
            OnBeforeExit();
            if(isChild)
                transform.parent.gameObject.SetActive(false);
            else
            {
                transform.gameObject.SetActive(false);
            }

            if (ReturnObject)
                ReturnObject.SetActive(true);
        }
    }
}