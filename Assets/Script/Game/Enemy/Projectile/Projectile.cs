﻿using System;
using System.Collections.Generic;
using System.Linq;
using Script.Game.Enemy.Projectile;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script.Game.Projectile
{


    public class Projectile : BaseProjectile
    {
        public HashSet<Projectile> Group { get; set; }

        private void Awake()
        {
            isParryable = true;
        }

        /// <summary>
        /// 생성시 실행. 수동실행필요
        /// </summary>
        public override void OnSummon()
        {
            SoundManager.Instance.Play(sound);
        }
        
        
        /// <summary>
        /// 피격 성공시 실행
        /// </summary>
        /// <param name="p"></param>
        public override void OnHit(Player.Player p)
        {
            p.HP -= damage;
            if(damage > 0 && Group != null)
                foreach (var prjt in Group)
                {
                    prjt.damage = 0;
                }
            Remove();
        }
        
        public override void Remove()
        {
            Destroy(gameObject);
        }
        
        /// <summary>
        /// 패링 성공시 실행
        /// 기본적으로 Destory까지 실행됨
        /// </summary>
        /// <param name="p"></param>
        public override void OnParring(Player.Player p)
        {
            p.Score += parringScore;
            if(parringScore > 0 && Group != null)
                
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
            transform.right = dir;
            facing = speed * dir;
        }

        private void Update()
        {
            
        }        

        private void FixedUpdate()
        {
            transform.Translate(speed * Vector2.right, Space.Self); 
            //transform.Translate(Vector3.left);

            Vector3 prjtPoint = Camera.main.WorldToScreenPoint(transform.position);
            if(prjtPoint.x<0 || prjtPoint.x>Screen.width || prjtPoint.y<0 || prjtPoint.y >Screen.height)
                Remove();
        }


        
        
    }
    
}