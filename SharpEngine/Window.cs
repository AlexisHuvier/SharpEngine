using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine;

public class Window
{
    public Color BackgroundColor;

    public Window(int width, int height, string title, Color? backgroundColor = null) : 
        this(new Vec2I(width, height), title, backgroundColor) {}
    
    public Window(Vec2I screenSize, string title, Color? backgroundColor = null)
    {
        BackgroundColor = backgroundColor ?? Color.Black;
        Raylib.InitWindow(screenSize.X, screenSize.Y, title);
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColor);
            
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }

    public void Stop()
    {
        Raylib.CloseWindow();
    }
}