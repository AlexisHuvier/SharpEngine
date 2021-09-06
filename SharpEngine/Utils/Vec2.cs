using System;
using Microsoft.Xna.Framework;

namespace SharpEngine
{
    public class Vec2
    {
        public float x;
        public float y;

        public Vec2(float same)
        {
            x = same;
            y = same;
        }

        public Vec2(float x_, float y_) 
        {
            x = x_;
            y = y_;
        }

        public float Length()
        {
            return (float) Math.Sqrt(x * x + y * y);
        }

        internal Vector2 ToMonoGameVector()
        {
            return new Vector2(x, y);
        }

        public override string ToString()
        {
            return $"Vec2(x={x}, y={y})";
        }
    }
}
