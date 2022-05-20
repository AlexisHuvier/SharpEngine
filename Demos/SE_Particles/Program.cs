using SharpEngine;

namespace SE_Particles
{
    class Program
    {
        static void Main(string[] args)
        {
            Window win = new Window(new Vec2(900, 600), Color.CORNFLOWER_BLUE);

            win.AddScene(new MyScene());
            win.Run();
        }
    }
}