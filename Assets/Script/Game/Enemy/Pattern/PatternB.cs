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
                    //pf.tpLeft();
                    //pf.TestShoot(Vector2.right);
                }
                else
                {
                    //pf.tpRight();
                    //pf.TestShoot(Vector2.left);
                }
                yield return 0.5f;
                isLeft = !isLeft;
            }
            
            yield return 1.0f;
        }
    }
}