using SharpEngine;

namespace SE_BasicWindow
{
    class MyScene : Scene
    {
        public MyScene() : base()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.D1))
                GetWindow().fullscreen = FullScreenType.NO_FULLSCREEN;
            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.D2))
                GetWindow().fullscreen = FullScreenType.HARDWARE_FULLSCREEN;
            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.D3))
                GetWindow().fullscreen = FullScreenType.BORDERLESS_FULLSCREEN;
        }
    }
}
