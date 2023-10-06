using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

namespace Script.Game.Player
{
    /// <summary>
    /// 패링 구역 클래스.
    /// 패링 구역 안에 있는 투사체에 대한 관리를 한다.
    /// </summary>
    public class ParryingArea : MonoBehaviour
    {
        /// <summary>
        /// queue 로 관리
        /// </summary>
        private LinkedList<Projectile.Projectile> _inRangeList;
        private HashSet<Projectile.Projectile> _inRangeSet;
        public bool Parryable
        {
            get => _inRangeList.Any();
        }

        private void Awake()
        {
            _inRangeList = new LinkedList<Projectile.Projectile>();
            _inRangeSet = new HashSet<Projectile.Projectile>();
        }

        public Projectile.Projectile GetFirst()
        {
            return _inRangeList.First();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.CompareTag("Projectile")) return;
            Projectile.Projectile prjt = other.GetComponent<Projectile.Projectile>();
            Add(prjt);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.transform.CompareTag("Projectile")) return;
            Projectile.Projectile prjt = other.GetComponent<Projectile.Projectile>();
            Remove(prjt);   
        }

        public bool Add(Projectile.Projectile prjt)
        {
            if (_inRangeSet.Contains(prjt))
                return false;
            _inRangeList.AddLast(prjt);
            _inRangeSet.Add(prjt);
            return true;
        }
        
        public bool Remove(Projectile.Projectile prjt)
        {
            if (!_inRangeSet.Contains(prjt))
                return false;
            _inRangeList.Remove(prjt);
            _inRangeSet.Remove(prjt);
            return true;
        }

    }
}
