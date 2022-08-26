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
        AddWidget(new Slider(new Vec2(200, 400), Color.Orange, font: "basic"));
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
