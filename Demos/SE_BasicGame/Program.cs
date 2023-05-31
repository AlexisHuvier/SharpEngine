using ImGuiNET;
using SharpEngine;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SE_BasicWindow;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(900, 600), Color.CornflowerBlue, debug: true)
        {
            RenderImGui = _ =>
            {
                DebugManager.CreateSharpEngineImGuiWindow();
                {
                    ImGui.Begin("Basic Game Information");
                    ImGui.Text($"Downed Keys : {string.Join(", ", InputManager.GetDownedKeys().Select(x => x.ToString()))}");
                    ImGui.End();
                }
            }
        };
        
        win.FontManager.AddFont("basic", "Resources/basic.ttf");
        win.TextureManager.AddTexture("KnightM", "Resources/KnightM.png");

        win.AddScene(new MyScene());
        win.Run();
    }
}