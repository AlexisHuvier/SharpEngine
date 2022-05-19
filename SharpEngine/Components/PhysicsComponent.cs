using System;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

namespace SharpEngine.Components
{

    /// <summary>
    /// Composant de physique basique
    /// </summary>
    public class PhysicsComponent : Component
    {
        private enum FixtureType
        {
            RECTANGLE,
            CIRCLE
        }

        private class FixtureInfo
        {
            public float density;
            public float restitution;
            public float friction;
            public FixtureType type;
            public object parameter;
            public Vec2 offset;
        }

        public Body body;

        private BodyType bodyType;
        private List<FixtureInfo> fixtures = new List<FixtureInfo>();

        /// <summary>
        /// Initialise le Composant
        /// </summary>
        /// <param name="bodyType">Type de corps</param>
        public PhysicsComponent(BodyType bodyType = BodyType.Dynamic) : base()
        {
            this.bodyType = bodyType;
        }

        public Vec2 GetPosition() => new Vec2(body.Position.X, body.Position.Y);
        public void SetPosition(Vec2 position) => body.Position = new tainicom.Aether.Physics2D.Common.Vector2(position.x, position.y);

        public int GetRotation() => (int)(body.Rotation * 180 / System.Math.PI);
        public void SetRotation(int rotation) => body.Rotation = (float)(rotation * System.Math.PI / 180f);

        public void AddRectangleCollision(Vec2 size, Vec2 offset = null, float density = 1f, float restitution = 0.5f, float friction = 0.5f)
        {
            FixtureInfo fixture = new FixtureInfo()
            {
                density = density,
                restitution = restitution,
                friction = friction,
                type = FixtureType.RECTANGLE,
                parameter = size,
                offset = offset ?? new Vec2(0)
            };
            fixtures.Add(fixture);
        }

        public void AddCircleCollision(float radius, Vec2 offset = null, float density = 1f, float restitution = 0.5f, float friction = 0.5f)
        {
            FixtureInfo fixture = new FixtureInfo()
            {
                density = density,
                restitution = restitution,
                friction = friction,
                type = FixtureType.CIRCLE,
                parameter = radius,
                offset = offset ?? new Vec2(0)
            };
            fixtures.Add(fixture);
        }

        public override void Initialize()
        {
            base.Initialize();

            body = GetWindow().GetCurrentScene().world.CreateBody(bodyType: bodyType);
            foreach(FixtureInfo info in fixtures)
            {
                Fixture fixture;
                switch(info.type)
                {
                    case FixtureType.RECTANGLE:
                        Vec2 size = info.parameter as Vec2;
                        fixture = body.CreateRectangle(size.x, size.y, info.density, info.offset.ToAetherPhysics());
                        break;
                    case FixtureType.CIRCLE:
                        float radius = Convert.ToSingle(info.parameter);
                        fixture = body.CreateCircle(radius, info.density, info.offset.ToAetherPhysics());
                        break;
                    default:
                        throw new Exception($"Unknown Type of Fixture : {info.type}");
                }
                fixture.Restitution = info.restitution;
                fixture.Friction = info.friction;
            }

            if(entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                body.Rotation = (float)(tc.rotation * System.Math.PI / 180f);
                body.Position = new tainicom.Aether.Physics2D.Common.Vector2(tc.position.x, tc.position.y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                tc.position.x = body.Position.X;
                tc.position.y = body.Position.Y;
                tc.rotation = (int)(body.Rotation * 180 / System.Math.PI);
            }
        }

        public override string ToString() => $"PhysicsComponent(body={body}, nbFixtures={fixtures.Count})";
    }
}
