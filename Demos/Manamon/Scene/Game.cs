using Manamon.Data;
using Manamon.Data.DB;
using Manamon.Entity;
using SharpEngine.Components;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace Manamon.Scene;

public class Game: SharpEngine.Scene
{
    public Vec2 CurrentMap = Vec2.Zero;
    public readonly List<EnemyEntity> Enemies = new();
    
    private readonly MapEntity _map;

    public Game()
    {
        _map = AddEntity(new MapEntity());
        AddEntity(new PlayerEntity());
        
        AddWidget(new Button(new Vec2(600, 800), "Menu", "basic", new Vec2(200, 50))).Command =
            button => button.GetWindow().IndexCurrentScene = 0;
    }

    public bool NextMap(int direction)
    {
        var newMap = direction switch
        {
            0 => CurrentMap + new Vec2(-1, 0),
            1 => CurrentMap + new Vec2(1, 0),
            2 => CurrentMap + new Vec2(0, -1),
            3 => CurrentMap + new Vec2(0, 1),
            _ => CurrentMap
        };

        if (MapData.GetMapByPosition(newMap) is { Map: not null } map)
        {
            var tileMapComponent = _map.GetComponent<TileMapComponent>();
            tileMapComponent.TileMap = map.Map;
            CurrentMap = newMap;

            foreach (var enemy in Enemies)
                RemoveEntity(enemy);
            Enemies.Clear();
            foreach (var enemy in map.Enemies)
            {
                var entity = AddEntity(new EnemyEntity(enemy.Key, new Vec2(0.75f), enemy.Value));
                entity.Initialize();
                Enemies.Add(entity);
            }

            return true;
        }

        return false;
    }
}