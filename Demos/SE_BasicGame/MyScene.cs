using SharpEngine;
using SharpEngine.Component;
using SharpEngine.Entity;
using SharpEngine.Math;
using SharpEngine.Utils;

namespace SE_BasicWindow;

internal class MyScene: Scene
{
    public MyScene()
    {
        var e = AddEntity(new Entity());
        e.AddComponent(new TransformComponent(new Vec2(400), new Vec2(2)));
        e.AddComponent(new SpriteComponent("KnightM"));
        e.AddComponent(new SpriteSheetComponent("KnightM", new Vec2(16, 28), new List<Animation>
        {
            new("hit", new List<uint> { 0 }, 0.1f),
            new("idle", new List<uint> { 1, 2, 3, 4 }, 0.1f),
            new("run", new List<uint> { 5, 6, 7, 8 }, 0.1f)
        }, "idle", true, new Vec2(200, -100)));
        e.AddComponent(new RectComponent(Color.Black, new Vec2(25), true, new Vec2(-100, -200)));
        e.AddComponent(new TextComponent("test", color: Color.Red, fontSize: 50, offset: new Vec2(-100)));
        e.AddComponent(new TextComponent("test", "basic", color: Color.Blue, fontSize: 50, offset: new Vec2(100)));
    }
}
