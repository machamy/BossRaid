using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Game.Enemy
{
    /// <summary>
    /// 패턴 관리 스크립트
    /// </summary>
    public class PatternController: MonoBehaviour
    {
        /// <summary>
        /// 패턴 목록
        /// </summary>
        private List<Pattern> Patterns;
        [SerializeField] private List<Phase> Phases;
        [SerializeField] private int PATTERN_NUM;
        public int PhaseLv { get; set; }
        

        private void Awake()
        {
            PhaseLv = 0;
            Patterns = new List<Pattern>();
            Patterns.Add(new PatternA());
            Patterns.Add(new PatternB());
            Patterns.Add(new PatternC());
            Patterns.Add(new PatternD());
        }

        public IEnumerator Rountine(Professor pf, Player.Player p)
        {
            yield return new WaitForSeconds(2);
            PhaseLv += 1;
            while (true)
            {
                Phase currentPhase = Phases[PhaseLv];
                Pattern pattern = Patterns[currentPhase.GetNextPatternNum()];
                Debug.Log("Sellected Pattern : " + pattern.GetType());

                foreach (float delay in pattern.NextAction(pf,p))
                {
                    /// 패턴간의 딜레이
                    yield return new WaitForSeconds(delay);
                }
                // 페이즈간의 딜레이
                yield return new WaitForSecondsRealtime(currentPhase.frequency);
            }
        }

        public void OnScoreUpdate(int score)
        {
            if (score > Phases[PhaseLv].maxScore)
            {
                PhaseLv += 1;
            }
        }

        // /// <summary>
        // /// 처음 패턴 확률 초기화.
        // /// </summary>
        // /// <param name="probablities"></param>
        // /// <returns></returns>
        // public PatternController InitNextPhase(int maxScore,float frequency,params int[] probablities)
        // {
        //     var list = new List<int>(probablities);
        //     int sum = 0;
        //     Array.ForEach(probablities,(n => sum += n));
        //     while(list.Count <= PATTERN_NUM)
        //         list.Add(0);
        //     Probablity2DList.Add(list);
        //     SumOfProbablity.Add(sum);
        //     
        //     FrequencyList.Add(frequency);
        //     MaxScoreList.Add(maxScore);
        //     return this;
        // }
        
        // public Pattern GetNextPattern(int phase)
        // {
        //     List<int> probablities = Probablity2DList[phase];
        //     // 정해진 페이즈를 초과하면 0번 페이즈
        //     if (phase > Probablity2DList.Count)
        //         return GetNextPattern(0);
        //     int random_value = Random.Range(0, SumOfProbablity[phase]);
        //     for(int i = 0; i <= PATTERN_NUM; i++)
        //     {
        //         int next_probablities = probablities[i];
        //         if (random_value < next_probablities)
        //         {
        //             return Patterns[i];
        //         }
        //         random_value -= next_probablities;
        //     }
        //     Debug.LogError("[PatternController::GetNextPattern] 알고리즘 에러");
        //     return new Pattern(0);
        // }
    }
}