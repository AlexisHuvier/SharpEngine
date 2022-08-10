using Microsoft.Xna.Framework;

namespace SharpEngine.Utils.Math;

/// <summary>
/// Vecteur 2D
/// </summary>
public class Vec2
{
    public float X;
    public float Y;

    public Vec2(float same)
    {
        X = same;
        Y = same;
    }

    public Vec2(float x, float y) 
    {
        X = x;
        Y = y;
    }

    public Vec2 Normalized()
    {
        var length = Length();
        return length == 0 ? null : new Vec2(X / length, Y / length);
    }

    public float Length() => (float) System.Math.Sqrt(X * X + Y * Y);
    public float LengthSquared() => X * X + Y * Y;

    public Vector2 ToMg() => new(X, Y);
    public tainicom.Aether.Physics2D.Common.Vector2 ToAetherPhysics() => new(X, Y);

    public override bool Equals(object obj)
    {
        if (obj is Vec2 vec)
            return this == vec;
        return obj != null && obj.Equals(this);
    }

    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => $"Vec2(x={X}, y={Y})";

    public static bool operator !=(Vec2 vec1, Vec2 vec2) => !(vec1 == vec2);

    public static bool operator ==(Vec2 vec1, Vec2 vec2)
    {
        if (vec1 is null)
            return vec2 is null;
        if (vec2 is null)
            return false;
        return System.Math.Abs(vec1.X - vec2.X) < InternalUtils.FloatTolerance && System.Math.Abs(vec1.Y - vec2.Y) < InternalUtils.FloatTolerance;
    }

    public static Vec2 operator -(Vec2 vec) => vec == null ? null : new Vec2(-vec.X, -vec.Y);
    public static Vec2 operator -(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.X - vec2.X, vec.Y - vec2.Y);
    public static Vec2 operator -(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.X - factor, vec.Y - factor);
    public static Vec2 operator +(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.X + vec2.X, vec.Y + vec2.Y);
    public static Vec2 operator +(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.X + factor, vec.Y + factor);
    public static Vec2 operator *(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.X * vec2.X, vec.Y * vec2.Y);
    public static Vec2 operator *(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.X * factor, vec.Y * factor);
    public static Vec2 operator /(Vec2 vec, Vec2 vec2) => vec == null || vec2 == null ? null : new Vec2(vec.X / vec2.X, vec.Y / vec2.Y);
    public static Vec2 operator /(Vec2 vec, float factor) => vec == null ? null : new Vec2(vec.X / factor, vec.Y / factor);
    public static implicit operator Vec2(Vector2 vec) => new(vec.X, vec.Y);
}