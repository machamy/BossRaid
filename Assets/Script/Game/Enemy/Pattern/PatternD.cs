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
            
            for (int i = 0; i < 1; i++)
            {
                if (isLeft)
                {
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
            	pf.팀플();
            	yield return 0.375f;
            	pf.팀플();
                yield return 0.375f;
                pf.팀플();
                
                isLeft = !isLeft;
            }

            /*pf.출쳌(isLeft);
            pf.팀플(dir);
            pf.팀플(dir);
            isLeft = !isLeft;
            dir = Vector2.left;
            pf.tpRight();
            pf.출쳌(isLeft);
            pf.팀플(dir);
            pf.팀플(dir);
            */

            yield return 1.0f;
        }
    }
}