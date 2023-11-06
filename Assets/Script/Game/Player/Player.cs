using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Script.Game.Player;
using Script.Game.Projectile;
using Script.Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Type = System.Type;

namespace Script.Game.Player
{
    public class Player : MonoBehaviour, DBUser
    {
        public int DEAFAULT_HP;
        private int hp;
        private int score;
        private bool isAlive;
        public bool IsAlive => isAlive;
        
        private bool isLeft;
        

        public bool IsLeft
        {
            get => isLeft;
            set
            {
                if (isLeft == value)
                {
                    return;
                }
                isLeft = value;
                if (isLeft)
                {
                    transform.localScale = new Vector3(-1,1,1);
                }
                else
                {
                    transform.localScale = new Vector3(1,1,1);
                }
            }
        }
        
        
        public UnityEvent<int> OnHPUpdate { get; } = new UnityEvent<int>();
        public UnityEvent<int> OnScoreUpdate { get; } = new UnityEvent<int>();
        [Header("Skills")]
        public SkillHolder[] SkillHolders;
        [Header("Parry")]
        public ParryingArea parryingAreaFront;
        public ParryingArea parryingAreaAll;
        [Header("ETC")]
        [SerializeField] private Professor Professor;

        private SpriteRenderer _spriteRenderer;
        
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
            _spriteRenderer = GetComponent<SpriteRenderer>();
            ApplyDBdata();
            hp = DEAFAULT_HP;
            isAlive = true;
            StartCoroutine(TestScore(1));
        }

        public void ApplyDBdata()
        {
            if (DB.PlayerHP != null)
                DEAFAULT_HP = int.Parse(DB.PlayerHP[0]);
            if (DB.PlayerSpeed != null)
                GetComponent<Movement>().defaultSpeed = float.Parse(DB.PlayerSpeed[0]);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        IEnumerator TestScore(int n)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                Score += n;
            }
        }

        public void ForceParry()
        {
            Parry(PrjtType.Attend, true);
            Parry(PrjtType.Practice, true);
            Parry(PrjtType.Team, true);
        }
        
        public bool Parry(Projectile.PrjtType type, bool isDirectional = false)
        {
            if (!IsAlive) return false;
            ParryingArea area = isDirectional ? parryingAreaFront : parryingAreaAll;
            if (!parryingAreaFront.Parryable) return false;
            var q = area.PopAll(type);
            while (q.Any())
            {
                q.Dequeue().OnParring(this);
            }
            return true;
        }
        private void OnDeath()
        {
            isAlive = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.1f);
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