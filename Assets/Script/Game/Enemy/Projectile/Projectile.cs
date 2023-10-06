using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script.Game.Projectile
{
    public class Projectile: MonoBehaviour
    {
        public bool parryable;
        public float speed;
        public Vector2 velocity;
        public Vector2 target;
        
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

        public void UpdateDirection()
        {
            var dir = (target - new Vector2(transform.position.x, transform.position.y)).normalized;
            UpdateDirection(dir);
        }
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
    
    enum Type
    {
        과제, 팀플, 출석
    }
}