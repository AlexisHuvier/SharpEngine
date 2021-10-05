using Microsoft.Xna.Framework;

namespace SharpEngine
{
    public class Rect
    {
        public Vec2 position;
        public Vec2 size;

        public Rect(Vec2 pos, Vec2 siz)
        {
            position = pos;
            size = siz;
        }

        public Rect(float x, float y, Vec2 size): this(new Vec2(x, y), size) { }
        public Rect(Vec2 pos, float width, float height) : this(pos, new Vec2(width, height)) { }
        public Rect(float x, float y, float width, float height) : this(new Vec2(x, y), new Vec2(width, height)) { }

        public Rectangle ToMG()
        {
            return new Rectangle((int) position.x, (int) position.y, (int) size.x, (int) size.y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Rect rect)
                return this == rect;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Rect(position={position}, size={size})";
        }

        public static bool operator !=(Rect r1, Rect r2)
            => !(r1 == r2);

        public static bool operator ==(Rect r1, Rect r2)
        {
            if (r1 is null)
                return r2 is null;
            else if (r2 is null)
                return false;
            else
                return r1.position == r2.position && r1.size == r2.size;
        }

        public static implicit operator Rect(Rectangle rectangle)
        {
            return new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}
