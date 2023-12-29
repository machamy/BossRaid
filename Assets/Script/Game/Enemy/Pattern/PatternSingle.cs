using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Game.Enemy
{
    
    /// <summary>
    /// 단일 공격
    /// 4가지 공격중 하나 선택해 공격한다.
    /// </summary>
    public class PatternSingle : Pattern
    {
        private Ground gr;
        
        
        public PatternSingle() : base(2)
        {
            gr = GameObject.FindObjectOfType<Ground>();
            
        }
        public override IEnumerable<float> NextAction(Professor pf, Player.Player p)
        {
            int atk = GetNextAttack();
            switch (atk)
            {
                case 0:
                    pf.과제();
                    break;
                case 1:
                    pf.팀플();
                    break;
                case 2:
                    foreach (var delay in (Random.Range(0,2) == 0) ? 출첵L(pf) : 출첵R(pf))
                     {
                         yield return delay;
                     }
                    break;
                case 3:
                    gr.퀴즈();
                    break;
            }
            Debug.Log($"[PatternSingle::NextAction] : {atk}(과제,팀플,출첵,퀴즈)");
            
            yield return 0.0f;
        }

        private int MAX_PROBABILTY = 100;
        private int[] ProbablityList = {25,25,25,25};
        private int previous_attack = -1;
        
        /// <summary>
        /// 무작위 공격 받아오기
        /// 이전에 한 공격은 확률이 낮아짐
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private int GetNextAttack()
        {
            int random_value = Random.Range(0, MAX_PROBABILTY);
            for(int i = 0; i < ProbablityList.Length; i++)
            {
                int next_probablities = ProbablityList[i];
                if (random_value < next_probablities)
                {
                    // 이전 공격과 다르면 초기화
                    if (previous_attack != i)
                    {
                        for (var i1 = 0; i1 < ProbablityList.Length; i1++)
                            ProbablityList[i1] = 25;
                    }
                    else
                    {
                        ProbablityList[i] = Math.Max(ProbablityList[i] - 10, 0);
                    }
                    return i;
                }
                random_value -= next_probablities;
            }
            
            throw new Exception("[PatternSingle::GetNextAttack] 알고리즘 에러");
            return 0;
        }
    }
}