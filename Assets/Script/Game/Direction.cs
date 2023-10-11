using System;
using UnityEngine;

namespace Script.Game
{
    
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