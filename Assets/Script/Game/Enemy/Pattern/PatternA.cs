using System.Collections.Generic;

namespace Script.Game.Enemy
{
    /// <summary>
    /// 간단한 2연격
    /// </summary>
    public class PatternA : Pattern
    {
        public PatternA() : base(2)
        {
        }

        public virtual IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            yield return 1.0f;
        }
    }
}