using System;
using Script.Game.Projectile;
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

        public void SetData(string[] data)
        {
            Debug.Log($"[BaseSkill::SetData] {type.ToString()} : {data[0]} {data[1]} {data[2]} {data[3]}");
            cooldown = float.Parse(data[0]);
            duration = float.Parse(data[1]);
            isDirectional = data[2] == "TRUE";
            PrjtType.TryParse(data[3], out type);
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
            p.Parry(type, isDirectional);
        }
    }
}