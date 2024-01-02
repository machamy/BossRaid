using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace Script.Game.UI
{
    
    public class SimpleUI: MonoBehaviour
    {
        public GameObject ReturnObject;
        
        
        public virtual void OnBeforeExit()
        {
            
        }
        
        public void ClickExit()
        {
            OnBeforeExit();
            transform.parent.gameObject.SetActive(false);

            if (ReturnObject)
                ReturnObject.SetActive(true);
        }
    }
}