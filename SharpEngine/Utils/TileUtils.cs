using System.Collections.Generic;

namespace SharpEngine.Utils
{
    public class TileUtils
    {
        public struct Tile
        {
            public int id;
            public string source;
        }

        public struct Layer
        {
            public List<int> tiles;
        }
    }
}
