using SharpEngine;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        AddWidget(new ProgressBar(new Vec2(100, 100), Color.Green, value: 85));
        GetWidgets<ProgressBar>()[0].AddChild(new ProgressBar(new Vec2(100, 100), Color.Blue, value: 76));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsKeyPressed(Key.D1))
            GetWindow().Fullscreen = FullScreenType.NoFullscreen;
        if (InputManager.IsKeyPressed(Key.D2))
            GetWindow().Fullscreen = FullScreenType.HardwareFullscreen;
        if (InputManager.IsKeyPressed(Key.D3))
            GetWindow().Fullscreen = FullScreenType.BorderlessFullscreen;
        if (InputManager.IsKeyPressed(Key.D))
            GetWindow().Debug = !GetWindow().Debug;
    }
}
