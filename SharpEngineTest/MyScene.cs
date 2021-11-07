using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Widgets;
using System.Collections.Generic;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {
            Entity ent2 = new Entity();
            ent2.AddComponent<TransformComponent>(new Vec2(450, 300));
            ent2.AddComponent<TileMapComponent>("tiled/map_multilayer.tmx");
            AddEntity(ent2);

            Entity ent = new Entity();
            ent.AddComponent<TransformComponent>(new Vec2(450, 300));
            ent.AddComponent<RectCollisionComponent>(new Vec2(10), new Vec2(0), true);
            ent.AddComponent<SpriteComponent>("test");
            ent.AddComponent<ControlComponent>(ControlType.FOURDIRECTION);
            ent.AddComponent<AutoMovementComponent>(new Vec2(2), 5);
            AddEntity(ent);

            Entity ent3 = new Entity();
            ent3.AddComponent<TransformComponent>(new Vec2(0, 0));
            ent3.AddComponent<CircleCollisionComponent>(200, new Vec2(0), true);
            ent3.AddComponent<SpriteComponent>("test");
            AddEntity(ent3);

            CameraManager.followEntity = ent;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.F))
                System.Console.WriteLine($"{DebugManager.GetFPS()} - {DebugManager.GetGCMemory()}");
        }
    }
}
