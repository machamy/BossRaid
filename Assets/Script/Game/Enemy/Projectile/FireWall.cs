using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    public class FireWall : MonoBehaviour
    {
        public float speed = 15f;
        public int damage;
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


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 prjtPoint = Camera.main.WorldToScreenPoint(transform.position);
            if(prjtPoint.y<0 || prjtPoint.y >Screen.height)
                IsMove = false;

            UpdateFire();
        }
    }
}
