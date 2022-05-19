namespace SharpEngine.Components
{
    /// <summary>
    /// Composant permettant un mouvement ou une rotation automatique
    /// </summary>
    public class AutoMovementComponent : Component
    {
        public Vec2 direction;
        public int rotation;

        /// <summary>
        /// Initialise le Composant.
        /// </summary>
        /// <param name="direction">Mouvement automatique (Vec2(0))</param>
        /// <param name="rotation">Rotation automatique</param>
        public AutoMovementComponent(Vec2 direction = null, int rotation = 0) : base()
        {
            this.direction = direction ?? new Vec2(0);
            this.rotation = rotation;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if (entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                if (direction.Length() != 0)
                {
                    Vec2 pos = new Vec2(tc.position.x, tc.position.y) + direction;
                    if (entity.GetComponent<PhysicsComponent>() is PhysicsComponent pc)
                        pc.SetPosition(pos);
                    else
                        tc.position = pos;
                }

                if(rotation != 0)
                {
                    int rot = tc.rotation + rotation;
                    tc.rotation = rot;
                }
            }
        }

        public override string ToString() => $"AutoMovementComponent(direction={direction}, rotation={rotation})";
    }
}
