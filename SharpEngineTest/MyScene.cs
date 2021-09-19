using SharpEngine;
using SharpEngine.Components;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {

            var ent = new Entity();
            ent.AddComponent<TransformComponent>(new Vec2(100), new Vec2(2, 2));
            ent.AddComponent<SpriteComponent>("test");
            ent.AddComponent<ControlComponent>();
            ent.AddComponent<RectCollisionComponent>(new Vec2(90));
            AddEntity(ent);

            var ent2 = new Entity();
            ent2.AddComponent<TransformComponent>(new Vec2(300), new Vec2(1, 1));
            ent2.AddComponent<SpriteComponent>("test");
            ent2.AddComponent<RectCollisionComponent>(new Vec2(45));
            AddEntity(ent2);

            foreach (Component comp in entities[0].GetComponents())
                System.Console.WriteLine(comp);
        }
    }
}
