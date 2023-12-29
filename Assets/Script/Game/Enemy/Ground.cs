using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Enemy
{
    public class Ground : MonoBehaviour
    {
        public GameObject fire;
        public GameObject firePreview;
        public float previewDelay = 1.0f;
        public float Posinterval = 1.5f;
        public float Quizdelay = 1.0f;  //좌우 공격 시 딜레이
        public GameObject player;




        //패턴C 이동제한용 불기둥
        IEnumerator TestSpawn(float minX, float maxX)
        {
            while (true)
            {
                for (float x = minX; x < maxX; x += Posinterval)
                {
                    Vector2 spawnPosition = new Vector2(x, transform.position.y);
                    //SpawnByPosition(spawnPosition,fire);
                    StartCoroutine(SpawnByPosition(spawnPosition, fire));
                }
                yield return new WaitForSeconds(1.5f);
                break;
            }
        }

        //패턴E
        public void 연속퀴즈(Vector2 firePos)
        {
            SpawnByPosition(firePos,fire);
            StartCoroutine(SpawnByPosition(firePos, fire));
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
            StartCoroutine(SpawnByPosition(playerPos, fire));
        }

        //Position 좌표로 받아서 불기둥 스폰
        public IEnumerator SpawnByPosition(Vector2 position, GameObject prefeb)
        {
            // 미리보기용 공격 범위 표시
            showPreview(position);

            // 딜레이 후 실제 공격 발동
            yield return StartCoroutine(realAttack(position, prefeb));

            // 선딜레이 1.0f 후 공격 범위 OFF
            StartCoroutine(hidePreview(1.5f));
        }

        IEnumerator realAttack(Vector2 pos, GameObject prefeb)
        {
            yield return new WaitForSeconds(previewDelay);

            GameObject fire = Instantiate(prefeb);
            FireWall firewall = fire.GetComponent<FireWall>();
            firewall.transform.position = pos;

        }

        IEnumerator hidePreview(float delay)
        {
            yield return new WaitForSeconds(delay);
            firePreview.SetActive(false);
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
            StartCoroutine(SpawnByPosition(position, fire));
        }

        public void showPreview(Vector2 position)
        {
            Instantiate(firePreview, position, Quaternion.identity);
            firePreview.transform.position = position;
            firePreview.SetActive(true);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
