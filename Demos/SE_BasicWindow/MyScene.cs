using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public Entity e;
    public MyScene()
    {
        e = new Entity();
        e.AddComponent(new TransformComponent(new Vec2(450, 300), new Vec2(3)));
        e.AddComponent(new AnimSpriteSheetComponent("KnightM", new Vec2(16, 28), new Dictionary<string, List<int>>
        {
            { "hit", new List<int> { 0 } },
            { "idle", new List<int> { 1, 2, 3, 4 } },
            { "run", new List<int> { 5, 6, 7, 8 } }
        }, "idle", 100f));
        AddEntity(e);
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
