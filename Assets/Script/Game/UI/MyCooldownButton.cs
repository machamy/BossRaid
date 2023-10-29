using System;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Script.Game
{
    /// <summary>
    /// 쿨타임이 그려지는 버튼.
    /// SkillHolder를 할당해줘야한다.
    /// </summary>
    /// 
    public class MyCooldownButton:MyButton
    {
        [Header("SkillHolder 등록시 OnUp 등록 금지")]
        public SkillHolder holder;

        public GameObject CooldownPrefeb;
        
        private void Start()
        {
            onUp.AddListener(holder.Activate);
            GameObject obj = Instantiate(CooldownPrefeb,transform);
            holder.cooldownImg = obj.GetComponent<Image>();
            holder.cooldownImg.fillAmount = 0;
        }

        private void Update()
        {
            
        }
    }
}