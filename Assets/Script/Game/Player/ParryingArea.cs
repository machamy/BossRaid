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
        private LinkedList<BaseProjectile> _inRangeList;
        private HashSet<BaseProjectile> _inRangeSet;
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
        public Queue<BaseProjectile> PopAll(PrjtType type){
            Queue<BaseProjectile> queue = new Queue<BaseProjectile>();
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
            _inRangeList = new LinkedList<BaseProjectile>();
            _inRangeSet = new HashSet<BaseProjectile>();
        }

        private void Start()
        {

        }
        

        [Obsolete]
        public BaseProjectile GetFirst()
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
            BaseProjectile prjt = other.GetComponent<BaseProjectile>();
            Add(prjt);
        }

        /// <summary>
        /// 투사체가 나갔을 때 제거
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.transform.CompareTag("Projectile")) return;
            BaseProjectile prjt = other.GetComponent<BaseProjectile>();
            Remove(prjt);   
        }
        
        /// <summary>
        /// 투사체를 관리목록에 추가
        /// </summary>
        /// <param name="prjt"></param>
        /// <returns></returns>
        protected bool Add(BaseProjectile prjt)
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
        protected bool Remove(BaseProjectile prjt)
        {
            if (!_inRangeSet.Contains(prjt))
                return false;
            _inRangeList.Remove(prjt);
            _inRangeSet.Remove(prjt);
            return true;
        }

    }
}
