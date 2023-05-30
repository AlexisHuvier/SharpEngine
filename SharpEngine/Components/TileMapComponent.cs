using System;
using System.Collections.Generic;
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
    private Texture2D _internalTexture;
    private bool _loaded;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="tilemap">Chemin vers la tilemap</param>
    /// <exception cref="Exception"></exception>
    public TileMapComponent(string tilemap)
    {
        TileMap = tilemap;
    }

    private void UpdateTileMap(string tilemap)
    {
        Map = GetWindow().TileMapManager.GetMap(tilemap);
        
        _layers = new List<Layer>();
        
        if (Map.Map.Layers.Count == 0) return;

        foreach (var layer in Map.Map.Layers)
        {
            if (layer.Data == null)
                continue;
            
            var lay = new Layer();
            for (var i = 0; i < layer.Data!.Tiles.Count; i++)
            {
                var position =
                    new Vec2(
                        Map.TileSize.X * Convert.ToInt32(i % Convert.ToInt32(Map.Size.X)),
                        Map.TileSize.Y * Convert.ToInt32(i / Convert.ToInt32(Map.Size.X)));
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
                        x * Map.TileSize.X + Map.TileSize.X * Convert.ToInt32(i % Convert.ToInt32(chunk.Width)),
                        y * Map.TileSize.Y + Map.TileSize.Y * Convert.ToInt32(i / Convert.ToInt32(chunk.Width)));
                    chun.Tiles.Add(new Tile(position, Map.GetTile(chunk.Tiles[i])));
                }
                lay.Chunks.Add(chun);
            }
            _layers.Add(lay);
        }
        
        if(_loaded)
            UpdateTexture();
    }

    public override void Initialize()
    {
        _transformComponent = Entity.GetComponent<TransformComponent>();
        UpdateTileMap(_tilemap);
    }

    public override void LoadContent()
    {
        base.LoadContent();
        UpdateTexture();
        _loaded = true;
    }

    public void UpdateTexture()
    {
        var target2D = new RenderTarget2D(Entity.Scene.Window.InternalGame.GraphicsDevice,
            (int)(Map.TileSize.X * Map.Size.X), (int)(Map.TileSize.Y * Map.Size.Y), false,
            GetWindow().InternalGame.GraphicsDevice.PresentationParameters.BackBufferFormat,
            GetWindow().InternalGame.GraphicsDevice.PresentationParameters.DepthStencilFormat);
        
        Entity.Scene.Window.InternalGame.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
        Entity.Scene.Window.InternalGame.GraphicsDevice.SetRenderTarget(target2D);
        Entity.Scene.Window.InternalGame.GraphicsDevice.Clear(Color.Transparent);
        
        var layerNb = 0;
        foreach (var layer in _layers)
        {
            foreach (var tile in layer.Tiles)
            {
                if(tile.Type == null) continue;
                
                var texture = Entity.Scene.Window.TextureManager.GetTexture(tile.Type.Value.Source);
                Renderer.RenderTexture(Entity.Scene.Window, texture, tile.Position,
                    tile.Type.Value.SourceRect, Color.White, 0, Vec2.Zero, Vec2.One, 
                    SpriteEffects.None, layerNb * 0.0001f, true);
            }

            foreach (var chunk in layer.Chunks)
            {
                foreach (var tile in chunk.Tiles)
                {
                    if(tile.Type == null) continue;
                    
                    var texture = Entity.Scene.Window.TextureManager.GetTexture(tile.Type.Value.Source);
                    Renderer.RenderTexture(Entity.Scene.Window, texture, tile.Position,
                        tile.Type.Value.SourceRect, Color.White, 0, Vec2.Zero, Vec2.One,
                        SpriteEffects.None, layerNb * 0.0001f, true);
                }
            }

            layerNb++;
        }

        Entity.Scene.Window.InternalGame.SpriteBatch.End();
        Entity.Scene.Window.InternalGame.SpriteBatch.Begin();
        Entity.Scene.Window.InternalGame.GraphicsDevice.SetRenderTarget(null);
        Entity.Scene.Window.InternalGame.SpriteBatch.End();
        _internalTexture = target2D;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        if (_transformComponent == null || _layers.Count == 0) return;

        var originPosition = new Vec2(Map.TileSize.X * Map.Size.X / 2, Map.TileSize.Y * Map.Size.Y / 2);
        Renderer.RenderTexture(Entity.Scene.Window, _internalTexture,
            _transformComponent.Position - CameraManager.Position, null, Color.White, 0, originPosition,
            _transformComponent.Scale, SpriteEffects.None, _transformComponent.LayerDepth);


    }

    public override string ToString() => $"TileMapComponent(Tilemap={Map})";
}
