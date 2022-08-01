using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Managers;
using SharpEngine.Utils;

namespace SE_Tiled;
public sealed class MyScene : Scene
{
    public MyScene()
    {
        var ent = new Entity();
        ent.AddComponent(new TransformComponent(new Vec2(420, 300)));
        ent.AddComponent(new TileMapComponent("Resources/map.tmx"));
        AddEntity(ent);

        var ent2 = new Entity();
        ent2.AddComponent(new TransformComponent(new Vec2(420, 300)));
        ent2.AddComponent(new ControlComponent(ControlType.FourDirection));
        ent2.AddComponent(new SpriteComponent("sprite0"));
        AddEntity(ent2);

        CameraManager.FollowEntity = ent2;
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