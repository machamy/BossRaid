﻿using System;
using Script.Global;
using UnityEngine;

namespace Script.Game
{
    /// <summary>
    /// 히트박스 그려주는 클래스
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class BoxCollider2DDrawer : MonoBehaviour
    {
        public Color Color;
        
        private void Start()
        {
            if (!MyDebug.Instance.getBool("showhitbox"))  
                return;
            LineRenderer lineRenderer = initLineRenderer();
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            
            Vector3[] positions = new Vector3[4];
            positions[0] = new Vector3(boxCollider2D.size.x / 2.0f, boxCollider2D.size.y / 2.0f,0);
            positions[1] = new Vector3(-boxCollider2D.size.x / 2.0f, boxCollider2D.size.y / 2.0f,0);
            positions[2] = new Vector3(-boxCollider2D.size.x / 2.0f, -boxCollider2D.size.y / 2.0f,0);
            positions[3] = new Vector3(boxCollider2D.size.x / 2.0f, -boxCollider2D.size.y / 2.0f,0);
            
            lineRenderer.SetPositions(positions);

        }
        /// <summary>
        /// 기본 세팅으로 LineRender를 추가함.
        /// </summary>
        /// <returns>추가된 LineRenderer</returns>
        private LineRenderer initLineRenderer()
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
            lineRenderer.startWidth = 0.025f;
            lineRenderer.endWidth = 0.025f;
            lineRenderer.useWorldSpace = false;
            lineRenderer.loop = true;
            lineRenderer.positionCount = 4;
            Color.a = 255;
            lineRenderer.startColor = lineRenderer.endColor = Color;
            
            return lineRenderer;
        }
        
    }
}