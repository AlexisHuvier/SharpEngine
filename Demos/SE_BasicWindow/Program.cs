using SharpEngine;

namespace SE_BasicWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            Window win = new Window(new Vec2(900, 600), Color.CORNFLOWER_BLUE, fullscreen: FullScreenType.BORDERLESS_FULLSCREEN);

            win.AddScene(new MyScene());
            win.Run();
        }
    }
}