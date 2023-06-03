using System;

namespace SharpEngine.Math;

public struct Rect
{
    public float X;
    public float Y;
    public float Width;
    public float Height;

    public Rect(Vec2 position, Vec2 size): this(position.X, position.Y, size.X, size.Y) { }
    public Rect(float x, float y, Vec2 size): this(x, y, size.X, size.Y) { }
    public Rect(Vec2 position, float width, float height): this(position.X, position.Y, width, height) { }

    public Rect(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

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
    public static bool operator ==(Rect r1, Rect r2) => System.Math.Abs(r1.X - r2.X) < Internal.FloatTolerance &&
                                                        System.Math.Abs(r1.Y - r2.Y) < Internal.FloatTolerance &&
                                                        System.Math.Abs(r1.Width - r2.Width) < Internal.FloatTolerance &&
                                                        System.Math.Abs(r1.Height - r2.Height) < Internal.FloatTolerance;
    public static implicit operator Rect(RectI rectI) => new(rectI.X, rectI.Y, rectI.Width, rectI.Height);
}