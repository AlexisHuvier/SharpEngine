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

        public static implicit operator Rect(Rectangle rectangle)
        {
            return new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}
