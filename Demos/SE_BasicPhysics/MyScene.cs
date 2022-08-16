using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using tainicom.Aether.Physics2D.Dynamics;

namespace SE_BasicPhysics;

internal sealed class MyScene : Scene
{
    public MyScene()
    {
        var ent2 = new Entity();
        ent2.AddComponent(new TransformComponent(new Vec2(420, 100)));
        ent2.AddComponent(new SpriteComponent("test"));
        var temphys = ent2.AddComponent(new PhysicsComponent());
        temphys.AddRectangleCollision(new Vec2(44), restitution: 1f);
        temphys.CollisionCallback = (_, _, _) => false;
        AddEntity(ent2);
        
        var ent = new Entity();
        ent.AddComponent(new TransformComponent(new Vec2(420, 50)));
        ent.AddComponent(new SpriteComponent("test"));
        var phys = ent.AddComponent(new PhysicsComponent());
        phys.AddRectangleCollision(new Vec2(44), restitution: 1f);
        phys.AddJoin(new DistanceJoint(JointType.Distance, ent2));
        AddEntity(ent);
        
        
        var ent3 = new Entity();
        ent3.AddComponent(new TransformComponent(new Vec2(220, 100)));
        ent3.AddComponent(new SpriteComponent("test"));
        ent3.AddComponent(new PhysicsComponent()).AddRectangleCollision(new Vec2(44), restitution: 1f);
        AddEntity(ent3);
        
        var ent4 = new Entity();
        ent4.AddComponent(new TransformComponent(new Vec2(220, 50)));
        ent4.AddComponent(new SpriteComponent("test"));
        var phys2 = ent4.AddComponent(new PhysicsComponent());
        phys2.AddRectangleCollision(new Vec2(44), restitution: 1f);
        phys2.AddJoin(new DistanceJoint(JointType.Distance, ent3, 20));
        AddEntity(ent4);
        
        
        var ent5 = new Entity();
        ent5.AddComponent(new TransformComponent(new Vec2(620, 100)));
        ent5.AddComponent(new SpriteComponent("test"));
        ent5.AddComponent(new PhysicsComponent()).AddRectangleCollision(new Vec2(44), restitution: 1f);
        AddEntity(ent5);
        
        var ent6 = new Entity();
        ent6.AddComponent(new TransformComponent(new Vec2(620, 50)));
        ent6.AddComponent(new SpriteComponent("test"));
        var phys3 = ent6.AddComponent(new PhysicsComponent());
        phys3.AddRectangleCollision(new Vec2(44), restitution: 1f);
        phys3.AddJoin(new DistanceJoint(JointType.Distance, ent5, 100));
        AddEntity(ent6);

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
            Entities[2].GetComponent<PhysicsComponent>().SetPosition(new Vec2(220, 100));
            Entities[3].GetComponent<PhysicsComponent>().SetPosition(new Vec2(220, 50));
            Entities[4].GetComponent<PhysicsComponent>().SetPosition(new Vec2(620, 100));
            Entities[5].GetComponent<PhysicsComponent>().SetPosition(new Vec2(620, 50));
        }
        
        
        if (InputManager.IsKeyPressed(Key.A))
        {
            Console.WriteLine($"SE Version : {DebugManager.GetSharpEngineVersion()}");
            Console.WriteLine($"Monogame Version : {DebugManager.GetMonogameVersion()}");
            Console.WriteLine($"FPS : {DebugManager.GetFps()}");
            Console.WriteLine($"GC Memory : {DebugManager.GetGcMemory()}");
        }
    }
}
