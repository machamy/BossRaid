using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Projectile
{
    public class Attendance : MonoBehaviour
    {
        Transform playerPos;
        Transform professorPos;
        [SerializeField] float delaytime;
        bool isMoving=false;
        //교수 초기 위치
        Vector3 firstPPos;

        
        void Start()
        {
            playerPos = GameObject.Find("Player").GetComponent<Transform>();
            professorPos = GameObject.Find("Professor").GetComponent<Transform>();
            firstPPos = professorPos.position;

            Invoke("StartMoving",delaytime);
        }

        void StartMoving() {
            //순간이동 위치설정
            float xVlaue = Random.Range(-9, 9);
            float yValue = Random.Range(1, 3);
            Vector3 newPosition = new Vector3(xVlaue, yValue, 0);
            professorPos.position = newPosition;
            transform.position = newPosition;

            isMoving = true;
        }
         // Update is called once per frame
        void Update()
        {
            if(isMoving) {
                Vector3 dir = (playerPos.position - transform.position).normalized;
                transform.Translate(dir * Time.deltaTime * 5);
            }

            if (!isMoving) {
                // 기존 위치와 비교하여 변동이 있을 경우 반전.
                if (professorPos.position != firstPPos)
                {
                    professorPos.position = new Vector3(-firstPPos.x, firstPPos.y, firstPPos.z);
                }
            }
        }
    }
}

