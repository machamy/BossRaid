using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    public class Pattern
    {
        private int actionIdx;
        public int MAX_ACTION_IDX { get; private set;}

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
        
    }
}