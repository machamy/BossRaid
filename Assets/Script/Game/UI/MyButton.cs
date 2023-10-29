using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Game
{
    public class MyButton: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
         {
        public bool IsPressing { get; private set; }
        
        [Header("MyButton")]
        public UnityEvent onDown;
        public UnityEvent onUp;
        public UnityEvent onPressing;
        

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            onDown.Invoke();
            IsPressing = true;
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            onUp.Invoke();
            IsPressing = false;
        }

        private void OnPointerPressing()
        {
            onPressing.Invoke();
        }
        
        protected virtual void FixedUpdate()
        {
            if(IsPressing) OnPointerPressing();
        }
        
    }
}