using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    /// <summary>
    /// 매우 빠른 템포의 3연격
    /// </summary>
    public class PatternC : Pattern
    {
        private Ground gr;
        public PatternC() : base(3)
        {
            gr = GameObject.FindObjectOfType<Ground>();
        }
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            bool isLeft = Random.Range(0,2) == 1;
            //gr.퀴즈();
            
            //출첵 이후 교수위치가 Left
            if (isLeft) 
            {
                pf.Facing = Direction.Left;
                pf.tpRight();
                yield return 0.375f;
                pf.tpRightUp(); //공중에 위치
                //gr.랜덤퀴즈(RightPosStart,RightPosEnd) 오른쪽 절반
                pf.출첵(LeftPosStart,LeftPosEnd,AttendAmount);
                yield return 0.5f;
                pf.Facing = Direction.Right;
                pf.tpLeft();
                //gr.랜덤퀴즈(LeftPosStart,LeftPosEnd)
            }
            else
            {
                pf.Facing = Direction.Right;
                pf.tpLeft();
                yield return 0.375f;
                pf.tpLeftUp();
                //gr.랜덤퀴즈(LeftPosStart,LeftPosEnd)
                pf.출첵(RightPosStart,RightPosEnd,AttendAmount);
                yield return 0.5f;
                pf.Facing = Direction.Left;
                pf.tpRight();
                //gr.랜덤퀴즈(RightPosStart,RightPosEnd)
            }
            yield return 0.5f;
            pf.과제();
            yield return 0.175f;
            pf.과제();
            
            yield return 1.0f;
        }
    }
}