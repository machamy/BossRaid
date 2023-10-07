using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script.Game.Projectile
{
    public enum Type
    {
        과제, 팀플, 출석
    }
    public class Projectile: MonoBehaviour
    {
        public bool parryable;
        public float speed;
        public Vector2 velocity;
        public Vector2 target;
        public Type Type { get; private set;}
        
        public int damage;
        public int parringScore;
        /// <summary>
        /// 피격 성공시 실행
        /// </summary>
        /// <param name="p"></param>
        public virtual void OnHit(Player.Player p)
        {
            p.HP -= damage;
            Remove();
        }
        /// <summary>
        /// 패링 성공시 실행
        /// </summary>
        /// <param name="p"></param>
        public virtual void OnParring(Player.Player p)
        {
            p.Score += parringScore;
            Debug.Log(parringScore);
            Remove();
        }
        /// <summary>
        /// 이미 설정된 타겟으로 발사
        /// </summary>
        public void UpdateDirection()
        {
            var dir = (target - new Vector2(transform.position.x, transform.position.y)).normalized;
            UpdateDirection(dir);
        }
        /// <summary>
        /// 인자로 받은 방향으로 발사
        /// </summary>
        /// <param name="dir">투사체가 날아갈 방향</param>
        public void UpdateDirection(Vector2 dir)
        {
            velocity = speed * dir;
        }

        private void Start()
        {
            
        }
        

        private void FixedUpdate()
        {
            transform.Translate(velocity);
        }

        private void Remove()
        {
            Destroy(gameObject);
        }
    }
    
    
}