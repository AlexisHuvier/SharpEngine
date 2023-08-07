using SharpEngine;
using SharpEngine.Component;
using SharpEngine.Entity;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using SharpEngine.Utils.Input;
using SharpEngine.Widget;
using tainicom.Aether.Physics2D.Dynamics;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        var e1 = new Entity();
        e1.AddComponent(new TransformComponent(new Vec2(100)));
        e1.AddComponent(new RectComponent(Color.Blue, new Vec2(50)));
        e1.AddComponent(new PhysicsComponent(fixedRotation: true, ignoreGravity: true, debugDraw: true))
            .AddCircleCollision(50, restitution: 0f);
        e1.AddComponent(new ControlComponent(speed: 300));
        AddEntity(e1);

        for (var x = 0; x < 2; x++)
        {
            for (var y = 0; y < 2; y++)
            {
                var e2 = new Entity();
                e2.AddComponent(new TransformComponent(new Vec2(240 + 120*x, 80 + 120*y)));
                e2.AddComponent(new RectComponent(Color.Red, new Vec2(50)));
                e2.AddComponent(new PhysicsComponent(BodyType.Static, true, true, true))
                    .AddRectangleCollision(new Vec2(50), restitution: 0f);
                AddEntity(e2);
            }
        }

        AddWidget(new Selector(new Vec2(500), new List<string> { "Un", "Deux", "SUPPPPERRR TROIS !" }, "basic")).ValueChanged += 
            (_, args) => Console.WriteLine(args.OldValue + " => " + args.NewValue);

        var e3 = new Entity();
        e3.AddComponent(new TransformComponent(new Vec2(100, 500), new Vec2(3)));
        e3.AddComponent(new SpriteSheetComponent("KnightM", new Vec2(16, 28), new List<Animation>
        {
            new("idle", new List<uint> { 1, 2, 3, 4 }, 0.1f)
        }, "idle", flipX: true));
        AddEntity(e3);
    }

    public override void OpenScene()
    {
        base.OpenScene();
        Window!.CameraManager.FollowEntity = Entities[0];
    }

    public override void Update(float delta)
    {
        base.Update(delta);

        if (InputManager.IsKeyPressed(Key.A))
            Window!.CameraManager.Mode =
                Window!.CameraManager.Mode == CameraMode.Follow ? CameraMode.Basic : CameraMode.Follow;
    }
}
