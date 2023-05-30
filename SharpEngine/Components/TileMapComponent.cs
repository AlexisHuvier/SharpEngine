using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.TileMap;

namespace SharpEngine.Components;

/// <summary>
/// Composant qui affiche une tilemap de Tiled (.tmx)
/// </summary>
public class TileMapComponent: Component
{
    public TileMap Map { get; private set; }

    public string TileMap
    {
        get => _tilemap;
        set
        {
            _tilemap = value;
            if(GetWindow() != null)
                UpdateTileMap(_tilemap);
        }
    }
    
    private List<Layer> _layers = new();
    private string _tilemap;
    private TransformComponent _transformComponent;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="tilemap">Chemin vers la tilemap</param>
    /// <exception cref="Exception"></exception>
    public TileMapComponent(string tilemap)
    {
        TileMap = tilemap;
    }

    public void UpdateTileMap(string tilemap)
    {
        Map = GetWindow().TileMapManager.GetMap(tilemap);
        
        _layers = new List<Layer>();
        
        if (_transformComponent == null || Map.Map.Layers.Count == 0) return;

        var compPosition = Map.Map.Infinite
            ? _transformComponent.Position
            : new Vec2(_transformComponent.Position.X - Map.TileSize.X * Map.Size.X * _transformComponent.Scale.X / 2,
                _transformComponent.Position.Y - Map.TileSize.Y * Map.Size.Y * _transformComponent.Scale.Y / 2);

        foreach (var layer in Map.Map.Layers.Where(layer => layer.Data != null))
        {
            var lay = new Layer();
            for (var i = 0; i < layer.Data!.Tiles.Count; i++)
            {
                var position =
                    new Vec2(
                        compPosition.X + Map.TileSize.X * _transformComponent.Scale.X * Convert.ToInt32(i % Convert.ToInt32(Map.Size.X)),
                        compPosition.Y + Map.TileSize.Y * _transformComponent.Scale.Y * Convert.ToInt32(i / Convert.ToInt32(Map.Size.X)));
                lay.Tiles.Add(new Tile(position, Map.GetTile(layer.Data.Tiles[i])));
            }

            foreach (var chunk in layer.Data!.Chunks)
            {
                var chun = new Chunk();
                var x = chunk.X;
                var y = chunk.Y;
                for (var i = 0; i < chunk.Tiles.Count; i++)
                {
                    var position = new Vec2(
                        compPosition.X + x * Map.TileSize.X * _transformComponent.Scale.X +
                        Map.TileSize.X * _transformComponent.Scale.X * Convert.ToInt32(i % Convert.ToInt32(chunk.Width)),
                        compPosition.Y + y * Map.TileSize.Y * _transformComponent.Scale.Y +
                        Map.TileSize.Y * _transformComponent.Scale.Y * Convert.ToInt32(i / Convert.ToInt32(chunk.Width)));
                    chun.Tiles.Add(new Tile(position, Map.GetTile(chunk.Tiles[i])));
                }
                lay.Chunks.Add(chun);
            }
            _layers.Add(lay);
        }
    }

    public override void Initialize()
    {
        _transformComponent = Entity.GetComponent<TransformComponent>();
        UpdateTileMap(_tilemap);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        if (_transformComponent == null || _layers.Count == 0) return;
        
        var originPosition = new Vec2(
            Map.TileSize.X * _transformComponent.Scale.X / 2,
            Map.TileSize.Y * _transformComponent.Scale.Y / 2
            );

        var layerNb = 0;
        foreach (var layer in _layers)
        {
            foreach (var tile in layer.Tiles)
            {
                if(tile.Type == null) continue;
                
                var texture = Entity.Scene.Window.TextureManager.GetTexture(tile.Type.Value.Source);
                Renderer.RenderTexture(Entity.Scene.Window, texture, tile.Position - CameraManager.Position,
                    tile.Type.Value.SourceRect, Color.White, 0, originPosition, _transformComponent.Scale,
                    SpriteEffects.None, _transformComponent.LayerDepth + 0.00001f * layerNb);
            }

            foreach (var chunk in layer.Chunks)
            {
                foreach (var tile in chunk.Tiles)
                {
                    if(tile.Type == null) continue;
                    
                    var texture = Entity.Scene.Window.TextureManager.GetTexture(tile.Type.Value.Source);
                    Renderer.RenderTexture(Entity.Scene.Window, texture, tile.Position - CameraManager.Position,
                        tile.Type.Value.SourceRect, Color.White, 0, originPosition, _transformComponent.Scale,
                        SpriteEffects.None, _transformComponent.LayerDepth + 0.00001f * layerNb);
                }
            }

            layerNb++;
        }

    }

    public override string ToString() => $"TileMapComponent(Tilemap={Map})";
}
