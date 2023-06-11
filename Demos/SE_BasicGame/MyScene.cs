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
        e.AddComponent(new TransformComponent(new Vec2(400)));
        e.AddComponent(new SpriteComponent("KnightM"));
        e.AddComponent(new TextComponent("test", color: Color.Red, fontSize: 50, offset: new Vec2(-100)));
        e.AddComponent(new TextComponent("test", "basic", color: Color.Blue, fontSize: 50, offset: new Vec2(100)));
    }
}
