using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Script.Game.Enemy.Projectile;
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
        [SerializeField] private int hp;
        [SerializeField] private int score;
        private bool isAlive;
        private bool isInvincible;

        private Animator Animator;
        [SerializeField] private Animator barrierAnimator;
        [SerializeField] private Animator shieldAnimator;
        [SerializeField] private Animator swingAnimator;

        private Movement Movement;
        private bool isUsingSkill;
        private bool isBarriering;
        private bool isShielding;
        private bool isSwinging;


        public bool IsDashing => Movement.isDashing;

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

        public bool IsShielding
        {
            get => isShielding;
            set
            {
                isShielding = value;
                shieldAnimator.SetBool("IsShielding", value);
            }
        }

        public bool IsSwinging
        {
            get => isSwinging;
            set
            {
                isSwinging = value;
                swingAnimator.SetBool("IsSwinging", value);
            }
        }

        public bool IsAlive => isAlive;

        public bool IsMoving => Movement.isMoving;

        public bool CanMove
        {
            get => canMove;
            set { canMove = value; }
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
        [Header("Skills")] public SkillHolder[] SkillHolders;
        [Header("Parry")] public ParryingArea parryingAreaFront;
        public ParryingArea parryingAreaAll;
        [Header("ETC")] [SerializeField] private Professor Professor;

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
                
                
                if (value < hp)
                {
                    OnHurt(value);
                }
                hp = value;
                OnHPUpdateEvent.Invoke(value);
                // 부상시
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
            Animator.SetBool("IsDashing", IsDashing);
        }

        private void FixedUpdate()
        {
            float border = 5f;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -border, border),
                transform.position.y
                , transform.position.z);
        }



        public void ForceParry()
        {
            Parry(PrjtType.Attend, true);
            Parry(PrjtType.Practice, true);
            Parry(PrjtType.Team, true);
        }


        public void PlaySkillAnimation(string name, float time)
        {
            float speed = 1.0f / time;
            Animator.SetFloat($"{name}Speed", speed);
            Animator.SetBool("IsUsingSkill", true);
            Animator.SetTrigger(name);
        }


        /// <summary>
        /// 지정된 type에 따른 투사체를 패링구역에서 패링.
        /// </summary>
        /// <param name="type">패링할 타입</param>
        /// <param name="isDirectional">패링구역 선택. true => front</param>
        /// <returns></returns>
        public int Parry(PrjtType type, bool isDirectional = false)
        {
            if (!IsAlive) return 0;
            ParryingArea area = isDirectional ? parryingAreaFront : parryingAreaAll;
            if (!area.Parryable) return 0;
            var q = area.PopAll(type);
            int count = 0;
            while (q.Any())
            {
                BaseProjectile prjt = q.Dequeue();
                if (prjt.Type != type || !prjt.IsParryable)
                    continue;
                Debug.Log(prjt.name + " " + prjt.Type);
                count++;
                prjt.OnParring(this);
            }

            return count;
        }

        public IEnumerator InvincibleRoutine() => InvincibleRoutine(INVICIBLE_TIME);

        public IEnumerator InvincibleRoutine(float time)
        {
            Color original = _spriteRenderer.color;
            Color tmp = _spriteRenderer.color;
            tmp.a = 0.5f; // 테스트용 색 변경
            _spriteRenderer.color = tmp;
            isInvincible = true;
            yield return new WaitForSeconds(time);
            isInvincible = false;
            _spriteRenderer.color = original;
        }

        /// <summary>
        /// 체력 감소시
        /// </summary>
        internal void OnHurt(int hp)
        {
            if (hp == 0)
            {
                OnDeath();
            }

            SoundManager.Instance.Play("Effect/Injured");
            StartCoroutine(InvincibleRoutine());
        }

        /// <summary>
        /// 플레이어가 죽을시 실행되는 리스너
        /// </summary>
        private void OnDeath()
        {
            isAlive = false;
            GoResult();
        }


        public void OnClear()
        {
            GoResult();
        }


        private void GoResult()
        {
            PlayerPrefs.SetInt("result_hp", hp);
            PlayerPrefs.SetInt("result_score", score);
            SceneManager.LoadScene("Scenes/ResultScreen");
            SoundManager.Instance.Clear();
        }


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
