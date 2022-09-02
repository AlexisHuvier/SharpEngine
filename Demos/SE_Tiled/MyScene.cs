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
        tilemap.AddComponent(new TileMapComponent("map"));
        AddEntity(tilemap);
        
        var tilemapTileset = new Entity();
        tilemapTileset.AddComponent(new TransformComponent(new Vec2(620, 300), scale: new Vec2(2)));
        tilemapTileset.AddComponent(new TileMapComponent("map_tileset"));
        AddEntity(tilemapTileset);

        var tilemapInfinite = new Entity();
        tilemapInfinite.AddComponent(new TransformComponent(new Vec2(420, 600)));
        tilemapInfinite.AddComponent(new TileMapComponent("map_infinite"));
        AddEntity(tilemapInfinite);

        var player = new Entity();
        player.AddComponent(new TransformComponent(new Vec2(420, 300)));
        player.AddComponent(new SpriteComponent("sprite0"));
        player.AddComponent(new ControlComponent(ControlType.FourDirection));
        AddEntity(player);

        CameraManager.FollowEntity = player;
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