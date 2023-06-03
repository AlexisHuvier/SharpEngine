using System;

namespace SharpEngine.Math;

public struct RectI
{
    public int X;
    public int Y;
    public int Width;
    public int Height;

    public RectI(Vec2I position, Vec2I size): this(position.X, position.Y, size.X, size.Y) { }
    public RectI(int x, int y, Vec2I size): this(x, y, size.X, size.Y) { }
    public RectI(Vec2I position, int width, int height): this(position.X, position.Y, width, height) { }

    public RectI(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public override bool Equals(object obj)
    {
        if (obj is RectI rect)
            return this == rect;
        return obj != null && obj.Equals(this);
    }
    
    public bool Equals(RectI other) => X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) &&
                                      Height.Equals(other.Height);
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);
    public override string ToString() => $"Rect(X={X}, Y={Y}, Width={Width}, Height={Height})";
    public static bool operator !=(RectI r1, RectI r2) => !(r1 == r2);
    public static bool operator ==(RectI r1, RectI r2) =>
        r1.X == r2.X && r1.Y == r2.Y && r1.Width == r2.Width && r1.Height == r2.Height;

}