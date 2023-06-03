using SharpEngine;
using SharpEngine.Utils;

namespace SE_BasicWindow;

internal static class Program
{
    private static void Main()
    {
        var window = new Window(800, 600, "SE Raylib", Color.CornflowerBlue);
        window.Run();
    }
}