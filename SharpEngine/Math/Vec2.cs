using System;

namespace SharpEngine.Math;

/// <summary>
/// Vecteur 2D flottant
/// </summary>
public struct Vec2
{
    public static readonly Vec2 Zero  = new(0);
    public static readonly Vec2 One = new(1);
    
    public float X;
    public float Y;

    public float Length => MathF.Sqrt(X * X + Y * Y);
    public float LengthSquared => X * X + Y * Y;

    public Vec2 Normalized
    {
        get
        {
            var length = MathF.Sqrt(X * X + Y * Y);
            return length == 0 ? Zero : new Vec2(X / length, Y / length);
        }
    }

    public Vec2(float xy)
    {
        X = xy;
        Y = xy;
    }

    public Vec2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float DistanceTo(Vec2 vec2)
    {
        var x = System.Math.Abs(vec2.X - X);
        var y = System.Math.Abs(vec2.Y - Y);
        return MathF.Sqrt(x * x + y * y);
    }
    
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

    public static bool operator ==(Vec2 vec1, Vec2 vec2) =>
        System.Math.Abs(vec1.X - vec2.X) < Internal.FloatTolerance &&
        System.Math.Abs(vec1.Y - vec2.Y) < Internal.FloatTolerance;
    
    public static Vec2 operator -(Vec2 vec) => new(-vec.X, -vec.Y);
    public static Vec2 operator -(Vec2 vec, Vec2 vec2) => new(vec.X - vec2.X, vec.Y - vec2.Y);
    public static Vec2 operator -(Vec2 vec, float factor) => new(vec.X - factor, vec.Y - factor);
    public static Vec2 operator +(Vec2 vec, Vec2 vec2) => new(vec.X + vec2.X, vec.Y + vec2.Y);
    public static Vec2 operator +(Vec2 vec, float factor) => new(vec.X + factor, vec.Y + factor);
    public static Vec2 operator *(Vec2 vec, Vec2 vec2) => new(vec.X * vec2.X, vec.Y * vec2.Y);
    public static Vec2 operator *(Vec2 vec, float factor) => new(vec.X * factor, vec.Y * factor);
    public static Vec2 operator /(Vec2 vec, Vec2 vec2) => new(vec.X / vec2.X, vec.Y / vec2.Y);
    public static Vec2 operator /(Vec2 vec, float factor) => new(vec.X / factor, vec.Y / factor);
    public static implicit operator Vec2(Vec2I vec2I) => new(vec2I.X, vec2I.Y);
}