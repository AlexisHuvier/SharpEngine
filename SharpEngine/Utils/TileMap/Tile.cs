using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.TileMap;

public class Tile
{
    public readonly uint Id;
    public readonly string Source;
    public readonly Rect SourceRect;

    public Tile(uint id, string source, Rect sourceRect)
    {
        Id = id;
        Source = source;
        SourceRect = sourceRect;
    }
}