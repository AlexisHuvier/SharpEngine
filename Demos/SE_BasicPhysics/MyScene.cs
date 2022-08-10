using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SE_BasicPhysics;

internal sealed class MyScene : Scene
{
    public MyScene()
    {
        var ent2 = new Entity();
        ent2.AddComponent(new TransformComponent(new Vec2(420, 100)));
        ent2.AddComponent(new SpriteComponent("test"));
        ent2.AddComponent(new PhysicsComponent()).AddRectangleCollision(new Vec2(44), restitution: 1f);
        AddEntity(ent2);
        
        var ent = new Entity();
        ent.AddComponent(new TransformComponent(new Vec2(420, 50)));
        ent.AddComponent(new SpriteComponent("test"));
        var phys = ent.AddComponent(new PhysicsComponent());
        phys.AddRectangleCollision(new Vec2(44), restitution: 1f);
        phys.AddJoin(ent2);
        AddEntity(ent);

        for (var x = 20; x < 900; x += 150)
        {
            for (var y = 150; y < 600; y += 150)
            {
                var e2 = new Entity();
                e2.AddComponent(new TransformComponent(new Vec2(y % 300 == 0 ? x : x + 75 , y + 50)));
                e2.AddComponent(new SpriteComponent("test"));
                e2.AddComponent(new PhysicsComponent(tainicom.Aether.Physics2D.Dynamics.BodyType.Static))
                    .AddRectangleCollision(new Vec2(44));
                AddEntity(e2);
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
        {
            Entities[0].GetComponent<PhysicsComponent>().SetPosition(new Vec2(420, 100));
            Entities[1].GetComponent<PhysicsComponent>().SetPosition(new Vec2(420, 50));
        }
    }
}
