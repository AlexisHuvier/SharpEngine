using SharpEngine;
using SharpEngine.Components;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
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

        }
    }
}
