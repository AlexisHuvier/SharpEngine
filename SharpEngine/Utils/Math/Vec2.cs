using System;
using Microsoft.Xna.Framework;

namespace SharpEngine.Utils.Math;

/// <summary>
/// Vecteur 2D
/// </summary>
public struct Vec2
{
    public static Vec2 Zero { get; } = new(0);
    public static Vec2 One { get; } = new(1);
    
    public float X { get; set; }
    public float Y { get; set; }

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
        return length == 0 ? Zero : new Vec2(X / length, Y / length);
    }

    public float Length() => (float) System.Math.Sqrt(X * X + Y * Y);
    public float LengthSquared() => X * X + Y * Y;

    public readonly Vector2 ToMg() => new(X, Y);

    public override bool Equals(object obj)
    {
        if (obj is Vec2 vec)
            return this == vec;
        return obj != null && obj.Equals(this);
    }
    public bool Equals(Vec2 other) => X.Equals(other.X) && Y.Equals(other.Y);
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override string ToString() => $"Vec2(x={X}, y={Y})";

    public static bool operator !=(Vec2 vec1, Vec2 vec2) => !(vec1 == vec2);

    public static bool operator ==(Vec2 vec1, Vec2 vec2) => System.Math.Abs(vec1.X - vec2.X) < InternalUtils.FloatTolerance && System.Math.Abs(vec1.Y - vec2.Y) < InternalUtils.FloatTolerance;
    public static Vec2 operator -(Vec2 vec) => new(-vec.X, -vec.Y);
    public static Vec2 operator -(Vec2 vec, Vec2 vec2) => new(vec.X - vec2.X, vec.Y - vec2.Y);
    public static Vec2 operator -(Vec2 vec, float factor) => new(vec.X - factor, vec.Y - factor);
    public static Vec2 operator +(Vec2 vec, Vec2 vec2) => new(vec.X + vec2.X, vec.Y + vec2.Y);
    public static Vec2 operator +(Vec2 vec, float factor) => new(vec.X + factor, vec.Y + factor);
    public static Vec2 operator *(Vec2 vec, Vec2 vec2) => new(vec.X * vec2.X, vec.Y * vec2.Y);
    public static Vec2 operator *(Vec2 vec, float factor) => new(vec.X * factor, vec.Y * factor);
    public static Vec2 operator /(Vec2 vec, Vec2 vec2) => new(vec.X / vec2.X, vec.Y / vec2.Y);
    public static Vec2 operator /(Vec2 vec, float factor) => new(vec.X / factor, vec.Y / factor);
    public static implicit operator Vec2(Vector2 vec) => new(vec.X, vec.Y);
}