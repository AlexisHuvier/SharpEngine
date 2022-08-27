using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Components;

/// <summary>
/// Composant qui affiche une tilemap de Tiled (.tmx)
/// </summary>
public class TileMapComponent: Component
{
    public string Map;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="tilemap">Chemin vers la tilemap</param>
    /// <exception cref="Exception"></exception>
    public TileMapComponent(string tilemap)
    {
        Map = tilemap;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        var tileMap = GetWindow().TileMapManager.GetMap(Map);

        if (Entity.GetComponent<TransformComponent>() is not { } tc || tileMap.Map.Layers.Count == 0) return;
        
        var compPosition = tc.Position - tileMap.TileSize * tileMap.Size / 2 - CameraManager.Position;
        var originPosition = tileMap.TileSize / 2;

        foreach (var layer in tileMap.Map.Layers.Where(layer => layer.Data != null))
        {
            for(var i = 0; i < layer.Data!.Tiles.Count; i++)
            {
                if (layer.Data.Tiles[i] == 0) continue;
                var tile = tileMap.GetTile(layer.Data.Tiles[i]);
                var position = new Vec2(compPosition.X + tileMap.TileSize.X * Convert.ToInt32(i % Convert.ToInt32(tileMap.Size.X)), compPosition.Y + tileMap.TileSize.Y * Convert.ToInt32(i / Convert.ToInt32(tileMap.Size.X)));
                var texture = Entity.Scene.Window.TextureManager.GetTexture(tile.Source);
                Renderer.RenderTexture(Entity.Scene.Window, texture, position, tile.SourceRect, Color.White, 0, originPosition, Vec2.One, SpriteEffects.None, 1);
            }

            foreach (var chunk in layer.Data!.Chunks)
            {
                var x = chunk.X;
                var y = chunk.Y;
                for (var i = 0; i < chunk.Tiles.Count; i++)
                {
                    if (chunk.Tiles[i] == 0) continue;
                    var tile = tileMap.GetTile(chunk.Tiles[i]);
                    var position = new Vec2(compPosition.X + x * chunk.Width + tileMap.TileSize.X * Convert.ToInt32(i % chunk.Width), compPosition.Y + y * chunk.Height + tileMap.TileSize.Y * Convert.ToInt32(i / chunk.Width));
                    var texture = Entity.Scene.Window.TextureManager.GetTexture(tile.Source);
                    Console.WriteLine($"{layer.Data!.Chunks.IndexOf(chunk)} => ({x*chunk.Width}x{y*chunk.Height}) - {position}");
                    Renderer.RenderTexture(Entity.Scene.Window, texture, position, tile.SourceRect, Color.White, layer.Data!.Chunks.IndexOf(chunk) == 0 ? 45: 0, originPosition, Vec2.One,SpriteEffects.None, 1);
                }
            }
        }

    }

    public override string ToString() => $"TileMapComponent(Tilemap={Map})";
}
