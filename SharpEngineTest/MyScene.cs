using SharpEngine;
using SharpEngine.Components;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {

            var ent = new Entity();
            ent.AddComponent<TransformComponent>(new Vec2(100), new Vec2(2, 2), 45);
            ent.AddComponent<SpriteComponent>("test");
            AddEntity(ent);

            foreach(Component comp in entities[0].GetComponents())
                System.Console.WriteLine(comp);
        }
    }
}
