using SharpEngine;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {
            AddEntity(new Entity());
            System.Console.WriteLine(entities);
        }
    }
}
