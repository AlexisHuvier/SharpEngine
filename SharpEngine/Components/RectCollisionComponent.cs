using Microsoft.Xna.Framework;

namespace SharpEngine.Components
{
    public class RectCollisionComponent: Component
    {
        public Vec2 size;
        public Vec2 offset;

        public RectCollisionComponent(params object[] parameters): base(parameters)
        {
            size = new Vec2(1);
            offset = new Vec2(0);

            if (parameters.Length >= 1 && parameters[0] is Vec2 si)
                size = si;
            if (parameters.Length >= 2 && parameters[1] is Vec2 off)
                offset = off;
        }

        public bool CanGo(Vec2 position, string cause)
        {
            var rect = new Rectangle((int) (position.x + offset.x - size.x / 2), (int) (position.y + offset.y - size.y / 2), (int)size.x, (int)size.y);
            foreach(Entity e in entity.scene.GetEntities())
            {
                if (e != entity)
                {
                    if (e.GetComponent<RectCollisionComponent>() is RectCollisionComponent rcc && e.GetComponent<TransformComponent>() is TransformComponent tc)
                    {
                        var erect = new Rectangle((int)(tc.position.x + rcc.offset.x - rcc.size.x / 2), (int)(tc.position.y + rcc.offset.y - rcc.size.y / 2), (int)rcc.size.x, (int)rcc.size.y);
                        if (erect.Intersects(rect))
                            return false;
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            return $"RectCollisionComponent(size={size}, offset={offset})";
        }
    }
}
