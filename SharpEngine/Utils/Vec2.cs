using Microsoft.Xna.Framework;

namespace SharpEngine
{
    /// <summary>
    /// Vecteur 2D
    /// </summary>
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

        public Vec2 Normalized()
        {
            var length = Length();
            if (length == 0)
                return null;
            else
                return new Vec2(x / length, y / length);
        }

        public float Length() => (float) System.Math.Sqrt(x * x + y * y);
        public float LengthSquared() => x * x + y * y;

        public Vector2 ToMG() => new Vector2(x, y);
        public tainicom.Aether.Physics2D.Common.Vector2 ToAetherPhysics() => new tainicom.Aether.Physics2D.Common.Vector2(x, y);

        public override bool Equals(object obj)
        {
            if (obj is Vec2 vec)
                return this == vec;
            return base.Equals(obj);
        }

        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"Vec2(x={x}, y={y})";

        public static bool operator !=(Vec2 vec1, Vec2 vec2) => !(vec1 == vec2);

        public static bool operator ==(Vec2 vec1, Vec2 vec2)
        {
            if (vec1 is null)
                return vec2 is null;
            else if (vec2 is null)
                return false;
            else
                return vec1.x == vec2.x && vec1.y == vec2.y;
        }

        public static Vec2 operator -(Vec2 vec) => vec == null ? null : new Vec2(-vec.x, -vec.y);
        public static Vec2 operator -(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.x - vec2.x, vec.y - vec2.y);
        public static Vec2 operator -(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.x - factor, vec.y - factor);
        public static Vec2 operator +(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.x + vec2.x, vec.y + vec2.y);
        public static Vec2 operator +(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.x + factor, vec.y + factor);
        public static Vec2 operator *(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.x * vec2.x, vec.y * vec2.y);
        public static Vec2 operator *(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.x * factor, vec.y * factor);
        public static Vec2 operator /(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.x / vec2.x, vec.y / vec2.y);
        public static Vec2 operator /(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.x / factor, vec.y / factor);
        public static implicit operator Vec2(Vector2 vec) => new Vec2(vec.X, vec.Y);
    }
}
