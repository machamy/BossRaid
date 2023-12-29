using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    /// <summary>
    /// 플레이어 좌우에 한번 , 플레이어 위치에 한번  수직공격
    /// </summary>
    public class PatternE : Pattern
    {
        private Ground gr;
        private float Startx = 3.0f;
        
        public PatternE() : base(2)
        {
            gr = GameObject.FindObjectOfType<Ground>();
        }
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            for(float x = Startx; x > -7.0f; x -= 1.5f)
            {
                pf.tpRight();
                yield return pf.teleport_time;
                Vector2 newPos = new Vector2(x, 0.5f);
                gr.연속퀴즈(newPos);
            }
    
            yield return 1.0f;
        }
    }
}