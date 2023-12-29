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
                    pf.tpLeft();
                    yield return pf.teleport_time;
                    pf.과제();
                    yield return 0.5f;
                }
                else
                {
                    pf.tpRight();
                    yield return pf.teleport_time;
                    pf.과제();
                    yield return 0.5f;
                }
                yield return 0.5f;
                isLeft = !isLeft;
            }
        }
    }
}