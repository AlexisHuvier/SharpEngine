using SharpEngine;

namespace SharpEngineTest
{
    class Program
    {
        static bool DontStart() => false;

        static void Main(string[] args)
        {
            Window win = new Window(new Vec2(900, 600), Color.CORNFLOWER_BLUE);

            win.textureManager.AddTexture("test", "test.png");
            win.textureManager.AddTexture("spritesheet", "spritesheet.png");
            win.textureManager.AddTexture("flamme", "flamme.gif");
            win.fontManager.AddFont("arial", "C:\\Windows\\Fonts\\arial.ttf");

            win.startCallback = DontStart;

            win.AddScene(new MyScene());
            win.Run();
        }
    }
}
