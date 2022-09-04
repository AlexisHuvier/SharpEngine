using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.TileMap;

public class Tile
{
    public readonly Vec2 Position;
    public readonly uint Type;

    public Tile(Vec2 position, uint type)
    {
        Position = position;
        Type = type;
    }
}