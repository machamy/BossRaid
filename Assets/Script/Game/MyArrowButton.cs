using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Script.Game
{
    public class MyArrowButton : MyButton, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 _positionPointer;
        public UnityEvent onLeft;
        public UnityEvent onRight;

        public float midRange;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _positionPointer = eventData.position;
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
                onLeft.Invoke();
            }else if (_positionPointer.x > transform.position.x+midRange)
            {
                onRight.Invoke();
            }
            
            base.FixedUpdate();
        }
    }
}