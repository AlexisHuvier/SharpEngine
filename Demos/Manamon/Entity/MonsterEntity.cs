using Manamon.Data;
using SharpEngine.Components;
using SharpEngine.Utils.Math;

namespace Manamon.Entity;

public class MonsterEntity: SharpEngine.Entities.Entity
{
    private readonly Monster _monster;
    
    public MonsterEntity(Vec2 position, Monster monster)
    {
        _monster = monster;
        
        AddComponent(new TransformComponent(position));
        AddComponent(new SpriteComponent(_monster.Data.Name));
    }
}