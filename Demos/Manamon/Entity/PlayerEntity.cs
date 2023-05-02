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
        AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true)).AddRectangleCollision(PlayerSize);
        AddComponent(new OutOfWindowComponent(direction => ((Game)GetScene()).NextMap(direction)));
    }
}