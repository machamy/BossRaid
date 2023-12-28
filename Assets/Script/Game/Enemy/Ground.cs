using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    public class Ground : MonoBehaviour
    {
        public GameObject fire;
        public float minX = -5f;    
        public float maxX = 5f; 
        public float Posinterval = 1.5f;
        public float Quizdelay = 1.0f;
        public GameObject player;


        //스폰 위치 인자로 받아 씀
        public void SpawnFireWall(Vector2 position)
        {
            GameObject fireWall = Instantiate(fire, position, Quaternion.identity);
        }

        //패턴C 이동제한용 불기둥
        IEnumerator TestSpawn(float minX, float maxX)
        {
            while (true)
            {
                for (float x = minX; x <= maxX; x += Posinterval)
                {
                    Vector2 spawnPosition = new Vector2(x, transform.position.y);
                    SpawnByPosition(spawnPosition,fire);
                }
                yield return new WaitForSeconds(1.5f);
                break;
            }
        }

        //패턴E
        public void 연속퀴즈(Vector2 firePos)
        {
            //StartCoroutine(SpawnSequentially(StartPosx,EndPosx));
            SpawnByPosition(firePos,fire);
        }

        //패턴C
        public void 랜덤퀴즈(float StartPosx, float EndPosx)
        {
            StartCoroutine(TestSpawn(StartPosx,EndPosx));
        }

        //패턴F
        public void 좌우퀴즈()
        {
            Vector2 playerPos = new Vector2(player.transform.position.x, transform.position.y);
            TestInterval(playerPos);
        }

        //기본 퀴즈 공격
        public void 퀴즈()
        {
            Vector2 playerPos = new Vector2(player.transform.position.x, transform.position.y);
            SpawnByPosition(playerPos,fire);
        }

        //Position 좌표로 받아서 불기둥 스폰
        public FireWall SpawnByPosition(Vector2 position, GameObject prefeb)
        {
            GameObject fire = Instantiate(prefeb);
            FireWall firewall = fire.GetComponent<FireWall>();
            return SpawnFireWall(firewall, position);
        }

        public FireWall SpawnFireWall(FireWall fw, Vector2 position)
        {
            fw.transform.position = position;
            return fw;
        }

        public void TestInterval(Vector2 playerPos)
        {
            Vector2 LeftPos = new Vector2(playerPos.x-Posinterval, transform.position.y);
            Vector2 RightPos = new Vector2(playerPos.x+Posinterval, transform.position.y);
            StartCoroutine(SpawnInterval(LeftPos));
            StartCoroutine(SpawnInterval(RightPos));
            StartCoroutine(SpawnInterval(playerPos, Quizdelay));
        }

        private IEnumerator SpawnInterval(Vector2 position, float delay = 0f)
        {
            yield return new WaitForSeconds(delay);
            SpawnByPosition(position, fire);
        }


        void Start()
        {
            //StartCoroutine(TestSpawn());
            //퀴즈();
            //랜덤퀴즈();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
