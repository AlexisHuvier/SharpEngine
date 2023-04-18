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
    public TileMap Map { get; internal set; }

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
        
        if (Entity.GetComponent<TransformComponent>() is not { } tc || Map.Map.Layers.Count == 0) return;
        
        var compPosition = Map.Map.Infinite ? tc.Position: tc.Position - Map.TileSize * Map.Size * tc.Scale / 2;

        foreach (var layer in Map.Map.Layers.Where(layer => layer.Data != null))
        {
            var lay = new Layer();
            for (var i = 0; i < layer.Data!.Tiles.Count; i++)
            {
                var position =
                    new Vec2(
                        compPosition.X + Map.TileSize.X * tc.Scale.X * Convert.ToInt32(i % Convert.ToInt32(Map.Size.X)),
                        compPosition.Y + Map.TileSize.Y * tc.Scale.Y * Convert.ToInt32(i / Convert.ToInt32(Map.Size.X)));
                lay.Tiles.Add(new Tile(position, layer.Data.Tiles[i]));
            }

            foreach (var chunk in layer.Data!.Chunks)
            {
                var chun = new Chunk();
                var x = chunk.X;
                var y = chunk.Y;
                for (var i = 0; i < chunk.Tiles.Count; i++)
                {
                    var position = new Vec2(
                        compPosition.X + x * Map.TileSize.X * tc.Scale.X +
                        Map.TileSize.X * tc.Scale.X * Convert.ToInt32(i % Convert.ToInt32(chunk.Width)),
                        compPosition.Y + y * Map.TileSize.Y * tc.Scale.Y +
                        Map.TileSize.Y * tc.Scale.Y * Convert.ToInt32(i / Convert.ToInt32(chunk.Width)));
                    chun.Tiles.Add(new Tile(position, chunk.Tiles[i]));
                }
                lay.Chunks.Add(chun);
            }
            _layers.Add(lay);
        }
    }

    public override void Initialize()
    {
        UpdateTileMap(_tilemap);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        if (Entity.GetComponent<TransformComponent>() is not { } tc || _layers.Count == 0) return;
        
        var originPosition = Map.TileSize * tc.Scale / 2;

        foreach (var layer in _layers)
        {
            foreach (var tile in layer.Tiles)
            {
                if(tile.Type == 0) continue;
                var tiletype = Map.GetTile(tile.Type);
                var texture = Entity.Scene.Window.TextureManager.GetTexture(tiletype?.Source);
                Renderer.RenderTexture(Entity.Scene.Window, texture, tile.Position - CameraManager.Position, tiletype?.SourceRect, Color.White, 0, originPosition, tc.Scale, SpriteEffects.None, 1);
            }

            foreach (var chunk in layer.Chunks)
            {
                foreach (var tile in chunk.Tiles)
                {
                    if(tile.Type == 0) continue;
                    var tiletype = Map.GetTile(tile.Type);
                    var texture = Entity.Scene.Window.TextureManager.GetTexture(tiletype?.Source);
                    Renderer.RenderTexture(Entity.Scene.Window, texture, tile.Position - CameraManager.Position, tiletype?.SourceRect, Color.White, 0, originPosition, tc.Scale, SpriteEffects.None, 1);
                }
            }
        }

    }

    public override string ToString() => $"TileMapComponent(Tilemap={Map})";
}
