using System;
using System.Collections;
using Script.Global;
using TMPro;
using UnityEngine;
using System.Linq;

namespace Script.Game.Enemy.Projectile
{
    public class Display: MonoBehaviour, DBUser
    {
        private TextMeshPro textMesh;
        
        [SerializeField]
        private Color[] saColors;
        [SerializeField]
        private Color[] pColors;

        [SerializeField]
        private float saDisplayTime;
        [SerializeField]
        private float saFadeoutTime;
        [SerializeField]
        private float pDisplayTime;
        [SerializeField]
        private float pFadeoutTime;
        [SerializeField]
        private string[] saNames;
        [SerializeField]
        private string[] pNames;

        private float remainingTime;
        
        public void Start()
        {
            textMesh = GetComponent<TextMeshPro>();
            textMesh.text = "";
            ApplyDBdata();
        }

        public void displaySA(int i)
        {
            displayText(saNames[i], saColors[i], saDisplayTime, saFadeoutTime);
        }

        public void displayPattern(int i)
        {
            displayText(pNames[i], pColors[i], pDisplayTime, pFadeoutTime);
        }

        public void displayText(string name,Color color, float time, float fadeTime)
        {
            StartCoroutine(DisplayRoutine(name,color, time, fadeTime));
        }

        private IEnumerator DisplayRoutine(string name,Color color, float time, float fadeTime)
        {
            textMesh.text = name;
            textMesh.color = color;
            yield return new WaitForSeconds(time);
            // 페이드 아웃
            int i = 10;
            while (i > 0)
            {
                i--;
                Color c = textMesh.color;
                c.a = i / 10;
                textMesh.color = c;
                yield return new WaitForSeconds(fadeTime / 10);
            }
            textMesh.text = "";
        }
        

        public void ApplyDBdata()
        {
            if (DB.SingleAttackDisplayColors == null)
                return;
            // 색 적용
            saColors = new Color[4];
            pColors = new Color[7];
            for (var i = 0; i < saColors.Length; i++)
            {
                saColors[i] = Code2Color(DB.SingleAttackDisplayColors[i]);
            }
            for (var i = 0; i < pColors.Length; i++)
            {
                pColors[i] = Code2Color(DB.PatternDisplayColors[i]);
            }
            // 시간 적용
            saDisplayTime = float.Parse(DB.SingleAttackDisplayTime[0]);
            saFadeoutTime = float.Parse(DB.SingleAttackDisplayTime[1]);
            pDisplayTime = float.Parse(DB.PatternDisplayTime[0]);
            pFadeoutTime = float.Parse(DB.PatternDisplayTime[1]);
            // 이름 적용
            saNames = (string[])DB.SingleAttackDisplayNames.Clone();
            pNames = (string[])DB.PatternDisplayNames.Clone();

        }

        private Color Code2Color(string code)
        {
            int r,g,b;
            var raw = code.Substring(1);;
            int s = 0;
            r = Convert.ToInt32(raw.Substring(s, 2),16);
            g = Convert.ToInt32(raw.Substring(s, 2),16);
            b = Convert.ToInt32(raw.Substring(s, 2),16);
            return new Color((float)r/256,(float)g/256,(float)b/256,1);
        }
    }
}