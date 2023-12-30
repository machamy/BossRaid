using Script.Game.Projectile;
using UnityEngine;

namespace Script.Game.Player
{
    [CreateAssetMenu]
    public class BarrierSkill : BaseSkill
    {
        public override void OnBeginActivate(Player p)
        {
            p.IsBarriering = true;
        }

        public override void OnEndAcitavte(Player p)
        {
            p.IsBarriering = false;
        }
    }
}