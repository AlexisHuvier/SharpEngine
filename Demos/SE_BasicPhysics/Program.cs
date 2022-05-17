using SharpEngine;

namespace SE_BasicPhysics
{
    class Program
    {
        static void Main(string[] args)
        {
            Window win = new Window(new Vec2(900, 600), Color.CORNFLOWER_BLUE);

            win.textureManager.AddTexture("test", "Resources/test.png");

            win.AddScene(new MyScene());
            win.Run();
        }
    }
}