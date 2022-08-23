using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Managers;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SE_Tiled;
public sealed class MyScene : Scene
{
    public MyScene()
    {
        var tilemap = new Entity();
        tilemap.AddComponent(new TransformComponent(new Vec2(220, 300)));
        tilemap.AddComponent(new TileMapComponent("Resources/map.tmx"));
        AddEntity(tilemap);
        
        var tilemapTileset = new Entity();
        tilemapTileset.AddComponent(new TransformComponent(new Vec2(620, 300)));
        tilemapTileset.AddComponent(new TileMapComponent("Resources/map_tileset.tmx"));
        AddEntity(tilemapTileset);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!InputManager.IsKeyPressed(Key.A)) return;
        
        Console.WriteLine($"SE Version : {DebugManager.GetSharpEngineVersion()}");
        Console.WriteLine($"Monogame Version : {DebugManager.GetMonogameVersion()}");
        Console.WriteLine($"FPS : {DebugManager.GetFps()}");
        Console.WriteLine($"GC Memory : {DebugManager.GetGcMemory()}");
    }
}