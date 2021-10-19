using Microsoft.Xna.Framework;
using System;

namespace SharpEngine.Components
{
    /// <summary>
    /// Composant ajoutant une collision rectangulaire
    /// </summary>
    public class RectCollisionComponent: Component
    {
        public Vec2 size;
        public Vec2 offset;
        public bool solid;
        public Action<Entity, Entity, string> collisionCallback;

        /// <summary>
        /// Initialise le Composant.<para/>
        /// -> Paramètre 1 : Taille du rectangle (<seealso cref="Vec2"/>) (Vec2(1))<para/>
        /// -> Paramètre 2 : Décalage par rapport à la position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 3 : Bloque le passage (bool) (true)
        /// </summary>
        /// <param name="parameters">Paramètres du Composant</param>
        public RectCollisionComponent(params object[] parameters): base(parameters)
        {
            size = new Vec2(1);
            offset = new Vec2(0);
            solid = true;
            collisionCallback = null;

            if (parameters.Length >= 1 && parameters[0] is Vec2 si)
                size = si;
            if (parameters.Length >= 2 && parameters[1] is Vec2 off)
                offset = off;
            if (parameters.Length >= 3 && parameters[2] is bool sol)
                solid = sol;
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
                        {
                            if (collisionCallback != null)
                                collisionCallback(entity, e, cause);
                            if (rcc.collisionCallback != null)
                                collisionCallback(e, entity, cause);
                            if (solid && rcc.solid)
                                return false;
                        }
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
