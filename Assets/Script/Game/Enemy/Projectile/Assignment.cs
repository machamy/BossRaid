using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Projectile
{
    public class Assignment : Projectile
    {

        Transform playerPos;

        // Start is called before the first frame update
        void Start()
        {
            playerPos = GameObject.Find("Player").GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 dir = (playerPos.position- transform.position).normalized;
            transform.Translate(dir*Time.deltaTime*5);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.tag == "Player") {
                Destroy(gameObject);
            }
        }
    }


}

