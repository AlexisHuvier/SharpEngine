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
            ent.AddComponent<RectCollisionComponent>(new Vec2(10), new Vec2(0), false).collisionCallback = (Entity e, Entity other, string cause) => System.Console.WriteLine(cause);
            ent.AddComponent<SpriteComponent>("test");
            ent.AddComponent<ControlComponent>(ControlType.FOURDIRECTION);
            AddEntity(ent);

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
