using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.TileMap;

public class TileType
{
    public readonly uint Id;
    public readonly string Source;
    public readonly Rect SourceRect;

    public TileType(uint id, string source, Rect sourceRect)
    {
        Id = id;
        Source = source;
        SourceRect = sourceRect;
    }
}