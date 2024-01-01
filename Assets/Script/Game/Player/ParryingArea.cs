using System;
using System.Collections.Generic;
using System.Linq;
using Script.Game.Enemy.Projectile;
using UnityEngine;

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


        /// <summary>
        /// type에 해당되는 모든 투사체를 리스트에서 빼낸 후 반환.
        /// Pop된 후에는 ParryingArea에서 관리되지 않음.
        /// </summary>
        /// <param name="type">뽑아낼 투사체 type</param>
        /// <returns>type에 해당되는 모든 투사체 Set</returns>
        public Queue<Projectile.Projectile> PopAll(PrjtType type){
            Queue<Projectile.Projectile> queue = new Queue<Projectile.Projectile>();
            // 시간복잡도 최적화를 위해 노드 접근
            var node = _inRangeList.First;
            while (node is not null)
            {
                var next_node = node.Next;
                var value = node.Value;
                if (value.Type == type)
                {
                    queue.Enqueue(value);
                    _inRangeList.Remove(node);
                    _inRangeSet.Remove(value);
                }
                node = next_node;
            }
            return queue;
        }

        

        private void Awake()
        {
            _inRangeList = new LinkedList<Projectile.Projectile>();
            _inRangeSet = new HashSet<Projectile.Projectile>();
        }

        private void Start()
        {

        }
        

        [Obsolete]
        public Projectile.Projectile GetFirst()
        {
            return _inRangeList.First();
        }

        /// <summary>
        /// 투사체가 들어왔을 떄 추가
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.CompareTag("Projectile")) return;
            Projectile.Projectile prjt = other.GetComponent<Projectile.Projectile>();
            Add(prjt);
        }

        /// <summary>
        /// 투사체가 나갔을 때 제거
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.transform.CompareTag("Projectile")) return;
            Projectile.Projectile prjt = other.GetComponent<Projectile.Projectile>();
            Remove(prjt);   
        }
        
        /// <summary>
        /// 투사체를 관리목록에 추가
        /// </summary>
        /// <param name="prjt"></param>
        /// <returns></returns>
        protected bool Add(Projectile.Projectile prjt)
        {
            if (_inRangeSet.Contains(prjt))
                return false;
            _inRangeList.AddLast(prjt);
            _inRangeSet.Add(prjt);
            return true;
        }
        
        /// <summary>
        /// 투사체를 관리목록에서 제거
        /// </summary>
        /// <param name="prjt"></param>
        /// <returns></returns>
        protected bool Remove(Projectile.Projectile prjt)
        {
            if (!_inRangeSet.Contains(prjt))
                return false;
            _inRangeList.Remove(prjt);
            _inRangeSet.Remove(prjt);
            return true;
        }

    }
}
