using SharpEngine;
using SharpEngine.Components;

namespace SE_BasicPhysics
{
    class MyScene : Scene
    {
        public MyScene() : base()
        {
            Entity ent = new Entity();
            ent.AddComponent(new TransformComponent(new Vec2(420, 300)));
            ent.AddComponent(new SpriteComponent("test"));
            ent.AddComponent(new PhysicsComponent(tainicom.Aether.Physics2D.Dynamics.BodyType.Dynamic)).AddRectangleCollision(new Vec2(44));
            AddEntity(ent);

            Entity e2 = new Entity();
            e2.AddComponent(new TransformComponent(new Vec2(450, 500)));
            e2.AddComponent(new SpriteComponent("test"));
            e2.AddComponent(new PhysicsComponent(tainicom.Aether.Physics2D.Dynamics.BodyType.Static)).AddRectangleCollision(new Vec2(44));
            AddEntity(e2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.F))
                System.Console.WriteLine($"{DebugManager.GetFPS()} - {DebugManager.GetGCMemory()}");

            if (InputManager.IsMouseButtonPressed(SharpEngine.Inputs.MouseButton.LEFT))
                entities[0].GetComponent<PhysicsComponent>().SetPosition(new Vec2(420, 300));
        }
    }
}
