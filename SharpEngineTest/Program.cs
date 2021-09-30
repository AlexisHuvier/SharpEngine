using SharpEngine;
using System;

namespace SharpEngineTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Window win = new Window(new Vec2(900, 600), Color.CORNFLOWER_BLUE);

            win.textureManager.AddTexture("test", "test.png");
            win.fontManager.AddFont("arial", "C:\\Windows\\Fonts\\arial.ttf");

            win.AddScene(new MyScene());
            win.Run();
        }
    }
}
