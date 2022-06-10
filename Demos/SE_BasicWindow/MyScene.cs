using SharpEngine;
using SharpEngine.Widgets;

namespace SE_BasicWindow
{
    class MyScene : Scene
    {
        public MyScene() : base()
        {
            AddWidget(new ProgressBar(new Vec2(100, 100), Color.GREEN, value:85));
            GetWidgets<ProgressBar>()[0].AddChild(new ProgressBar(new Vec2(100, 100), Color.BLUE, value:76));
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
