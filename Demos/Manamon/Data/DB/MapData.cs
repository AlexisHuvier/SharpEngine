using SharpEngine.Utils.Math;

namespace Manamon.Data.DB;

public struct MapData
{
    public Vec2 Position { get; set; }
    public string Map { get; set; }
    public Dictionary<Vec2, string> Enemies { get; set; }

    private MapData(Vec2 position, string map, Dictionary<Vec2, string> enemies)
    {
        Position = position;
        Map = map;
        Enemies = enemies;
    }

    private static readonly List<MapData> Types = new()
    {
        new MapData(new Vec2(0), "map", new Dictionary<Vec2, string>()),
        new MapData(new Vec2(1, 0), "map2", new Dictionary<Vec2, string>()
        {
            { new Vec2(200), "Liwä" }
        })
    };

    public static MapData? GetMapByPosition(Vec2 position) => Types.FirstOrDefault(x => x.Position == position);
}