using System.Collections.Generic;
using SharpEngine.Utils.Math;

namespace SharpEngine.Utils;

public static class TileUtils
{
    public struct Tile
    {
        public int Id;
        public string Source;
        public Rect SourceRect;
    }

    public struct Layer
    {
        public List<int> Tiles;
    }
}
