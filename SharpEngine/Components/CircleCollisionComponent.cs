using System;

namespace SharpEngine.Components
{
    public class CircleCollisionComponent : Component
    {
        public int radius;
        public Vec2 offset;
        public bool solid;
        public Action<Entity, Entity, string> collisionCallback;

        /// <summary>
        /// Initialise le Composant.<para/>
        /// -> Paramètre 1 : Taille du cercle (int) (1)<para/>
        /// -> Paramètre 2 : Décalage par rapport à la position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 3 : Bloque le passage (bool) (true)
        /// </summary>
        /// <param name="parameters">Paramètres du Composant</param>
        public CircleCollisionComponent(params object[] parameters) : base(parameters)
        {
            radius = 1;
            offset = new Vec2(0);
            solid = true;
            collisionCallback = null;

            if (parameters.Length >= 1 && parameters[0] is int rad)
                radius = rad;
            if (parameters.Length >= 2 && parameters[1] is Vec2 off)
                offset = off;
            if (parameters.Length >= 3 && parameters[2] is bool sol)
                solid = sol;
        }

        public bool CanGo(Vec2 position, string cause)
        {
            foreach (Entity e in entity.scene.GetEntities())
            {
                if (e != entity)
                { 
                    if(e.GetComponent<TransformComponent>() is TransformComponent tc) { 
                        if (e.GetComponent<RectCollisionComponent>() is RectCollisionComponent rcc)
                        {
                            if (Math.IntersectCircleRect(position.x, position.y, radius, tc.position.x, tc.position.y, rcc.size.x, rcc.size.y))
                            {
                                if (collisionCallback != null)
                                    collisionCallback(entity, e, cause);
                                if (rcc.collisionCallback != null)
                                    collisionCallback(e, entity, cause);
                                if (solid && rcc.solid)
                                    return false;
                            }
                        }
                        else if(e.GetComponent<CircleCollisionComponent>() is CircleCollisionComponent ccc)
                        {
                            var dx = position.x - tc.position.x;
                            var dy = position.y - tc.position.y;
                            var distance = System.Math.Sqrt(dx * dx + dy * dy);

                            if (distance < radius + ccc.radius)
                            {
                                if (collisionCallback != null)
                                    collisionCallback(entity, e, cause);
                                if (ccc.collisionCallback != null)
                                    collisionCallback(e, entity, cause);
                                if (solid && ccc.solid)
                                    return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            return $"CircleCollisionComponent(radius={radius}, offset={offset})";
        }
    }
}

