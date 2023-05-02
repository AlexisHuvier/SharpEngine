using Manamon.Data.DB;
using SharpEngine.Components;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;

namespace Manamon.Entity;

public class EnemyEntity: SharpEngine.Entities.Entity
{
    private readonly EnemyData _data;
    
    public EnemyEntity(Vec2 position, Vec2 scale, string data)
    {
        _data = EnemyData.GetTypeByName(data);
        AddComponent(new TransformComponent(position, scale));
        AddComponent(new SpriteComponent(_data.Name));
        AddComponent(new PhysicsComponent(BodyType.Static, true, true)).AddRectangleCollision(new Vec2(50, 100));
    }
}