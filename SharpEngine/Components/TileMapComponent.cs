using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpEngine.Managers;
using SharpEngine.Utils;

namespace SharpEngine.Components;

/// <summary>
/// Composant qui affiche une tilemap de Tiled (.tmx)
/// </summary>
public class TileMapComponent: Component
{
    protected string Orientation;
    protected string Renderorder;
    protected Vec2 Size;
    protected Vec2 TileSize;
    protected bool Infinite;
    protected List<TileUtils.Tile> Tiles;
    protected List<TileUtils.Layer> Layers;
    protected List<string> Textures;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="tilemap">Chemin vers la tilemap</param>
    /// <exception cref="Exception"></exception>
    public TileMapComponent(string tilemap)
    {
        Tiles = new List<TileUtils.Tile>();
        Layers = new List<TileUtils.Layer>();
        Textures = new List<string>();

        var file = XElement.Load(tilemap);

        Orientation = file.Attribute("orientation")?.Value;
        if (Orientation != "orthogonal")
            throw new Exception("SharpEngine can only use orthogonal tilemap.");
        Renderorder = file.Attribute("renderorder")?.Value;
        if (Renderorder != "right-down")
            throw new Exception("SharpEngine can only use tilemap with right-down renderorder");
        Size = new Vec2(Convert.ToInt32(file.Attribute("width")?.Value), Convert.ToInt32(file.Attribute("height")?.Value));
        TileSize = new Vec2(Convert.ToInt32(file.Attribute("tilewidth")?.Value), Convert.ToInt32(file.Attribute("tileheight")?.Value));
        Infinite = Convert.ToBoolean(Convert.ToInt32(file.Attribute("infinite")?.Value));
        if(Infinite)
            throw new Exception("SharpEngine can only use non-infinite tilemap.");

        foreach(var tiletype in file.Element("tileset")?.Elements("tile")!)
        {
            var tile = new TileUtils.Tile()
            {
                Id = Convert.ToInt32(tiletype.Attribute("id")?.Value) + 1,
                Source = System.IO.Path.GetFileNameWithoutExtension(tiletype.Element("image")?.Attribute("source")?.Value)
            };
            Textures.Add(System.IO.Path.GetDirectoryName(tilemap) + System.IO.Path.DirectorySeparatorChar + tiletype.Element("image")?.Attribute("source")?.Value);
            Tiles.Add(tile);
        }

        foreach(var element in file.Elements("layer"))
        {
            var tiles = ((element.Element("data")?.Value.Split(",")!.Select(tile => Convert.ToInt32(tile))) ?? Array.Empty<int>()).ToList();
            var layer = new TileUtils.Layer
            {
                Tiles = tiles
            };
            Layers.Add(layer);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        for(var i = Textures.Count - 1; i > -1; i--) {
            GetWindow().TextureManager.AddTexture(System.IO.Path.GetFileNameWithoutExtension(Textures[i]), Textures[i]);
            Textures.RemoveAt(i);
        }
    }

    public TileUtils.Tile GetTile(int id)
    {
        foreach (var tile in Tiles.Where(tile => tile.Id == id))
            return tile;
        
        return new TileUtils.Tile();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Entity.GetComponent<TransformComponent>() is not { } tc || Layers.Count <= 0) return;
        
        foreach(var layer in Layers)
        {
            for(var i = 0; i < layer.Tiles.Count; i++)
            {
                if (layer.Tiles[i] == 0) continue;
                    
                var position = tc.Position - TileSize * Size / 2 - CameraManager.Position + new Vec2(TileSize.X * Convert.ToInt32(i % Convert.ToInt32(Size.X)), TileSize.Y * Convert.ToInt32(i / Convert.ToInt32(Size.Y)));
                GetSpriteBatch().Draw(GetWindow().TextureManager.GetTexture(GetTile(layer.Tiles[i]).Source), new Rect(position, TileSize).ToMg(), Color.White.ToMg());
            }
        }
    }

    public override string ToString() => $"TileMapComponent(orientation={Orientation}, renderorder={Renderorder}, size={Size}, tileSize={TileSize}, infinite={Infinite})";
}
