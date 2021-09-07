using SharpEngine;
using System;

namespace SharpEngineTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Window win = new Window(new Vec2(900, 600));
            Console.WriteLine(win.screenSize);
            win.Run();
        }
    }
}
