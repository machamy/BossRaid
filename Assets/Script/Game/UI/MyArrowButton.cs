using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Script.Game
{   
    /// <summary>
    /// 터치 방향키 클래스
    /// 꾹 눌러서 전환 가능
    /// </summary>
    public class MyArrowButton : MyButton, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 _positionPointer;
        public float midRange;
        
        [FormerlySerializedAs("onLeft")] public UnityEvent onLeftEvent;
        [FormerlySerializedAs("onRight")] public UnityEvent onRightEvent;

        private float interval = 0.25f;
        private float prevTimeL = -1f;
        private float prevTimeR = -1f;
        
        public UnityEvent onLeftDoubleEvent;
        public UnityEvent onRightDoubleEvent;
        /// <summary>
        /// 중간의 0으로 취급되는 너비
        /// </summary>
     

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _positionPointer = eventData.position;
    
            if (_positionPointer.x < transform.position.x-midRange)
            {
                if (Time.time - prevTimeL < interval)
                {
                    prevTimeL = -1.0f;
                    onLeftDoubleEvent.Invoke();
                }
                else
                {
                    prevTimeL = Time.time;
                }
            }
            else if (_positionPointer.x > transform.position.x+midRange)
            {
                if (Time.time - prevTimeR < interval)
                {
                    prevTimeR = -1.0f;
                    onRightDoubleEvent.Invoke();
                }
                else
                {
                    prevTimeR = Time.time;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _positionPointer = eventData.position;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _positionPointer = Vector2.zero;
        }
        
        protected override void FixedUpdate()
        {
            if(!IsPressing) return;
            
            if (_positionPointer.x < transform.position.x-midRange)
            {
                onLeftEvent.Invoke();
            }else if (_positionPointer.x > transform.position.x+midRange)
            {
                onRightEvent.Invoke();
            }
            
            base.FixedUpdate();
        }
    }
}