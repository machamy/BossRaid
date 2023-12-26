using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.FireWall
{
    public class Ground : MonoBehaviour
    {
        public GameObject Fire;
        public float minX = -5f;    
        public float maxX = 5f;     


        //스폰 위치 인자로 받아 씀
        public void SpawnFireWall(Vector2 position)
        {
            GameObject fireWall = Instantiate(Fire, position, Quaternion.identity);
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
            }
        }


        public void TestFunc()
        {
            Vector2 newPos = new Vector2(transform.position.x-3.5f, transform.position.y - 7.5f);
            SpawnFireWall(newPos);
        }


        void Start()
        {
            //StartCoroutine(TestSpawn());
            //TestFunc();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
