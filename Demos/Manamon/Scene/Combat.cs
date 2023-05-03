using Manamon.Data;
using Manamon.Entity;
using SharpEngine.Utils.Math;

namespace Manamon.Scene;

public class Combat: SharpEngine.Scene
{
    public Combat()
    {
        
    }

    public void Init(Enemy enemy)
    {
        foreach (var entity in GetEntities())
            RemoveEntity(entity);

        AddEntity(new MonsterEntity(new Vec2(100), enemy.Team[0]));
    }
}