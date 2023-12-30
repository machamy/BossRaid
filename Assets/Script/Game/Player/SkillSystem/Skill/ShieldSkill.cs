using Script.Game.Projectile;
using UnityEngine;

namespace Script.Game.Player
{
    [CreateAssetMenu]
    public class ShieldSkill : BaseSkill
    {
        public override void OnBeginActivate(Player p)
        {
            base.OnBeginActivate(p);
            p.IsShielding = true;
        }

        public override void OnEndAcitavte(Player p)
        {
            base.OnEndAcitavte(p);
            p.IsShielding = false;
        }
    }
}