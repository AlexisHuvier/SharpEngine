using ImGuiNET;
using Raylib_cs;
using SharpEngine;
using SharpEngine.Manager;
using Color = SharpEngine.Utils.Color;

namespace SE_BasicWindow;

internal static class Program
{
    private static void Main()
    {
        var window = new Window(800, 600, "SE Raylib", Color.CornflowerBlue, null, true)
        {
            RenderImGui = DebugManager.CreateSeImGuiWindow
        };
        
        window.TextureManager.AddTexture("KnightM", "Resources/KnightM.png");
        window.FontManager.AddFont("basic", "Resources/basic.ttf", 40);
        
        window.AddScene(new MyScene());
        
        window.Run();
    }
}