using System.Collections.Generic;

namespace SharpEngine.Utils.TileMap;

public class Layer
{
    public List<Chunk> Chunks { get; set; }
    public List<Tile> Tiles { get; set; }

    public Layer(List<Chunk> chunks, List<Tile> tiles)
    {
        Chunks = chunks;
        Tiles = tiles;
    }

    public Layer()
    {
        Chunks = new List<Chunk>();
        Tiles = new List<Tile>();
    }
}