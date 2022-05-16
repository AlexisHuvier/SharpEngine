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
        public Body body;

        public readonly string type;
        public readonly List<object> parameters;

        /// <summary>
        /// Initialise le Composant avec une collision Rectangulaire
        /// </summary>
        /// <param name="size">Taille de la collision</param>
        /// <param name="density">Densité</param>
        /// <param name="restitution">Restitution</param>
        /// <param name="friction">Friction</param>
        /// <param name="bodyType">Type de corps</param>
        public PhysicsComponent(Vec2 size, float density = 1f, float restitution = 0.5f, float friction = 0.5f, BodyType bodyType = BodyType.Dynamic) : base()
        {
            type = "Rectangle";
            parameters = new List<object>()
            {
                size, density, restitution, friction, bodyType
            };
        }

        /// <summary>
        /// Initialise le Composant avec une collision Circulaire
        /// </summary>
        /// <param name="radius">Rayon de la collision</param>
        /// <param name="density">Densité</param>
        /// <param name="restitution">Restitution</param>
        /// <param name="friction">Friction</param>
        /// <param name="bodyType">Type de corps</param>
        public PhysicsComponent(float radius, float density = 1f, float restitution = 0.5f, float friction = 0.5f, BodyType bodyType = BodyType.Dynamic) : base()
        {
            type = "Circle";
            parameters = new List<object>()
            {
                radius, density, restitution, friction, bodyType
            };
        }

        public Vec2 GetPosition() => new Vec2(body.Position.X, body.Position.Y);
        public void SetPosition(Vec2 position) => body.Position = new tainicom.Aether.Physics2D.Common.Vector2(position.x, position.y);

        public int GetRotation() => (int)(body.Rotation * 180 / System.Math.PI);
        public void SetRotation(int rotation) => body.Rotation = (float)(rotation * System.Math.PI / 180f);

        public override void Initialize()
        {
            base.Initialize();

            float density = System.Convert.ToSingle(parameters[1]);
            float restitution = System.Convert.ToSingle(parameters[2]);
            float friction = System.Convert.ToSingle(parameters[3]);
            BodyType bodyType = (BodyType)parameters[4];
            body = GetWindow().GetCurrentScene().world.CreateBody(bodyType: bodyType);
            Fixture fixture;

            switch(type)
            {
                case "Rectangle":
                    Vec2 size = parameters[0] as Vec2;
                    fixture = body.CreateRectangle(size.x, size.y, density, tainicom.Aether.Physics2D.Common.Vector2.Zero);
                    break;
                case "Circle":
                    float radius = System.Convert.ToSingle(parameters[0]);
                    fixture = body.CreateCircle(radius, density, tainicom.Aether.Physics2D.Common.Vector2.Zero);
                    break;
                default:
                    throw new Exception($"Unknown Type of Body : {type}");
            }

            fixture.Restitution = restitution;
            fixture.Friction = friction;

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

        public override string ToString()
        {
            return $"PhysicsComponent(body={body}, type={type})";
        }
    }
}
