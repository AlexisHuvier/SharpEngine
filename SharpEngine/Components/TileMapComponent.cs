using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;

namespace SharpEngine.Components;

/// <summary>
/// Composant qui affiche une tilemap de Tiled (.tmx)
/// </summary>
public class TileMapComponent: Component
{
    private readonly string _orientation;
    private readonly string _renderOrder;
    private readonly Vec2 _size;
    private readonly Vec2 _tileSize;
    private readonly bool _infinite;
    private readonly List<TileUtils.Tile> _tiles;
    private readonly List<TileUtils.Layer> _layers;
    private readonly List<string> _textures;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="tilemap">Chemin vers la tilemap</param>
    /// <exception cref="Exception"></exception>
    public TileMapComponent(string tilemap)
    {
        _tiles = new List<TileUtils.Tile>();
        _layers = new List<TileUtils.Layer>();
        _textures = new List<string>();

        var file = XElement.Load(tilemap);

        _orientation = file.Attribute("orientation")?.Value;
        if (_orientation != "orthogonal")
            throw new Exception("SharpEngine can only use orthogonal tilemap.");
        _renderOrder = file.Attribute("renderorder")?.Value;
        if (_renderOrder != "right-down")
            throw new Exception("SharpEngine can only use tilemap with right-down renderorder");
        _size = new Vec2(Convert.ToInt32(file.Attribute("width")?.Value), Convert.ToInt32(file.Attribute("height")?.Value));
        _tileSize = new Vec2(Convert.ToInt32(file.Attribute("tilewidth")?.Value), Convert.ToInt32(file.Attribute("tileheight")?.Value));
        _infinite = Convert.ToBoolean(Convert.ToInt32(file.Attribute("infinite")?.Value));
        if(_infinite)
            throw new Exception("SharpEngine can only use non-infinite tilemap.");

        foreach(var tiletype in file.Element("tileset")?.Elements("tile")!)
        {
            var tile = new TileUtils.Tile()
            {
                Id = Convert.ToInt32(tiletype.Attribute("id")?.Value) + 1,
                Source = System.IO.Path.GetFileNameWithoutExtension(tiletype.Element("image")?.Attribute("source")?.Value)
            };
            _textures.Add(System.IO.Path.GetDirectoryName(tilemap) + System.IO.Path.DirectorySeparatorChar + tiletype.Element("image")?.Attribute("source")?.Value);
            _tiles.Add(tile);
        }

        foreach(var element in file.Elements("layer"))
        {
            var tiles = element.Element("data")?.Value.Split(",")!.Select(tile => Convert.ToInt32(tile)).ToList() ;
            var layer = new TileUtils.Layer
            {
                Tiles = tiles
            };
            _layers.Add(layer);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        for(var i = _textures.Count - 1; i > -1; i--) {
            GetWindow().TextureManager.AddTexture(System.IO.Path.GetFileNameWithoutExtension(_textures[i]), _textures[i]);
            _textures.RemoveAt(i);
        }
    }

    public TileUtils.Tile GetTile(int id)
    {
        foreach (var tile in _tiles.Where(tile => tile.Id == id))
            return tile;
        
        return new TileUtils.Tile();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Entity.GetComponent<TransformComponent>() is not { } tc || _layers.Count <= 0) return;
        
        foreach(var layer in _layers)
        {
            for(var i = 0; i < layer.Tiles.Count; i++)
            {
                if (layer.Tiles[i] == 0) continue;
                
                var position = tc.Position - _tileSize * _size / 2 - CameraManager.Position + new Vec2(_tileSize.X * Convert.ToInt32(i % Convert.ToInt32(_size.X)), _tileSize.Y * Convert.ToInt32(i / Convert.ToInt32(_size.Y)));
                var texture = Entity.Scene.Window.TextureManager.GetTexture(GetTile(layer.Tiles[i]).Source);
                Renderer.RenderTexture(Entity.Scene.Window, texture, position, null, Color.White, 0, _tileSize / 2, new Vec2(1), SpriteEffects.None, 1);
            }
        }
    }

    public override string ToString() => $"TileMapComponent(orientation={_orientation}, renderorder={_renderOrder}, size={_size}, tileSize={_tileSize}, infinite={_infinite})";
}
