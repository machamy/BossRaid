using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.FireWall
{
    public class FireWall : MonoBehaviour
    {
        public float speed = 15f;
        public int damage;
        public float stopHeight = 3f; // 멈출높이
        private bool IsMove = true;
        //private GameObject target;

        public virtual void OnHit(Player.Player p)
        {
            p.HP -= damage;
        }

        //좌표 인자로 받아서 update
        public void UpdateFire()
        {
            if (IsMove)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                if (transform.position.y >= stopHeight)
                {
                    IsMove = false;
                }
            }
        }

        //현재 플레이어의 위치를 받아 불기둥 생성 - 미구현
        public void UpdateTargetFire()
        {

        }
        

        private void Remove()
        {
            Destroy(gameObject);
        }


        // Start is called before the first frame update
        void Start()
        {
            //target = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            UpdateFire();
        }
    }
}
