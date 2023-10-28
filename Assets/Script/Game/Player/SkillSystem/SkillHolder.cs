using System;
using UnityEngine;

namespace Script.Game.Player
{
    /// <summary>
    /// 스킬의 정보를 가지고 실행하는 곳
    /// </summary>
    public class SkillHolder : MonoBehaviour
    {
        enum SkillState
        {
            ready, active, cooldown
        }
        
        public BaseSkill skill;
        private float cooldownTime;
        private float acitveTime;
        private SkillState state = SkillState.ready;
        private Player p;

        private void Awake()
        {
            p = GetComponentInParent<Player>();
        }


        private void Update()
        {
            switch (state)
            {
                case SkillState.ready:
                    break;
                case SkillState.active:
                    acitveTime -= Time.deltaTime;
                    cooldownTime -= Time.deltaTime;
                    break;
                case SkillState.cooldown:
                    if (cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        this.state = SkillState.ready;
                    }
                    break;
            }
        }

        public void Activate()
        {
            if (state == SkillState.ready)
            {
                skill.Activate(p);
            }
        }
    }
}