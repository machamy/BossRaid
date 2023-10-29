using System;
using UnityEngine;

namespace Script.Game.Player
{
    /// <summary>
    /// 스킬의 정보를 가지고 실행하는 곳
    /// </summary>
    public class SkillHolder : MonoBehaviour
    {
        [Flags]
        enum State
        {
            ready = 0,
            active = 1 << 0,
            cooldown = 1 << 1,
            all = active | cooldown
        }

        public BaseSkill skill;
        private float cooldownTime;
        private float acitveTime;
        private State state = State.ready;
        private Player p;

        public float CooldownTime{ get=> cooldownTime; }
        public float ActiveTime{ get=> acitveTime; }

        private void Awake()
        {
            p = GetComponentInParent<Player>();
        }


        private void Update()
        {
            // 활성화시
            if (state.HasFlag(State.active))
            {
                if (acitveTime > 0)
                {
                    acitveTime -= Time.deltaTime;
                    skill.OnActivate(p);
                }
                else
                {
                    skill.OnBeginCooldown(p);
                    this.state = State.cooldown;
                }
            }
            // 쿨타임시
            if (state.HasFlag(State.cooldown))
            {
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    skill.OnEndCooldown(p);
                    this.state = State.ready;
                }
            }
        }

        public void Activate()
        {
            if (state == State.ready)
            {
                skill.OnBeginActivate(p);
                cooldownTime = skill.Cooldown;
                this.state = State.all;
            }
        }
    }
}