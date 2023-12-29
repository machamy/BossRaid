using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    public class Pattern
    {
        private int actionIdx;
        public int MAX_ACTION_IDX { get; private set;}
        
        protected int AttendAmount = 3;
        protected Vector2 LeftPosStart = new Vector2(-7, -1);
        protected Vector2 LeftPosEnd = new Vector2(0, -1);
        protected Vector2 RightPosStart = new Vector2(0, -1);
        protected Vector2 RightPosEnd = new Vector2(7, -1);
        public Pattern(int maxActionIdx)
        {
            MAX_ACTION_IDX = maxActionIdx;
        }
        /// <summary>
        /// 다음 행동을 실행하고 대기 시간을 반환한다.
        /// </summary>
        /// <returns>대기시간(딜레이)</returns>
        public virtual IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            yield return 1.0f;
        }

        protected IEnumerable<float> 출첵L(Professor pf)
        {
            pf.tpRight();
            yield return pf.teleport_time + 0.3f;
            pf.tpRightUp();
            yield return pf.teleport_time + 0.3f;
            pf.출첵(LeftPosStart,LeftPosEnd,AttendAmount);
            yield return 0.5f;
            pf.tpLeft();
            yield return pf.teleport_time;
        }

        protected IEnumerable<float> 출첵R(Professor pf)
        {
            pf.tpLeft();
            yield return pf.teleport_time + 0.3f;
            pf.tpLeftUp();
            yield return pf.teleport_time + 0.3f;
            pf.출첵(RightPosStart,RightPosEnd,AttendAmount);
            yield return 0.5f;
            pf.tpRight();
            yield return pf.teleport_time;
        }
    }
    
}