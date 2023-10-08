using System.Collections.Generic;
using UnityEngine;


namespace Script.Game.Enemy
{
    
    public class PatternD: Pattern
    {
        public PatternD() : base(2)
        {
  
        }
        
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            bool isLeft = Random.Range(0, 2) == 1;
            Vector2 dir = Vector2.right;

            //pf.출쳌(isLeft);
            //pf.과제(dir);
            //pf.과제(dir);
            isLeft = !isLeft;
            dir = Vector2.left;
            //pf.tpRight();
            //pf.출쳌(isLeft);
            //pf.과제(dir);
            //pf.과제(dir);
            
            yield return 1.0f;
        }
    }
}