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
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Type = System.Type;

namespace Script.Game.Player
{
    public class Player : MonoBehaviour, DBUser
    {
        public int DEAFAULT_HP;
        public float INVICIBLE_TIME;
        private int hp;
        private int score;
        private bool isAlive;
        private bool isInvincible;

        private Animator Animator;
        [SerializeField]
        private Animator barrierAnimator;

        private Movement Movement;
        private bool isUsingSkill;
        private bool isBarriering;

        public bool IsUsingSkill
        {
            get => isUsingSkill;
            set
            {
                isUsingSkill = value;
                Animator.SetBool("IsUsingSkill", value);
            }
        }
        
        public bool IsBarriering
        {
            get => isBarriering;
            set
            {
                isBarriering = value;
                barrierAnimator.SetBool("IsBarriering", value);
            }
        }
        public bool IsAlive => isAlive;

        public bool IsMoving => Movement.isMoving;
        public bool CanMove
        {
            get => canMove;
            set { Movement.canMove = this.canMove = value; }
        }

        private bool isLeft;
        private bool canMove;

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
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
        
        
        public UnityEvent<int> OnHPUpdateEvent { get; } = new UnityEvent<int>();
        public UnityEvent<int> OnScoreUpdateEvent { get; } = new UnityEvent<int>();
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
                if (isInvincible)
                    return;
                if (value < 0)
                    return;
                hp = value;
                OnHPUpdateEvent.Invoke(value);
                if (value == 0)
                {
                    OnDeath();
                }
                StartCoroutine(InvincibleRoutine());
            }
        }

        /// <summary>
        /// 점수. 변경시 OnScoreUpdateEvent호출
        /// </summary>
        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnScoreUpdateEvent.Invoke(score);
            }
        }

        /// <summary>
        /// hp, isAlive, canMove 초기화
        /// </summary>
        void Start()
        {
            Movement = GetComponent<Movement>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            ApplyDBdata();
            hp = DEAFAULT_HP;
            isAlive = true;
            canMove = true;
        }

        public void ApplyDBdata()
        {
            if (DB.PlayerHP != null)
                DEAFAULT_HP = int.Parse(DB.PlayerHP[0]);
            if (DB.PlayerSpeed != null)
                GetComponent<Movement>().defaultSpeed = float.Parse(DB.PlayerSpeed[0]);
            if (DB.PlayerInvicbleTime != null)
                INVICIBLE_TIME = float.Parse(DB.PlayerInvicbleTime[0]);
        }

        // Update is called once per frame
        void Update()
        {
            Animator.SetBool("IsMoving", IsMoving);
        }

        private void FixedUpdate()
        {
            float border = 5f;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -border, border),
                transform.position.y
                ,transform.position.z);
        }

        public void ForceParry()
        {
            Parry(PrjtType.Attend, true);
            Parry(PrjtType.Practice, true);
            Parry(PrjtType.Team, true);
        }


        public void PlayAnimation(string name, float time)
        {
            float speed = 1.0f / time;
            Animator.SetFloat($"{name}Speed",speed);
            Animator.SetBool("IsUsingSkill", true);
            Animator.SetTrigger(name);
        }
        
        
        /// <summary>
        /// 지정된 type에 따른 투사체를 패링구역에서 패링.
        /// </summary>
        /// <param name="type">패링할 타입</param>
        /// <param name="isDirectional">패링구역 선택. true => front</param>
        /// <returns></returns>
        public bool Parry(Projectile.PrjtType type, bool isDirectional = false)
        {
            if (!IsAlive) return false;
            ParryingArea area = isDirectional ? parryingAreaFront : parryingAreaAll;
            if (!area.Parryable) return false;
            var q = area.PopAll(type);
            while (q.Any())
            {
                Projectile.Projectile prjt = q.Dequeue();
                if (prjt.Type != type)
                    continue;
                Debug.Log(prjt.name + " "+ prjt.Type);
                prjt.OnParring(this);
            }
            return true;
        }

        public IEnumerator InvincibleRoutine()
        {
            Color original = _spriteRenderer.color;
            _spriteRenderer.color = Color.yellow; // 테스트용 색 변경
            isInvincible = true;
            yield return new WaitForSeconds(INVICIBLE_TIME);
            isInvincible = false;
            _spriteRenderer.color = original;
        }
        
        
        /// <summary>
        /// 플레이어가 죽을시 실행되는 리스너
        /// </summary>
        private void OnDeath()
        {
            isAlive = false;
            PlayerPrefs.SetInt("result_hp", hp);
            PlayerPrefs.SetInt("result_score", score);
            SceneManager.LoadScene("Scenes/ResultScreen");
            SoundManager.Instance.Clear();
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