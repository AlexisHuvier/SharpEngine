using System.Collections.Generic;
using System.IO;
using SharpEngine.Utils.Math;
using TiledLoader;
using TiledLoader.Objects;

namespace SharpEngine.Utils.TileMap;

public class TileMap
{
    public TiledMap Map { get; }
    public List<TileType> Tiles { get; }
    public Vec2 Size { get; }
    public Vec2 TileSize { get; }
    
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
                uint nbXTile;
                uint nbYTile;
                if (tileset.Spacing == 0)
                {
                    nbXTile = tileset.Image.Width / tileset.TileWidth;
                    nbYTile = tileset.Image.Height / tileset.TileHeight;
                }
                else
                {
                    nbXTile = tileset.Image.Width / (tileset.TileWidth + tileset.Spacing) + 1;
                    nbYTile = tileset.Image.Height / (tileset.TileHeight + tileset.Spacing) + 1;
                }

                for (var y = 0; y < nbYTile; y++)
                {
                    for (var x = 0; x < nbXTile; x++)
                        Tiles.Add(new TileType((uint)(id + x + y * nbXTile), source,
                            new Rect(x * (tileset.TileWidth + tileset.Spacing), y * (tileset.TileHeight + tileset.Spacing), tileset.TileWidth,
                                tileset.TileHeight)));
                }
            }
        }
    }

    public TileType? GetTile(uint id)
    {
        foreach(var tile in Tiles)
            if (tile.Id == id)
                return tile;
        return null;
    }
}