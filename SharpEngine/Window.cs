using System;
using Raylib_cs;
using SharpEngine.Manager;
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

    public TextureManager TextureManager { get; }
    public FontManager FontManager { get; }

    private Vec2I _screenSize;
    private string _title;
    private readonly SeImGui _seImGui;
    private bool _closeWindow;

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

        TextureManager = new TextureManager();
        FontManager = new FontManager();
        
        TextureManager.AddTexture("knight", "Resources/KnightM.png");
    }

    public void TakeScreenshot(string path) => Raylib.TakeScreenshot(path);

    public void Run()
    {
        if(StartCallback != null && !StartCallback())
            return;
        
        // LOAD 
        var texture = TextureManager.GetTexture("knight");
        
        while (!Raylib.WindowShouldClose() && !_closeWindow)
        {
            // UPDATE
            _seImGui.Update(Raylib.GetFrameTime());
            
            
            // DRAW IMGUI
            if(Debug)
                RenderImGui?.Invoke(this);
            
            // DRAW
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColor);

            Raylib.DrawTexture(texture, _screenSize.X / 2 - texture.width / 2, _screenSize.Y / 2 - texture.height / 2, Color.White);
            Raylib.DrawTextEx(FontManager.GetFont("RAYLIB_DEFAULT"), "SUPER TEST", new Vec2(100), 25, 2, Color.Black);
            
            if(Debug)
                _seImGui.Draw();
            
            Raylib.EndDrawing();
        }
        
        // UNLOAD
        TextureManager.Unload();
        FontManager.Unload();
        
        Raylib.CloseWindow();
    }

    public void Stop()
    {
        if (StopCallback == null || StopCallback())
            _closeWindow = true;
    }
}