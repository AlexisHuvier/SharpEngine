using System;
using ImGuiNET;
using Raylib_cs;
using SharpEngine.Math;
using SharpEngine.Utils;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine;

public class Window
{
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Raylib.SetWindowTitle(value);
        }
    }

    public Vec2I ScreenSize
    {
        get => _screenSize;
        set
        {
            _screenSize = value;
            Raylib.SetWindowSize(value.X, value.Y);
        }
    }

    public Vec2I Position
    {
        get => Raylib.GetWindowPosition();
        set => Raylib.SetWindowPosition(value.X, value.Y);
    }

    public Color BackgroundColor;
    public Func<bool> StartCallback;
    public Func<bool> StopCallback;
    public Action<Window> RenderImGui;
    public bool Debug;

    private Vec2I _screenSize;
    private string _title;
    private SeImGui _seImGui;
    private bool _closeWindow = false;

    public Window(int width, int height, string title, Color? backgroundColor = null, bool debug = false) : 
        this(new Vec2I(width, height), title, backgroundColor, debug) {}
    
    public Window(Vec2I screenSize, string title, Color? backgroundColor = null, bool debug = false)
    {
        _title = title;
        _screenSize = screenSize;
        BackgroundColor = backgroundColor ?? Color.Black;
        Debug = debug;
        
        Raylib.InitWindow(screenSize.X, screenSize.Y, title);
        
        _seImGui = new SeImGui();
        _seImGui.Load(screenSize.X, screenSize.Y);
    }

    public void TakeScreenshot(string path) => Raylib.TakeScreenshot(path);

    public void Run()
    {
        if(StartCallback != null && !StartCallback())
            return;
        
        while (!Raylib.WindowShouldClose() && !_closeWindow)
        {
            // UPDATE
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
            {
                BackgroundColor = Color.Black;
                Title = "Ceci est un test";
                Position = new Vec2I(150, 200);
                ScreenSize = new Vec2I(600, 800);
            }

            _seImGui.Update(Raylib.GetFrameTime());
            
            
            // DRAW IMGUI
            if(Debug)
                RenderImGui?.Invoke(this);
            
            // DRAW
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColor);
            
            if(Debug)
                _seImGui.Draw();
            
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }

    public void Stop()
    {
        if (StopCallback == null || StopCallback())
            _closeWindow = true;
    }
}