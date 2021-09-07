using SharpEngine;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {}

        public override void Initialize()
        {
            base.Initialize();

            System.Console.WriteLine("INITIALISE MY SCENE");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            System.Console.WriteLine($"{gameTime.elapsedGameTime} - {1 / gameTime.elapsedGameTime.TotalSeconds} FPS");
        }
    }
}
