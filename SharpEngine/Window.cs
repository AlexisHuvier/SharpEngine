using System;
using ImGuiNET;
using Raylib_cs;
using SharpEngine.Math;
using SharpEngine.Utils;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine;

public class Window
{
    public Color BackgroundColor;
    private SeImGui _seImGui;

    public Window(int width, int height, string title, Color? backgroundColor = null) : 
        this(new Vec2I(width, height), title, backgroundColor) {}
    
    public Window(Vec2I screenSize, string title, Color? backgroundColor = null)
    {
        BackgroundColor = backgroundColor ?? Color.Black;
        Raylib.InitWindow(screenSize.X, screenSize.Y, title);
        
        _seImGui = new SeImGui();
        _seImGui.Load(screenSize.X, screenSize.Y);
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            _seImGui.Update(Raylib.GetFrameTime());
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColor);
            
                _seImGui.Draw();
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }

    public void Stop()
    {
        Raylib.CloseWindow();
    }
}