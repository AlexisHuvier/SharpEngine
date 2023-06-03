using ImGuiNET;
using SharpEngine;
using SharpEngine.Manager;
using SharpEngine.Utils;

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
        window.Run();
    }
}