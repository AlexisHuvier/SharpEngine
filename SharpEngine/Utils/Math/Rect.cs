using Microsoft.Xna.Framework;

namespace SharpEngine.Utils.Math;

/// <summary>
/// Rectangle
/// </summary>
public class Rect
{
    public Vec2 Position;
    public Vec2 Size;

    public Rect(Vec2 pos, Vec2 siz)
    {
        Position = pos;
        Size = siz;
    }

    public Rect(float x, float y, Vec2 size): this(new Vec2(x, y), size) { }
    public Rect(Vec2 pos, float width, float height) : this(pos, new Vec2(width, height)) { }
    public Rect(float x, float y, float width, float height) : this(new Vec2(x, y), new Vec2(width, height)) { }

    public Rectangle ToMg() => new((int) Position.X, (int) Position.Y, (int) Size.X, (int) Size.Y);

    public override bool Equals(object obj)
    {
        if (obj is Rect rect)
            return this == rect;
        return obj != null && obj.Equals(this);
    }

    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => $"Rect(position={Position}, size={Size})";

    public static bool operator !=(Rect r1, Rect r2) => !(r1 == r2);

    public static bool operator ==(Rect r1, Rect r2)
    {
        if (r1 is null)
            return r2 is null;
        if (r2 is null)
            return false;
        return r1.Position == r2.Position && r1.Size == r2.Size;
    }

    public static implicit operator Rect(Rectangle rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
}
