using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.TileMap;

public readonly struct Tile
{
    public readonly Vec2 Position;
    public readonly TileType? Type;

    public Tile(Vec2 position, TileType? type)
    {
        Position = position;
        Type = type;
    }
}