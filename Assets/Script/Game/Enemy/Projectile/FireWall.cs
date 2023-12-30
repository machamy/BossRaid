using System.Collections;
using System.Collections.Generic;
using Script.Global;
using UnityEngine;

namespace Script.Game.Enemy
{
    public class FireWall : MonoBehaviour, DBUser
    {
        public float speed = 15f;
        public float lifetime;
        public int damage;
        private bool IsMove = true;
        private bool IsDamage = false;

        private void OnTriggerEnter2D(Collider2D one)
        {
            if (one.CompareTag("Player"))
            {
                IsDamage = true;
                Player.Player player = one.GetComponent<Player.Player>();
                if(IsDamage)
                {
                    OnHit(player);
                    IsDamage = false;
                }
            }
        }

        public virtual void OnHit(Player.Player p)
        {
            p.HP -= damage;
        }

        //좌표 인자로 받아서 update
        public void UpdateFire()
        {
            if (IsMove)
            {
                transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
                if (transform.position.y >= 0.5f)
                {
                    IsMove = false;
                    StartCoroutine(RemoveDelay(1.0f));
                }
            }
        }

        private IEnumerator RemoveDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Remove();
        }

        private void Remove()
        {
            Destroy(gameObject);
        }


        void Start()
        {
            ApplyDBdata();
        }

        public void ApplyDBdata()
        {
            if (DB.FireWall != null)
            {
                lifetime = float.Parse(DB.FireWall[0]);
                damage = int.Parse(DB.FireWall[1]);
            }

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            StartCoroutine(RemoveDelay(lifetime));
            //UpdateFire();
        }
    }
}
