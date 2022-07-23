using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Managers;
using SharpEngine.Utils;

namespace SE_BasicPhysics;

internal sealed class MyScene : Scene
{
    public MyScene()
    {
        var ent = new Entity();
        ent.AddComponent(new TransformComponent(new Vec2(420, 300)));
        ent.AddComponent(new SpriteComponent("test"));
        ent.AddComponent(new PhysicsComponent()).AddRectangleCollision(new Vec2(44));
        AddEntity(ent);

        var ent3 = new Entity();
        ent3.AddComponent(new TransformComponent(new Vec2(420, 300)));
        ent3.AddComponent(new SpriteComponent("test"));
        ent3.AddComponent(new ControlComponent(ControlType.FourDirection));
        ent3.AddComponent(new PhysicsComponent(tainicom.Aether.Physics2D.Dynamics.BodyType.Static))
            .AddRectangleCollision(new Vec2(44));
        AddEntity(ent3);

        var e2 = new Entity();
        e2.AddComponent(new TransformComponent(new Vec2(450, 500)));
        e2.AddComponent(new SpriteComponent("test"));
        e2.AddComponent(new PhysicsComponent(tainicom.Aether.Physics2D.Dynamics.BodyType.Static))
            .AddRectangleCollision(new Vec2(44));
        AddEntity(e2);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
            Entities[0].GetComponent<PhysicsComponent>().SetPosition(new Vec2(420, 300));
    }
}
