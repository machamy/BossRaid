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
        [FormerlySerializedAs("onDown")]public UnityEvent onDownEvent;
        [FormerlySerializedAs("onUp")] public UnityEvent onUpEvent;
        [FormerlySerializedAs("onPressing")] public UnityEvent onPressingEvent;
        

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            onDownEvent.Invoke();
            IsPressing = true;
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            onUpEvent.Invoke();
            IsPressing = false;
        }

        private void OnPointerPressing()
        {
            onPressingEvent.Invoke();
        }
        
        protected virtual void FixedUpdate()
        {
            if(IsPressing) OnPointerPressing();
        }
        
    }
}