using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

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

        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            bool isLeft = Random.Range(0,2) == 1;
            Vector2 dir;
            pf.출첵();
            yield break;
            if (isLeft)
            {
                pf.facing = Direction.Right;
                pf.tpLeft();
            }
            else
            {
                pf.facing = Direction.Left;
                pf.tpRight();
            }
            yield return 0.375f;
            
            
            pf.과제();
            yield return 0.375f;
            pf.팀플();

            yield return 0.5f;
        }
    }
}