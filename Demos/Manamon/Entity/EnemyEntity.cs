using Manamon.Data;
using SharpEngine.Components;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;

namespace Manamon.Entity;

public class EnemyEntity: SharpEngine.Entities.Entity
{
    private readonly Enemy _enemy;
    
    public EnemyEntity(Vec2 position, Vec2 scale, string data)
    {
        _enemy = new Enemy(data);
        AddComponent(new TransformComponent(position, scale));
        AddComponent(new SpriteComponent(_enemy.Data.Name));
        AddComponent(new PhysicsComponent(BodyType.Static, true, true)).AddRectangleCollision(new Vec2(50, 100));
    }
}