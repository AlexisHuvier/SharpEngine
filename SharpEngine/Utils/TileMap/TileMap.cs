using System.Collections.Generic;
using System.IO;
using SharpEngine.Utils.Math;
using TiledLoader;
using TiledLoader.Objects;

namespace SharpEngine.Utils.TileMap;

public class TileMap
{
    public readonly TiledMap Map;
    public readonly List<TileType> Tiles;
    public readonly Vec2 Size;
    public readonly Vec2 TileSize;
    
    public TileMap(string map, Window window)
    {
        Map = new TiledMap(map);
        Size = new Vec2(Map.Width, Map.Height);
        TileSize = new Vec2(Map.TileWidth, Map.TileHeight);
        
        Tiles = new List<TileType>();
        var mapFolder = Path.GetDirectoryName(Map.File);
        foreach (var tileset in Map.Tilesets)
        {
            var id = tileset.FirstGId;

            foreach (var tile in tileset.Tiles)
            {
                if (tile.Image == null) continue;

                var source = Map.File + tile.Image.Source;
                window.TextureManager.AddTexture(source, mapFolder + Path.DirectorySeparatorChar + tile.Image.Source);
                Tiles.Add(new TileType(tile.Id + 1, source, null));
            }

            id += (uint)tileset.Tiles.Count;
            
            if (tileset.Image != null)
            {
                var source = Map.File + tileset.Image.Source;
                window.TextureManager.AddTexture(source, mapFolder + Path.DirectorySeparatorChar + tileset.Image.Source);
                var nbXTile = tileset.Image.Width / tileset.TileWidth;
                var nbYTile = tileset.Image.Height / tileset.TileHeight;
                for (var y = 0; y < nbYTile; y++)
                {
                    for (var x = 0; x < nbXTile; x++)
                        Tiles.Add(new TileType((uint)(id + x + y * nbXTile), source,
                            new Rect(x * tileset.TileWidth, y * tileset.TileHeight, tileset.TileWidth,
                                tileset.TileHeight)));
                }
            }
        }
    }

    public TileType GetTile(uint id)
    {
        foreach(var tile in Tiles)
            if (tile.Id == id)
                return tile;
        return null;
    }
}