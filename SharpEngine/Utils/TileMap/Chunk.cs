using System.Collections.Generic;

namespace SharpEngine.Utils.TileMap;

public readonly struct Chunk
{
    public readonly List<Tile> Tiles;

    public Chunk(List<Tile> tiles)
    {
        Tiles = tiles;
    }

    public Chunk()
    {
        Tiles = new List<Tile>();
    }
}