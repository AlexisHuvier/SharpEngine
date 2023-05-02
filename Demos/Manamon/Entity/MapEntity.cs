using Manamon.Data;
using SharpEngine.Components;
using SharpEngine.Utils.Math;

namespace Manamon.Entity;

public class MapEntity : SharpEngine.Entities.Entity
{
    public MapEntity()
    {
        AddComponent(new TransformComponent(new Vec2(630, 470), scale: new Vec2(2)));
        AddComponent(new TileMapComponent("map"));
    }
}