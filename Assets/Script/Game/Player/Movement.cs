using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using Script.Global;
using UnityEngine;

namespace Script.Game.Player
{
    /// <summary>
    /// 이동 함수.
    /// Move 관련 함수로 모든 이동을 취합한 다음
    /// 그 취합된 벡터를 기반으로 오브젝트 이동
    /// TODO: 방식이 좀 복잡해서 그냥 Player와 합쳐도 무방할듯
    /// </summary>
    public class Movement : MonoBehaviour, DBUser
    {
        private Rigidbody2D body;
        private Player p;
        public float defaultSpeed;
        public float dashDistance;
        public float dashTime;
        public float dashCoolTime;
        
        public bool isMoving;
        
        public Vector3 currentVelocity;
        public Vector3 previousVelocity;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            body = transform.GetComponent<Rigidbody2D>();
            p = GetComponent<Player>();
            previousVelocity = currentVelocity = Vector3.zero;
            ApplyDBdata();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (currentDashCooltime > 0)
            {
                currentDashCooltime -= Time.deltaTime;
            }
        }

        
        private void FixedUpdate()
        {
            
            if (p.CanMove)
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
            if (isDashing)
                return;
            currentVelocity += new Vector3(delta * defaultSpeed, 0);
        }

        internal float currentDashCooltime = 0;
        internal bool isDashing = false;
        public void Dash(float delta)
        {
            if (currentDashCooltime > 0)
                return;
            float distance = delta * dashDistance;
            StartCoroutine(DashRoutine(distance, dashTime));
            p.MakeInvinvincible(dashTime);
        }

        public IEnumerator DashRoutine(float distance, float time)
        {
            int num = 10;
            isDashing = true;
            for (int i = 0; i < num; i++)
            {
                currentVelocity = Vector3.right * (distance / num);
                yield return new WaitForSeconds((float)time / num);
            }

            currentDashCooltime = dashCoolTime;
            isDashing = false;
        }

        public void ApplyDBdata()
        {
            if (DB.PlayerDashAmount != null)
                dashDistance = float.Parse(DB.PlayerDashAmount[0]);
            if (DB.PlayerDashTime != null)
            {
                dashTime = float.Parse(DB.PlayerDashTime[0]);
                dashCoolTime = float.Parse(DB.PlayerDashTime[1]);
            }
        }
    }
}
