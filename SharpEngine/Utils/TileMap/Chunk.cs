using System.Collections.Generic;

namespace SharpEngine.Utils.TileMap;

public class Chunk
{
    public List<Tile> Tiles;

    public Chunk(List<Tile> tiles)
    {
        Tiles = tiles;
    }

    public Chunk()
    {
        Tiles = new List<Tile>();
    }
}