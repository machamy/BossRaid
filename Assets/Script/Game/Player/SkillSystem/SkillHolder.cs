using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
        
        public Image cooldownImg;
        
        private State state = State.ready;
        private Player p;

        public float CooldownTime{ get=> cooldownTime; }
        public float ActiveTime{ get=> acitveTime; }

        private void Awake()
        {
            p = GetComponentInParent<Player>();
        }


        private IEnumerator ActiveRoutine()
        {
            state |= State.active;
            acitveTime = skill.Duration;
            // 활성화시
            while (acitveTime > 0)
            {
                acitveTime -= Time.deltaTime;
                skill.OnActivate(p);
                yield return new WaitForFixedUpdate();
            }
            
            state = State.cooldown;
        }
        private IEnumerator CoolDownRoutine()
        {
            state |= State.cooldown;
            float initialCooltime = cooldownTime = skill.Cooldown;
            skill.OnBeginCooldown(p);
            // 쿨타임시
            while(cooldownTime > 0)
            {
                cooldownTime -= Time.deltaTime;
                cooldownImg.fillAmount = cooldownTime / initialCooltime;
                yield return new WaitForFixedUpdate();
            }

            cooldownImg.fillAmount = 0;
            skill.OnEndCooldown(p);
            state = State.ready;
        }

        public void Activate()
        {
            if (state == State.ready)
            {
                Debug.Log("[SkillHolder::Activate]'" + skill.name + " is Activated!");
                skill.OnBeginActivate(p);
                StartCoroutine(ActiveRoutine());
                StartCoroutine(CoolDownRoutine());
            }
            else
            {
                if (state.HasFlag(State.active))
                {
                    Debug.Log("[SkillHolder::Activate]'" + skill.name + "' is activating : " + ActiveTime);
                }

                if (state.HasFlag(State.cooldown))
                {
                    Debug.Log("[SkillHolder::Activate]'" + skill.name + "' is on cooling : " + CooldownTime);
                }
            }
        }
    }
}