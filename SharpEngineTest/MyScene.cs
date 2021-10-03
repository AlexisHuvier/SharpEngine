using SharpEngine;
using SharpEngine.Components;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        Save save;

        public MyScene(): base()
        {
            Entity ent = new Entity();
            ent.AddComponent<TransformComponent>(new Vec2(100));
            ent.AddComponent<RectCollisionComponent>(new Vec2(10), new Vec2(0), false).collisionCallback = (Entity e, Entity other, string cause) => System.Console.WriteLine(cause);
            ent.AddComponent<SpriteComponent>("test");
            ent.AddComponent<ControlComponent>(ControlType.FOURDIRECTION);
            AddEntity(ent);

            Entity ent2 = new Entity();
            ent2.AddComponent<TransformComponent>(new Vec2(100, 300));
            ent2.AddComponent<RectCollisionComponent>(new Vec2(10), new Vec2(0), false);
            ent2.AddComponent<SpriteComponent>("test");
            AddEntity(ent2);

            CameraManager.followEntity = ent;

            save = Save.Load("save.ses", new System.Collections.Generic.Dictionary<string, object>() { { "point", 1 }});
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GetEntities()[0].GetComponent<TransformComponent>().zLayer = (int)GetEntities()[0].GetComponent<TransformComponent>().position.y;
            GetEntities()[1].GetComponent<TransformComponent>().zLayer = (int)GetEntities()[1].GetComponent<TransformComponent>().position.y;

            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.P))
                GetWindow().TakeScreenshot("test");
            if (InputManager.IsKeyReleased(SharpEngine.Inputs.Key.Q))
                GetWindow().Stop();
            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.A))
                save.SetObject("point", save.GetObjectAs<int>("point") + 1);
            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.S))
                save.Write("save.ses");
            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.R))
                System.Console.WriteLine(save.GetObjectAs<int>("point"));
        }
    }
}
