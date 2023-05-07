using Manamon.Component;
using Manamon.Scene;
using SharpEngine.Components;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace Manamon.Entity;

public class PlayerEntity: SharpEngine.Entities.Entity
{
    public readonly Vec2 PlayerSize = new(60, 120);

    public PlayerEntity()
    {
        AddComponent(new TransformComponent(new Vec2(200), new Vec2(0.75f)));
        AddComponent(new ControlComponent(ControlType.FourDirection));
        AddComponent(new SpriteComponent("Liwä"));
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(PlayerSize);
        phys.CollisionCallback = (_, other, _) =>
        {
            foreach (var enemy in ((Game)GetScene()).Enemies)
            {
                if (enemy.GetComponent<PhysicsComponent>() is { } physicsComponent &&
                    physicsComponent.Body == other.Body)
                {
                    ((Combat)GetScene().GetWindow().GetScene(2)).Init(enemy.Enemy);
                    GetScene().GetWindow().IndexCurrentScene = 2;
                    break;
                }
            }
            return true;
        };
        AddComponent(new OutOfWindowComponent(direction => ((Game)GetScene()).NextMap(direction)));
    }
}