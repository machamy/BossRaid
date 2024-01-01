using UnityEngine;

namespace Script.Game.Enemy.Projectile
{
    public enum PrjtType
    {
        Practice,
        Team,
        Attend,
        Wall
    }

    public abstract class BaseProjectile : MonoBehaviour
    {
        public float speed;
        public Vector2 facing;
        public Vector2 target;
        [SerializeField] private PrjtType type;
        [SerializeField] protected AudioClip sound;


        public PrjtType Type => type;

        public int damage;
        public int parringScore;

        /// <summary>
        /// 생성시 실행.
        /// </summary>
        public abstract void OnSummon();

        public abstract void OnHit(Player.Player p);

        public abstract void Remove();
    }
}