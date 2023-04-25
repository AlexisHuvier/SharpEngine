using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.TileMap;

public class Tile
{
    public Vec2 Position { get; }
    public TileType? Type { get; }

    public Tile(Vec2 position, TileType? type)
    {
        Position = position;
        Type = type;
    }
}