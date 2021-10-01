namespace SharpEngine.Components
{
    public class PhysicsComponent : Component
    {
        public int maxGravity;
        public int groundedGravity;
        public bool grounded;
        public int timeGravity;
        private int time;
        public int gravity;

        public PhysicsComponent(params object[] parameters) : base(parameters)
        {
            maxGravity = 5;
            groundedGravity = 2;
            timeGravity = 100;

            if (parameters.Length >= 1 && parameters[0] is int grav)
                gravity = grav;
            if (parameters.Length >= 2 && parameters[1] is int gG)
                groundedGravity = gG;
            if (parameters.Length >= 3 && parameters[2] is int tG)
                timeGravity = tG;

            grounded = false;
            gravity = 5;
            time = timeGravity;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                Vec2 pos = new Vec2(tc.position.x, tc.position.y);
                pos.y += gravity;

                if (entity.GetComponent<RectCollisionComponent>() is RectCollisionComponent rcc)
                {
                    if (rcc.CanGo(pos, "GRAVITY"))
                    {
                        grounded = false;
                        tc.position = pos;
                    }
                    else if (gravity > 0)
                    {
                        grounded = true;
                        gravity = groundedGravity;
                    }

                    if(time <= 0 && gravity < maxGravity && !grounded)
                    {
                        gravity += 1;
                        time = timeGravity;
                    }
                    time -= (int)gameTime.elapsedGameTime.TotalMilliseconds;
                }
                else
                    tc.position = pos;
            }
        }

        public override string ToString()
        {
            return $"PhysicsComponent(timeGravity={timeGravity}, maxGravity={maxGravity})";
        }
    }
}
