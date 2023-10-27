using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Projectile
{   
    public class Teamproject : Projectile
    {
        Transform playerPos;
        [SerializeField] float delaytime;
        bool isMoving = false;
        // Start is called before the first frame update
        void Start()
        {
            playerPos = GameObject.Find("Player").GetComponent<Transform>();
            Invoke("StartMoving", delaytime);
        }

        void StartMoving() {
            isMoving = true;
        }
        // Update is called once per frame
        void Update()
        {
            if (isMoving) {
                Vector3 dir = (playerPos.position - transform.position).normalized;
                transform.Translate(dir * Time.deltaTime * 5);
            }   
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.tag == "Player") {
                Destroy(gameObject);
            }
        }
    }
}

