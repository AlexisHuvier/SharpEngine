using ImGuiNET;
using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(900, 600), Color.CornflowerBlue, debug: true)
        {
            RenderImGui = _ =>
            {
                {
                    ImGui.Text($"SE Version : {DebugManager.GetSharpEngineVersion()}");
                    ImGui.Text($"Monogame Version : {DebugManager.GetMonogameVersion()}");
                    ImGui.Text($"FPS : {DebugManager.GetFps()}");
                    ImGui.Text($"GC Memory : {DebugManager.GetGcMemory() / 1024} ko");
                }
            }
        };
        
        win.FontManager.AddFont("basic", "Resources/basic.ttf");
        win.TextureManager.AddTexture("KnightM", "Resources/KnightM.png");

        win.AddScene(new MyScene());
        win.Run();
    }
}