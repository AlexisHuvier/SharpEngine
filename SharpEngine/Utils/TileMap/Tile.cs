using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.TileMap;

public class Tile
{
    public Vec2 Position { get; }
    public uint Type { get; }

    public Tile(Vec2 position, uint type)
    {
        Position = position;
        Type = type;
    }
}