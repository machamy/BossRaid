using System;
using Script.Game.Enemy.Projectile;
using UnityEngine;

namespace Script.Game.Player
{
    /// <summary>
    /// 플레이어 히트박스
    /// </summary>
    public class PlayerCollisonChecker: MonoBehaviour
    {
        private Player p;
        private void Awake()
        {
            p = transform.parent.GetComponent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!p.IsAlive || p.IsInvincible)
                return;
            if (other.transform.CompareTag("Projectile"))
            {
               BaseProjectile prj = other.transform.GetComponent<BaseProjectile>();
               prj.OnHit(p);
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!p.IsAlive || p.IsInvincible)
                return;
            if (other.transform.CompareTag("Projectile"))
            {
                BaseProjectile prj = other.transform.GetComponent<BaseProjectile>();
                prj.OnHit(p);
            }
        }
    }
}