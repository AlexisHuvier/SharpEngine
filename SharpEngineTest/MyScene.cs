using SharpEngine;
using SharpEngine.Components;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {
            var ent = new Entity();
            ent.AddComponent<TransformComponent>(new Vec2(30));
            AddEntity(ent);
            System.Console.WriteLine(entities[0].GetComponent<TransformComponent>());
        }
    }
}
