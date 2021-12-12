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
            Entity ent = new Entity();
            ent.AddComponent<TransformComponent>(new Vec2(450, 300));
            ent.AddComponent<RectCollisionComponent>(new Vec2(10), new Vec2(0), true, new Vec2(10));
            ent.AddComponent<SpriteComponent>("test");
            ent.AddComponent<ControlComponent>(ControlType.FOURDIRECTION);
            ent.AddComponent<TextComponent>("Salut !", "arial", Color.BLACK, true, new Vec2(-10));
            AddEntity(ent);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.F))
                System.Console.WriteLine($"{DebugManager.GetFPS()} - {DebugManager.GetGCMemory()}");
        }
    }
}
