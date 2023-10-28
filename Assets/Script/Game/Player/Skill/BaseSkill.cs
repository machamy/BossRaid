using System;
using UnityEngine;

namespace Script.Game.Player
{
    public class BaseSkill
    {
        public int Cooldown{ get; private set; }
        public int Duration{ get; private set; }
        public bool IsDirectional{ get; private set; }
        public Sprite sptire{ get; private set; }

        public virtual void Do()
        {
            
        }
    }
}