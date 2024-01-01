using System.Collections;
using System.Collections.Generic;
using Script.Game.Enemy.Projectile;
using Script.Global;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Game.Enemy
{
    public class FireWall : BaseProjectile
    {

        /// <summary>
        /// 지속시간
        /// </summary>
        public float lifetime;
        private bool isFire;
        [FormerlySerializedAs("previewtime")] public float warnTime;
        public float previewSpeed;
        [SerializeField]
        private Animator Animator;
        private bool isDamaged;

        public bool IsDamaged
        {
            get => isDamaged;
            set => isDamaged = value;
        }
    
        
        public bool IsFire
        {
            get => isFire;
            set
            {
                isFire = value;
                
                Animator.SetBool("IsFire", value);
            }
        }

        public bool IsPreview
        {
            get => IsPreview;
            set
            {
                Animator.SetBool("IsPreview", value);
            }
        }

        public void Start()
        {
            Animator = GetComponent<Animator>();
            Debug.Log(Animator);
            StartCoroutine(WaitPreview(warnTime));
        }


        public override void OnSummon()
        {

        }


        // FireEnd 애니메이션 종료시 자동 실행
        public override void Remove()
        {
            Destroy(gameObject);
        }

        public override void OnHit(Player.Player p)
        {
            if(!IsFire || IsDamaged)
                return;
            p.HP -= damage;
            IsDamaged = true;
        }

        // //좌표 인자로 받아서 update
        // public void UpdateFire()
        // {
        //     if (IsMove)
        //     {
        //         transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
        //         if (transform.position.y >= 0.5f)
        //         {
        //             IsMove = false;
        //             StartCoroutine(RemoveDelay(1.0f));
        //         }
        //     }
        // }

        /// <summary>
        /// StartFire 종료시 실행
        /// </summary>
        public void OnFire()
        {
            if(sound != null)
                SoundManager.Instance.Play(sound);
            IsFire = true;
            StartCoroutine(DelayRemove(lifetime));
        }

        private IEnumerator WaitPreview(float time)
        {
            yield return new WaitForSeconds(warnTime);
            IsPreview = false;
        }
        
        private IEnumerator DelayRemove(float delay)
        {
            yield return new WaitForSeconds(delay);
            IsFire = false;
        }
    }
}
