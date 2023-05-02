using ImGuiNET;
using Manamon.Scene;
using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace Manamon;

internal static class Program
{
    private static void Main()
    {
        var window = new Window(new Vec2(1200, 900), Color.LightGray, debug: true)
        {
            RenderImGui = win =>
            {
                DebugManager.CreateSharpEngineImGuiWindow();
                {
                    ImGui.Begin("Manamon Information");
                    ImGui.Text($"Current Scene : {win.IndexCurrentScene}");
                    ImGui.Text($"Map Coords : {(win.IndexCurrentScene == 1 ? ((Game)win.CurrentScene).CurrentMap : "Nop")}");
                    ImGui.Text($"Map Name : {(win.IndexCurrentScene == 1 ? win.CurrentScene.GetEntities()[0].GetComponent<TileMapComponent>().TileMap : "Nop")}");
                    
                    ImGui.End();
                }
            }
        };
        
        window.TileMapManager.AddMap("map", "Resource/Tileset/map.tmx");
        window.TileMapManager.AddMap("map2", "Resource/Tileset/map2.tmx");
        
        window.FontManager.AddFont("title", "Resource/Fonts/basic.ttf", 75);
        window.FontManager.AddFont("basic", "Resource/Fonts/basic.ttf", 35);
        
        window.TextureManager.AddTexture("Liwä", "Resource/Images/Monsters/Liwä.png");
        window.TextureManager.AddTexture("Iolena", "Resource/Images/Monsters/Iolena.png");
        window.TextureManager.AddTexture("Lilith", "Resource/Images/Monsters/Lilith.png");
        
        window.AddScene(new MainMenu());
        window.AddScene(new Game());
        window.IndexCurrentScene = 0;
        
        window.Run();
    }
}