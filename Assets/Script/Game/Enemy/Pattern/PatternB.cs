using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{   
    /// <summary>
    /// 느린 템포의 4연격
    /// </summary>
    public class PatternB : Pattern
    {
        public PatternB() : base(4)
        {
  
        }
        
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            bool isLeft = Random.Range(0,2) == 1;
            
            for (int i = 0; i < 4; i++)
            {
                if (isLeft)
                {
                    pf.Facing = Direction.Right;
                    pf.tpLeft();
                    pf.과제();
                    yield return 0.5f;
                }
                else
                {
                    pf.Facing = Direction.Left;
                    pf.tpRight();
                    pf.과제();
                    yield return 0.5f;
                }
                yield return 0.5f;
                isLeft = !isLeft;
            }
            
            yield return 1.0f;
        }
    }
}