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

        private void Remove()
        {
            Destroy(gameObject);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateFire();
        }
    }
}
