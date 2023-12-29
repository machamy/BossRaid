using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    /// <summary>
    /// 플레이어 좌우에 한번 , 플레이어 위치에 한번  수직공격
    /// </summary>
    public class PatternF : Pattern
    {
        private Ground gr;
        
        public PatternF() : base(2)
        {
            gr = GameObject.FindObjectOfType<Ground>();
        }
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            pf.tpRight();
            yield return pf.teleport_time;
            gr.좌우퀴즈();
        }
    }
}