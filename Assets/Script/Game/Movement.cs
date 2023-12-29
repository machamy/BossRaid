using UnityEngine;

namespace Script.Game
{
    /// <summary>
    /// 이동 함수.
    /// Move 관련 함수로 모든 이동을 취합한 다음
    /// 그 취합된 벡터를 기반으로 오브젝트 이동
    /// TODO: 방식이 좀 복잡해서 그냥 Player와 합쳐도 무방할듯
    /// </summary>
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D body;
        public float defaultSpeed;

        public bool canMove;
        public bool isMoving;
        
        public Vector3 currentVelocity;
        public Vector3 previousVelocity;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            body = transform.GetComponent<Rigidbody2D>();
            previousVelocity = currentVelocity = Vector3.zero;
            canMove = true;
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        
        private void FixedUpdate()
        {
            
            if (canMove)
            {
                ApplyMovement();
                if (currentVelocity.magnitude != 0)
                    isMoving = true;
                else
                    isMoving = false;
            }
            previousVelocity = currentVelocity;
            currentVelocity = Vector3.zero; // 관성 제거
        }

        /// <summary>
        /// 저장된 이동 속도에 따라 움직임.
        /// </summary>
        private void ApplyMovement()
        {
            transform.Translate(currentVelocity);
        }
        
        /// <summary>
        /// 횡이동. 1회 실행 등의 단순한 이동.
        /// </summary>
        /// <param name="delta">속도 배수</param>
        public void HorizontalMove(float delta)
        {
            currentVelocity += new Vector3(delta * defaultSpeed, 0);
        }
    }
}
