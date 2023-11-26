using System.Collections.Generic;
using UnityEngine;


namespace Script.Game.Enemy
{
    
    public class PatternD: Pattern
    {
        public PatternD() : base(8)
        {
  
        }

        private int AttendAmount;
        private Vector2 LeftPosStart = new Vector2(-10, -1);
        private Vector2 LeftPosEnd= new Vector2(0, -1);
        private Vector2 RightPosStart = new Vector2(0, -1);
        private Vector2 RightPosEnd = new Vector2(-10, -1);
        
        
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            bool isLeft = Random.Range(0, 2) == 1;
 
            for (int i = 0; i < 1; i++)
            {
                if (isLeft)
                {
                    pf.facing = Direction.Left;
                    pf.tpRight();
                    yield return 0.375f;
                    pf.tpRightUp();
                    pf.출첵(LeftPosStart,LeftPosEnd,AttendAmount);
                    yield return 0.5f;
                    pf.facing = Direction.Right;
                    pf.tpLeft();
                }
                else
                {
                    pf.facing = Direction.Right;
                    pf.tpLeft();
                    yield return 0.375f;
                    pf.tpLeftUp();
                    pf.출첵(RightPosStart,RightPosEnd,AttendAmount);
                    yield return 0.5f;
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
            yield return 1.0f;
        }
    }
}