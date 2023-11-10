using System;
using UnityEngine;

namespace Script.Game
{
    
    /// <summary>
    /// 간단히 쓰는 방향
    /// </summary>
    [Flags]
    public enum Direction
    {
        Zero = 0,
        Left=1<<0,
        Right=1<<1,
        Up=1<<2,
        Down=1<<3,
        LeftUp= Left|Up,
        LeftDown= Left|Down,
        RightUp= Right|Up,
        RightDown= Right|Down
    }

    public static class EnumMethod
    {
        /// <summary>
        /// 벡터를 반환
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Vector2 Vector(this Direction direction)
        {
            Vector2 result = Vector2.zero;
            if(direction.HasFlag(Direction.Left))
                result += Vector2.left;
            if(direction.HasFlag(Direction.Right))
                result += Vector2.right;
            if(direction.HasFlag(Direction.Down))
                result += Vector2.down;
            if (direction.HasFlag(Direction.Up))
                result += Vector2.up;
            result.Normalize();
            return result;
        }
    }
}