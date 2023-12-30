using System.Collections.Generic;
using UnityEngine;


namespace Script.Game.Enemy
{
    
    public class PatternD: Pattern
    {
        public PatternD() : base(4,8)
        {
  
        }


        
        
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            bool isLeft = Random.Range(0, 2) == 1;
 
            for (int i = 0; i < 1; i++)
            {
                var sellected_CC = (Random.Range(0, 2) == 0) ? 출첵L(pf) : 출첵R(pf);
                foreach (var delay in sellected_CC)
                {
                    yield return delay;
                }
		        yield return 0.375f;
            	pf.팀플();
            	yield return 0.375f;
            	pf.팀플();
                yield return 0.375f;
                pf.팀플();
                
                isLeft = !isLeft;
            }
        }
    }
}