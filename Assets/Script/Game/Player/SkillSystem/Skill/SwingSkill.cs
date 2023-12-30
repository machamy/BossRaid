using Script.Game.Projectile;
using UnityEngine;

namespace Script.Game.Player
{
    [CreateAssetMenu]
    public class SwingSkill : BaseSkill
    {
        public override void OnBeginActivate(Player p)
        {
            base.OnBeginActivate(p);
            p.IsSwinging = true;
        }

        public override void OnEndAcitavte(Player p)
        {
            base.OnEndAcitavte(p);
            p.IsSwinging = false;
        }
    }
}