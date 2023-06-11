using ImGuiNET;
using SharpEngine;
using SharpEngine.Manager;
using SharpEngine.Utils;
using SharpEngine.Utils.File.Save;

namespace SE_BasicWindow;

internal static class Program
{
    private static void Main()
    {
        var window = new Window(800, 600, "SE Raylib", Color.CornflowerBlue, true)
        {
            RenderImGui = win =>
            {
                DebugManager.CreateSeImGuiWindow();
                ImGui.Begin("Basic Game Debug");
                if(ImGui.Button("Stop Window"))
                    win.Stop();
                if(ImGui.Button("Screenshot"))
                    win.TakeScreenshot("test.png");
                ImGui.End();
            }
        };
        
        window.TextureManager.AddTexture("KnightM", "Resources/KnightM.png");
        window.FontManager.AddFont("basic", "Resources/basic.ttf", 50);
        
        window.AddScene(new MyScene());
        
        window.Run();
    }
}