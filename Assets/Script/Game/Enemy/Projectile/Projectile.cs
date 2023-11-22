using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script.Game.Projectile
{
    public enum PrjtType
    {
        Practice, Team, Attend
    }

    public class Projectile : MonoBehaviour
    {
        public float speed;
        public Vector2 facing;
        public Vector2 target;
        [SerializeField] private PrjtType type;
        public HashSet<Projectile> Group { get; set; }

        public PrjtType Type => type;

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
        /// 기본적으로 Destory까지 실행됨
        /// </summary>
        /// <param name="p"></param>
        public virtual void OnParring(Player.Player p)
        {
            p.Score += parringScore;
            Debug.Log(parringScore);
            foreach (var prjt in Group)
            {
                prjt.parringScore = 0;
            }
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
            float angle = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.up = dir;
            facing = speed * dir;
        }

        private void Start()
        {
            //5초 뒤 투사체 삭제
            Destroy(gameObject,5f);
        }
        

        private void FixedUpdate()
        {
           transform.Translate(speed * Vector2.up, Space.Self); 
            //transform.Translate(Vector3.left);
        }

        private void Remove()
        {
            Destroy(gameObject);
        }
        
        
    }
    
    
}