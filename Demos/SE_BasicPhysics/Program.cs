using SharpEngine;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SE_BasicPhysics;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(900, 600), Color.CornflowerBlue, debug: true, showPhysicDebugView: true)
        {
            RenderImGui = _ => DebugManager.CreateSharpEngineImGuiWindow()
        };

        win.TextureManager.AddTexture("test", "Resources/test.png");

        win.AddScene(new MyScene());
        win.Run();
    }
}