﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Game.Enemy
{
    [CreateAssetMenu(fileName = "New phase", menuName = "Phase/Create Phase", order = 0)]
    public class Phase : ScriptableObject
    {
        public int level;
        public int maxScore;
        public int frequency;
        public List<int> ProbablityList;
        [NonSerialized]
        public int sumOfProbalblty;

        private void Awake()
        {
            int sum = 0;
            ProbablityList.ForEach(n => sum += n);
            sumOfProbalblty = sum;
        }

        public int GetNextPatternNum(int PatternAmount)
        {
            // 정해진 페이즈를 초과하면 0번 페이즈
            int random_value = Random.Range(0, sumOfProbalblty);
            for(int i = 0; i <= PatternAmount; i++)
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
    }
    
    
}