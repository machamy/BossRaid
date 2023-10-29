using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game.Player;
using Script.Game.Projectile;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Type = System.Type;

namespace Script.Game.Player
{
    public class Player : MonoBehaviour
    {
        public int DEAFAULT_HP;
        private int hp;
        private int score;
        public UnityEvent<int> OnHPUpdate { get; } = new UnityEvent<int>();
        public UnityEvent<int> OnScoreUpdate { get; } = new UnityEvent<int>();
        public SkillHolder[] SkillHolders;
        
        public ParryingArea parryingAreaFront;
        public ParryingArea parryingAreaAll;
        
        [SerializeField] private Professor Professor;
        
        public int HP
        {
            get => hp;
            set
            {
                if (value < 0)
                    return;
                hp = value;
                OnHPUpdate.Invoke(value);
                if (value == 0)
                {
                    OnDeath();
                }
            }
        }

        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnScoreUpdate.Invoke(score);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            hp = DEAFAULT_HP;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ForceParry()
        {
            Parry(PrjtType.Attend, true);
            Parry(PrjtType.Practice, true);
            Parry(PrjtType.Team, true);
        }
        
        public bool Parry(Projectile.PrjtType type, bool isDirectional = false)
        {
            ParryingArea area = isDirectional ? parryingAreaFront : parryingAreaAll;
            if (!parryingAreaFront.Parryable) return false;
            foreach (var prjt in area.PopAll(type))
            {
                prjt.OnParring(this);
            }
            return true;
        }





        private void OnDeath()
        {
           gameObject.SetActive(false);
        }

        /*
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("??");
            Debug.Log((other.transform.tag));
            if (other.transform.tag == "Projectile")
            {

                Debug.Log("??");
                Projectile prj = other.transform.GetComponent<Projectile>();
                prj.OnHit(this);
            }
        }
        */
    }
}