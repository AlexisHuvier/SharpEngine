using System;
using Raylib_cs;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine;

/// <summary>
/// Class which represents and create Window
/// </summary>
public class Window
{
    /// <summary>
    /// Title of Window
    /// </summary>
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Raylib.SetWindowTitle(value);
        }
    }

    /// <summary>
    /// Size of Window
    /// </summary>
    public Vec2I ScreenSize
    {
        get => _screenSize;
        set
        {
            _screenSize = value;
            Raylib.SetWindowSize(value.X, value.Y);
        }
    }

    /// <summary>
    /// Position of Window
    /// </summary>
    public Vec2I Position
    {
        get => Raylib.GetWindowPosition();
        set => Raylib.SetWindowPosition(value.X, value.Y);
    }

    /// <summary>
    /// Background Color used in Window
    /// </summary>
    public Color BackgroundColor;
    
    /// <summary>
    /// Function which be called in Start of Window (can stop start by return false)
    /// </summary>
    public Func<bool> StartCallback;
    
    /// <summary>
    /// Function which be called in Stop of Window (can stop stop by return false)
    /// </summary>
    public Func<bool> StopCallback;
    
    /// <summary>
    /// Function which be called to render something in ImGui
    /// </summary>
    public Action<Window> RenderImGui;
    
    /// <summary>
    /// Manage Debug Mode of Window
    /// </summary>
    public bool Debug;

    
    /// <summary>
    /// Texture Manager of Window
    /// </summary>
    public TextureManager TextureManager { get; }
    
    /// <summary>
    /// Font Manager of Window
    /// </summary>
    public FontManager FontManager { get; }

    private Vec2I _screenSize;
    private string _title;
    private readonly SeImGui _seImGui;
    private bool _closeWindow;

    /// <summary>
    /// Create and Init Window
    /// </summary>
    /// <param name="width">Width of Window</param>
    /// <param name="height">Height of Window</param>
    /// <param name="title">Title of Window</param>
    /// <param name="backgroundColor">Background Color of Window (Black)</param>
    /// <param name="debug">Debug Mode (false)</param>
    public Window(int width, int height, string title, Color? backgroundColor = null, bool debug = false) : 
        this(new Vec2I(width, height), title, backgroundColor, debug) {}

    /// <summary>
    /// Create and Init Window
    /// </summary>
    /// <param name="screenSize">Size of Window</param>
    /// <param name="title">Title of Window</param>
    /// <param name="backgroundColor">Background Color of Window (Black)</param>
    /// <param name="debug">Debug Mode (false)</param>
    public Window(Vec2I screenSize, string title, Color? backgroundColor = null, bool debug = false)
    {
        _title = title;
        _screenSize = screenSize;
        BackgroundColor = backgroundColor ?? Color.Black;
        Debug = debug;
        
        Raylib.InitWindow(screenSize.X, screenSize.Y, title);
        Raylib.InitAudioDevice();
        
        _seImGui = new SeImGui();
        _seImGui.Load(screenSize.X, screenSize.Y);

        TextureManager = new TextureManager();
        FontManager = new FontManager();
        
        TextureManager.AddTexture("knight", "Resources/KnightM.png");
    }

    /// <summary>
    /// Take a screenshot and save it
    /// </summary>
    /// <param name="path">Path of saved screenshot</param>
    public void TakeScreenshot(string path) => Raylib.TakeScreenshot(path);

    /// <summary>
    /// Set master volume
    /// </summary>
    /// <param name="volume">Volume (0 to 1)</param>
    public void SetMasterVolume(float volume) => Raylib.SetMasterVolume(volume);

    /// <summary>
    /// Run Window
    /// </summary>
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
        
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    /// <summary>
    /// Stop Window
    /// </summary>
    public void Stop()
    {
        if (StopCallback == null || StopCallback())
            _closeWindow = true;
    }
}