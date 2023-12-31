﻿using Script.Game.Projectile;
using UnityEngine;

namespace Script.Game.Player
{
    [CreateAssetMenu]
    public class BarrierSkill : BaseSkill
    {
        
        public override void OnUse(Player p)
        {
            base.OnUse(p);
            SoundManager.Instance.Play("Effect/Barrier");
        }

        public override void OnBeginActivate(Player p)
        {
            base.OnBeginActivate(p);
            p.IsBarriering = true;
        }

        public override void OnEndAcitavte(Player p)
        {
            base.OnEndAcitavte(p);
            p.IsBarriering = false;
        }
    }
}