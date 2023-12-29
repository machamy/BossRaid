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
            //Vector2 dir;
            //pf.출첵();
            
            if (isLeft)
            {
                pf.tpLeft();
            }
            else
            {
                pf.tpRight();
            }
            yield return pf.teleport_time;
            
            
            pf.과제();
            yield return 0.5f;
            pf.과제();
            yield return 0.5f;
            pf.과제();

            yield return 0.5f;
        }
    }
}