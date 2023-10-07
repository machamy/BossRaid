using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Game.Enemy
{
    public class PatternController:ScriptableObject
    {
        /// <summary>
        /// 패턴 목록
        /// </summary>
        private List<Pattern> Patterns;
        /// <summary>
        /// 가승성 리스트
        /// </summary>
        public List<List<int>>  Probablity2DList;
        /// <summary>
        /// 내부 확률 계산용 리스트
        /// </summary>
        private List<int> SumOfProbablity;

        private int PATTERN_NUM;
        
        public void Awake()
        {
            SetNextProbablity(0,0,0,0);
        }
        
        /// <summary>
        /// 처음 패턴 확률 초기화.
        /// </summary>
        /// <param name="probablities"></param>
        /// <returns></returns>
        public PatternController SetNextProbablity(params int[] probablities)
        {
            var list = new List<int>(probablities);
            int sum = 0;
            Array.ForEach(probablities,(n => sum += n));
            while(list.Count <= PATTERN_NUM)
                list.Add(0);
            Probablity2DList.Add(list);
            SumOfProbablity.Add(sum);
            return this;
        }


        public Pattern GetNextPattern(int phase)
        {
            List<int> probablities = Probablity2DList[phase];
            // 정해진 페이즈를 초과하면 0번 페이즈
            if (phase > Probablity2DList.Count)
                return GetNextPattern(0);
            int random_value = Random.Range(0, SumOfProbablity[phase]);
            for(int i = 0; i <= PATTERN_NUM; i++)
            {
                int next_probablities = probablities[i];
                if (random_value < next_probablities)
                {
                    return Patterns[i];
                }
                random_value -= next_probablities;
            }
            Debug.LogError("[PatternController::GetNextPattern] 알고리즘 에러");
            return new Pattern(0);
        }
    }
}