using SharpEngine;
using SharpEngine.Utils;

namespace SE_BasicPhysics;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(900, 600), Color.CornflowerBlue);

        win.TextureManager.AddTexture("test", "Resources/test.png");

        win.AddScene(new MyScene());
        win.Run();
    }
}