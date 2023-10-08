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
            
            if (isLeft)
            {
                dir = Vector2.right;
                //pf.tpLeft();
            }
            else
            {
                dir =Vector2.left;
                //pf.tpRight();
            }
            
            //pf.과제(dir)
            //pf.팀플(dir)
            pf.TestShoot(dir); 
            pf.TestShoot(dir);
            
            yield return 1.0f;
        }
    }
}