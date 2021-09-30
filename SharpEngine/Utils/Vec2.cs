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
            return (float) System.Math.Sqrt(x * x + y * y);
        }

        internal Vector2 ToMG()
        {
            return new Vector2(x, y);
        }

        public override string ToString()
        {
            return $"Vec2(x={x}, y={y})";
        }

        public static Vec2 operator -(Vec2 vec, Vec2 vec2)
            => new Vec2(vec.x - vec2.x, vec.y - vec2.y);

        public static Vec2 operator -(Vec2 vec, int factor)
            => new Vec2(vec.x - factor, vec.y - factor);
        public static Vec2 operator +(Vec2 vec, Vec2 vec2)
            => new Vec2(vec.x + vec2.x, vec.y + vec2.y);

        public static Vec2 operator +(Vec2 vec, int factor)
            => new Vec2(vec.x + factor, vec.y + factor);

        public static Vec2 operator *(Vec2 vec, Vec2 vec2)
            => new Vec2(vec.x * vec2.x, vec.y * vec2.y);

        public static Vec2 operator *(Vec2 vec, int factor)
            => new Vec2(vec.x * factor, vec.y * factor);

        public static Vec2 operator /(Vec2 vec, Vec2 vec2)
            => new Vec2(vec.x / vec2.x, vec.y / vec2.y);

        public static Vec2 operator /(Vec2 vec, int factor)
            => new Vec2(vec.x / factor, vec.y / factor);

        public static implicit operator Vec2(Vector2 vec)
        {
            return new Vec2(vec.X, vec.Y);
        }
    }
}
