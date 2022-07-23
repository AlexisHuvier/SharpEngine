using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils;

namespace SE_Tiled;
public sealed class MyScene : Scene
{
    public MyScene()
    {
        var ent = new Entity();
        ent.AddComponent(new TransformComponent(new Vec2(420, 300)));
        ent.AddComponent(new TileMapComponent("Resources/map.tmx"));
        AddEntity(ent);
    }
}