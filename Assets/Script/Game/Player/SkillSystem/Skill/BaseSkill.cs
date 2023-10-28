using System;
using UnityEngine;

namespace Script.Game.Player
{
    [CreateAssetMenu]
    public class BaseSkill: ScriptableObject
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField] private bool isDirectional;
        [SerializeField] private string animation;
        [SerializeField] private Projectile.PrjtType type;
        
        public float Cooldown => cooldown;
        public float Duration => duration;
        public bool IsDirectional => isDirectional;

        public virtual void Activate(Player p)
        {
            
        }

        public virtual void BeginCooldown(Player p)
        {
            
        }
    }
}