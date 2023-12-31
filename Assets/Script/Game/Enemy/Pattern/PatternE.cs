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
        private float Startx = 2.5f;
        public float Posinterval;
        
        public PatternE() : base(5,2)
        {
            gr = GameObject.FindObjectOfType<Ground>();
            Posinterval = gr.Posinterval;
        }
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            
            for(float x = Startx; x > -7.0f; x -= Posinterval)
            {
                pf.tpRight();
                yield return pf.teleport_time;
                Vector2 newPos = new Vector2(x, gr.transform.position.y);
                gr.연속퀴즈(newPos);
            }
            
        }
    }
}