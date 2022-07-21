using System.Collections.Generic;

namespace SharpEngine.Utils;

public static class TileUtils
{
    public struct Tile
    {
        public int Id;
        public string Source;
    }

    public struct Layer
    {
        public List<int> Tiles;
    }
}
