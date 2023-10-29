using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    /// <summary>
    /// 매우 빠른 템포의 3연격
    /// </summary>
    public class PatternC : Pattern
    {
        public PatternC() : base(8)
        {
  
        }
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            bool isLeft = Random.Range(0,2) == 1;
            Vector2 dir;
            
            //출첵 이후 교수위치가 Left
            if (isLeft) 
            {
                //dir = Vector2.right;
                //pf.tpLeft();
                pf.facing = Direction.Left;
                pf.tpRight();
                yield return 0.2f;
                pf.tpRightUp();
                pf.출첵();
                yield return 0.2f;
                pf.facing = Direction.Right;
                pf.tpLeft();
            }
            else
            {
                //dir =Vector2.left;
                //pf.tpRight();
                pf.facing = Direction.Right;
                pf.tpLeft();
                yield return 0.2f;
                pf.tpLeftUp();
                pf.출첵();
                yield return 0.2f;
                pf.facing = Direction.Left;
                pf.tpRight();
            }
            yield return 0.375f;

        
            //pf.출첵(isLeft)
            pf.과제();
            yield return 0.375f;
            pf.과제();
            
            yield return 1.0f;
        }
    }
}