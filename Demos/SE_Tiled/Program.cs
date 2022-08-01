using SharpEngine;
using SharpEngine.Utils;

namespace SE_Tiled;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(900, 600), Color.CornflowerBlue);

        win.TextureManager.AddTexture("sprite0", "Resources/sprite0.png");
        
        win.AddScene(new MyScene());
        win.Run();
    }
}
