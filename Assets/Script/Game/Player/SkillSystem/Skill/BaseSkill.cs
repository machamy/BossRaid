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

        /// <summary>
        /// 시전시 실행
        /// </summary>
        /// <param name="p"></param>
        public virtual void OnBeginActivate(Player p)
        {
            p.Parry(type, isDirectional);
        }

        /// <summary>
        /// 쿨타임 시작시 실행
        /// </summary>
        /// <param name="p"></param>
        public virtual void OnBeginCooldown(Player p)
        {
            
        }
        /// <summary>
        /// 쿨타임 끝났을때 실행
        /// </summary>
        /// <param name="p"></param>
        public virtual void OnEndCooldown(Player p)
        {
            
        }

        /// <summary>
        /// Actiavete시 매 Update마다 실행
        /// </summary>
        /// <param name="p"></param>
        public virtual void OnActivate(Player p)
        {
            p.Parry(type, isDirectional);
        }
    }
}