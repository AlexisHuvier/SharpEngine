using SharpEngine;
using SharpEngine.Component;
using SharpEngine.Entity;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using SharpEngine.Utils.Input;
using tainicom.Aether.Physics2D.Dynamics;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        var e1 = new Entity();
        e1.AddComponent(new TransformComponent(new Vec2(100)));
        e1.AddComponent(new RectComponent(Color.Blue, new Vec2(50)));
        e1.AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true)).AddRectangleCollision(new Vec2(50));
        e1.AddComponent(new ControlComponent());
        AddEntity(e1);
        
        var e2 = new Entity();
        e2.AddComponent(new TransformComponent(new Vec2(200)));
        e2.AddComponent(new RectComponent(Color.Red, new Vec2(50)));
        e2.AddComponent(new PhysicsComponent(BodyType.Static, ignoreGravity: true, fixedRotation: true)).AddRectangleCollision(new Vec2(50));
        AddEntity(e2);
    }

    public override void Update(float delta)
    {
        base.Update(delta);

        Window!.CameraManager.Rotation += 10 * delta;
    }
}
