using System;
using System.Numerics;

namespace SharpEngine.Math;

/// <summary>
/// Vecteur 2D entier
/// </summary>
public struct Vec2I
{
    public static readonly Vec2I Zero  = new(0);
    public static readonly Vec2I One = new(1);
    
    public int X;
    public int Y;

    public float Length => MathF.Sqrt(X * X + Y * Y);
    public float LengthSquared => X * X + Y * Y;

    public Vec2 Normalized
    {
        get
        {
            var length = MathF.Sqrt(X * X + Y * Y);
            return length == 0 ? Vec2.Zero : new Vec2(X / length, Y / length);
        }
    }

    public Vec2I(int xy)
    {
        X = xy;
        Y = xy;
    }

    public Vec2I(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public float DistanceTo(Vec2I vec2)
    {
        var x = vec2.X - X;
        var y = vec2.Y - Y;
        return MathF.Sqrt(x * x + y * y);
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Vec2I vec)
            return this == vec;
        return obj != null && obj.Equals(this);
    }
    public bool Equals(Vec2I other) => X.Equals(other.X) && Y.Equals(other.Y);
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override string ToString() => $"Vec2I(x={X}, y={Y})";

    public static bool operator !=(Vec2I vec1, Vec2I vec2) => !(vec1 == vec2);

    public static bool operator ==(Vec2I vec1, Vec2I vec2) => vec1.X == vec2.X && vec1.Y == vec2.Y;
    
    public static Vec2I operator -(Vec2I vec) => new(-vec.X, -vec.Y);
    public static Vec2I operator -(Vec2I vec, Vec2I vec2) => new(vec.X - vec2.X, vec.Y - vec2.Y);
    public static Vec2I operator -(Vec2I vec, int factor) => new(vec.X - factor, vec.Y - factor);
    public static Vec2I operator +(Vec2I vec, Vec2I vec2) => new(vec.X + vec2.X, vec.Y + vec2.Y);
    public static Vec2I operator +(Vec2I vec, int factor) => new(vec.X + factor, vec.Y + factor);
    public static Vec2I operator *(Vec2I vec, Vec2I vec2) => new(vec.X * vec2.X, vec.Y * vec2.Y);
    public static Vec2I operator *(Vec2I vec, int factor) => new(vec.X * factor, vec.Y * factor);
    public static Vec2I operator /(Vec2I vec, Vec2I vec2) => new(vec.X / vec2.X, vec.Y / vec2.Y);
    public static Vec2I operator /(Vec2I vec, int factor) => new(vec.X / factor, vec.Y / factor);
    public static implicit operator Vec2I(Vector2 vec) => new((int)vec.X, (int)vec.Y);
    public static implicit operator Vector2(Vec2I vec) => new(vec.X, vec.Y);
}