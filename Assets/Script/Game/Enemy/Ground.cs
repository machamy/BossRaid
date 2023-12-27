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
        public float interval = 1.5f;
        public GameObject player;


        //스폰 위치 인자로 받아 씀
        public void SpawnFireWall(Vector2 position)
        {
            GameObject fireWall = Instantiate(fire, position, Quaternion.identity);
        }

    
        IEnumerator TestSpawn()
        {
            while (true)
            {
                for (float x = minX; x <= maxX; x += 3.5f)
                {
                    Vector2 spawnPosition = new Vector2(x, transform.position.y);
                    SpawnFireWall(spawnPosition);
                }
                yield return new WaitForSeconds(1.5f);
                break;
            }
        }

        public void 퀴즈()
        {
            TestQuiz();
        }

        public void 랜덤퀴즈(float StartPosx, float EndPosx)
        {
            TestFunc(6.5f);
        }

        public void 랜덤퀴즈(float x)
        {
            TestFunc(x);
        }

        //Position 받아서 불기둥 스폰되도록
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


        public void TestFunc(float xf)
        {
            Vector2 newPos = new Vector2(transform.position.x-xf, transform.position.y);
            SpawnByPosition(newPos, fire);
        }

        public void TestQuiz()
        {
            Vector2 playerPos = new Vector2(player.transform.position.x, transform.position.y);
            SpawnByPosition(playerPos, fire);
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
