using System;
using System.Collections;
using Script.Global;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Script.Game.Player
{
    /// <summary>
    /// 스킬의 정보를 가지고 실행하는 곳
    /// </summary>
    public class SkillHolder : MonoBehaviour, DBUser
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
        
        [NonSerialized]
        public Image cooldownImg;
        
        private State state = State.ready;
        private Player p;

        public float CooldownTime{ get=> cooldownTime; }
        public float ActiveTime{ get=> acitveTime; }

        private void Awake()
        {
            p = GetComponentInParent<Player>();
        }

        private void Start()
        {
            ApplyDBdata();
        }

        public void ApplyDBdata()
        {
            var data = DB.Get(skill.name);
            if (data == null)
                return;
            skill.SetData(data);
        }

        /// <summary>
        /// 스킬 활성화시 실행되고 있는 루틴
        /// skill의 OnActivete를 FixedUpdate마다 실행
        /// </summary>
        /// <returns></returns>
        private IEnumerator ActiveRoutine()
        {
            state |= State.active;
            acitveTime = skill.Duration;
            if (!skill.CanActiveMove)
                p.CanMove = false;
            // 활성화시
            while (acitveTime > 0)
            {
                acitveTime -= Time.deltaTime;
                skill.OnActivate(p);
                yield return new WaitForFixedUpdate();
            }
            if (!skill.CanActiveMove)
                p.CanMove = true;
            
            state = State.cooldown;
        }
        /// <summary>
        /// 스킬이 쿨타임 중일때 실행되고 있는 루틴
        /// cooltimeImg 를 수정해 쿨다운 표시
        /// 끝났을때 skill.OnEndCooldown을 실행
        /// </summary>
        /// <returns></returns>
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
            if(!p.IsAlive)
                return;
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