using SharpEngine;
using SharpEngine.Component;
using SharpEngine.Entity;
using SharpEngine.Math;

namespace SE_BasicWindow;

internal class MyScene: Scene
{
    public MyScene()
    {
        var e = AddEntity(new Entity());
        e.AddComponent(new TransformComponent(new Vec2(400)));
        e.AddComponent(new SpriteComponent("KnightM"));
    }
}
