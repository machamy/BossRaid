using System;
using System.Collections.Generic;
using Script.Global;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Script.Game.Enemy
{
    [CreateAssetMenu(fileName = "New phase", menuName = "Phase/Create Phase", order = 0)]
    public class Phase : ScriptableObject
    {
        public int level;
        public int maxScore;
        public float frequency;
        public List<int> ProbablityList;
        [NonSerialized]
        public int sumOfProbability;

        public string Name => $"Phase{level}";

        private void Awake()
        {
            var sum = 0;
            ProbablityList.ForEach(n => sum += n);
            sumOfProbability = sum;
        }
        
        /// <summary>
        /// 확률에 따른 다음 패턴을 가져옴
        /// </summary>
        /// <returns>다음 패턴의 번호</returns>
        /// <exception cref="Exception">sumOfProbability가 잘못됨</exception>
        public int GetNextPatternNum()
        {
            //timeScale == 0일 때 패턴 선택 제한
            if(Time.timeScale == 0f)
            {
                return 0;
            }

            int random_value = Random.Range(0, sumOfProbability);
            Debug.Log($"[Phase::GetNextPatternNum] val = {random_value}");
            for(int i = 0; i < ProbablityList.Count; i++)
            {
                int next_probablities = ProbablityList[i];
                if (random_value < next_probablities)
                {
                    return i;
                }
                random_value -= next_probablities;
            }
            
            throw new Exception("[Phase::GetNextPattern] 알고리즘 에러");
            return 0;
        }

        public int initSumOfProbablity()
        {
            int sum = 0;
            ProbablityList.ForEach(n => sum += n);
            sumOfProbability = sum;
            return sum;
        }


    }
    
    
    
}