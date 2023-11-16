using System;
using Script.Game.Projectile;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Game.Player
{
    [CreateAssetMenu]
    public class BaseSkill: ScriptableObject
    {
        [SerializeField] private float delay;
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField] private bool isDirectional;
        [SerializeField] private string animation;
        [SerializeField] private Projectile.PrjtType type;
        [SerializeField] private bool canActiveMove;
        
        public float Delay => delay;
        public float Cooldown => cooldown;
        public float Duration => duration;
        public bool IsDirectional => isDirectional;
        public bool CanActiveMove => canActiveMove;

        public void SetData(string[] data)
        {
            Debug.Log($"[BaseSkill::SetData] {type.ToString()} : {data[0]} {data[1]} {data[2]} {data[3]} {data[4]} {data[5]}");
            delay = float.Parse(data[0]);
            cooldown = float.Parse(data[1]);
            duration = float.Parse(data[2]);
            isDirectional = data[3] == "TRUE";
            PrjtType.TryParse(data[4], out type);
            canActiveMove = data[5] == "TRUE";
        }
        
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
            p.Parry(type, IsDirectional);
        }
    }
}