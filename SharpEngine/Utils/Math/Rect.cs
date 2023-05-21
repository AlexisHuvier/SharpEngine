using System;
using Microsoft.Xna.Framework;

namespace SharpEngine.Utils.Math;

/// <summary>
/// Rectangle
/// </summary>
public struct Rect
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Rect(Vec2 position, Vec2 size)
    {
        X = position.X;
        Y = position.Y;
        Width = size.X;
        Height = size.Y;
    }

    public Rect(float x, float y, Vec2 size)
    {
        X = x;
        Y = y;
        Width = size.X;
        Height = size.Y;
    }

    public Rect(Vec2 position, float width, float height)
    {
        X = position.X;
        Y = position.Y;
        Width = width;
        Height = height;
    }

    public Rect(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public Rectangle ToMg() => new((int) X, (int) Y, (int) Width, (int) Height);

    public override bool Equals(object obj)
    {
        if (obj is Rect rect)
            return this == rect;
        return obj != null && obj.Equals(this);
    }

    public bool Equals(Rect other) => X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) &&
                                      Height.Equals(other.Height);
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);
    public override string ToString() => $"Rect(X={X}, Y={Y}, Width={Width}, Height={Height})";
    public static bool operator !=(Rect r1, Rect r2) => !(r1 == r2);
    public static bool operator ==(Rect r1, Rect r2) => System.Math.Abs(r1.X - r2.X) < 0.00001f &&
                                                        System.Math.Abs(r1.Y - r2.Y) < 0.00001f &&
                                                        System.Math.Abs(r1.Width - r2.Width) < 0.00001f &&
                                                        System.Math.Abs(r1.Height - r2.Height) < 0.00001f;
    public static implicit operator Rect(Rectangle rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
}
