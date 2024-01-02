using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
    

namespace Script.Game.UI
{
    public class SimpleUI: BaseUI, IPointerDownHandler, IPointerUpHandler 
    {


        public void OnTouch()
        {
            ClickExit();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnTouch();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}