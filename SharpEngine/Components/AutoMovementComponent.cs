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
        /// Initialise le Composant.<para/>
        /// -> Paramètre 1 : Direction (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 2 : Rotation (int) (0)
        /// </summary>
        /// <param name="parameters">Paramètres du Composant</param>
        public AutoMovementComponent(params object[] parameters) : base(parameters)
        {
            direction = new Vec2(0);
            rotation = 0;

            if (parameters.Length >= 1 && parameters[0] is Vec2 dir)
                direction = dir;
            if (parameters.Length >= 2 && parameters[1] is int rot)
                rotation = rot;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if (entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                if (direction.Length() != 0)
                {
                    Vec2 pos = new Vec2(tc.position.x, tc.position.y) + direction;
                    if (entity.GetComponent<RectCollisionComponent>() is RectCollisionComponent rcc)
                    {
                        if (rcc.CanGo(pos, "ControlComponent"))
                            tc.position = pos;
                    }
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

        public override string ToString()
        {
            return $"AutoMovementComponent(direction={direction}, rotation={rotation})";
        }
    }
}
