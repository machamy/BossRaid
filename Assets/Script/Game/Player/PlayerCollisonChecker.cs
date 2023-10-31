using System;
using UnityEngine;

namespace Script.Game.Player
{
    public class PlayerCollisonChecker: MonoBehaviour
    {
        private Player p;
        private void Awake()
        {
            p = transform.parent.GetComponent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!p.IsAlive)
                return;
            if (other.transform.CompareTag("Projectile"))
            {
                Projectile.Projectile prj = other.transform.GetComponent<Projectile.Projectile>();
                prj.OnHit(p);
            }
        }
    }
}