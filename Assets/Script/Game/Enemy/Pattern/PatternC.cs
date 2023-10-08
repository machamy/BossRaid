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
            yield return 0.375f;
            //pf.출쳌(isLeft)
            yield return 0.375f;
            //pf.과제(dir)
            yield return 0.375f;
            //pf.과제(dir)
            
            yield return 1.0f;
        }
    }
}