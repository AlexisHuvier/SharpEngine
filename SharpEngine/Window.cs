using System;
using System.Collections.Generic;
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
            CameraManager.SetScreenSize(value);
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
    public Func<bool>? StartCallback;
    
    /// <summary>
    /// Function which be called in Stop of Window (can stop stop by return false)
    /// </summary>
    public Func<bool>? StopCallback;
    
    /// <summary>
    /// Function which be called to render something in ImGui
    /// </summary>
    public Action<Window>? RenderImGui;
    
    /// <summary>
    /// Manage Debug Mode of Window
    /// </summary>
    public bool Debug;
    
    
    /// <summary>
    /// Camera Manager of Window
    /// </summary>
    public CameraManager CameraManager { get; }

    
    /// <summary>
    /// Texture Manager of Window
    /// </summary>
    public TextureManager TextureManager { get; }
    
    /// <summary>
    /// Font Manager of Window
    /// </summary>
    public FontManager FontManager { get; }

    /// <summary>
    /// Index of Current Scene
    /// </summary>
    public int IndexCurrentScene
    {
        get => _internalIndexCurrentScene;
        set
        {
            if(_internalIndexCurrentScene != -1)
                _scenes[_internalIndexCurrentScene].CloseScene?.Invoke(_scenes[_internalIndexCurrentScene]);
            _internalIndexCurrentScene = value;
            _scenes[_internalIndexCurrentScene].OpenScene?.Invoke(_scenes[_internalIndexCurrentScene]);
        }
    }

    /// <summary>
    /// Current Scene
    /// </summary>
    public Scene CurrentScene
    {
        get => _scenes[_internalIndexCurrentScene];
        set
        {
            if(_internalIndexCurrentScene != 1)
                _scenes[_internalIndexCurrentScene].CloseScene?.Invoke(_scenes[_internalIndexCurrentScene]);
            _internalIndexCurrentScene = _scenes.IndexOf(value);
            _scenes[_internalIndexCurrentScene].OpenScene?.Invoke(_scenes[_internalIndexCurrentScene]);
        }
    }

    private Vec2I _screenSize;
    private string _title;
    private readonly SeImGui _seImGui;
    private bool _closeWindow;
    private readonly List<Scene> _scenes = new();
    private int _internalIndexCurrentScene = -1;

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
        CameraManager = new CameraManager();
        CameraManager.SetScreenSize(screenSize);
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
    /// Add Scene to Window and Set Current Scene to it
    /// </summary>
    /// <param name="scene">Scene which be added</param>
    public void AddScene(Scene scene)
    {
        scene.Window = this;
        _scenes.Add(scene);
        _internalIndexCurrentScene = _scenes.Count - 1;
    }

    /// <summary>
    /// Get Scene by Index
    /// </summary>
    /// <param name="index">Index of Scene</param>
    /// <returns>Scene</returns>
    public Scene GetScene(int index) => _scenes[index];

    /// <summary>
    /// Get Current Scene cast as T
    /// </summary>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Current Scene cast as T</returns>
    public T GetCurrentScene<T>() where T : Scene => (T)_scenes[_internalIndexCurrentScene];

    /// <summary>
    /// Get Scene cast as T
    /// </summary>
    /// <param name="index">Index of Scene</param>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Scene cast as T</returns>
    public T GetScene<T>(int index) where T : Scene => (T)_scenes[index];

    /// <summary>
    /// Run Window
    /// </summary>
    public void Run()
    {
        if(StartCallback != null && !StartCallback())
            return;
        
        // LOAD 
        DebugManager.Log(LogLevel.LogInfo, "Loading Scenes...");
        foreach (var scene in _scenes)
            scene.Load();
        DebugManager.Log(LogLevel.LogInfo, "Scenes loaded !");

        while (!Raylib.WindowShouldClose() && !_closeWindow)
        {
            // UPDATE
            _seImGui.Update(Raylib.GetFrameTime());
            
            foreach (var scene in _scenes)
                scene.Update(Raylib.GetFrameTime());
            
            CameraManager.Update(Raylib.GetFrameTime());
            
            // DRAW IMGUI
            if(Debug)
                RenderImGui?.Invoke(this);
            
            // DRAW
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColor);
            
            Raylib.BeginMode2D(CameraManager.Camera2D);
            foreach (var scene in _scenes)
                scene.DrawEntities();
            Raylib.EndMode2D();

            foreach (var scene in _scenes)
                scene.DrawWidgets();
            
            if(Debug)
                _seImGui.Draw();
            
            Raylib.EndDrawing();
        }
        
        // UNLOAD
        DebugManager.Log(LogLevel.LogInfo, "Unloading Scenes...");
        foreach (var scene in _scenes)
            scene.Unload();
        DebugManager.Log(LogLevel.LogInfo, "Scenes unloaded !");
        
        DebugManager.Log(LogLevel.LogInfo, "Unloading Textures...");
        TextureManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "Textures unloaded !");
        DebugManager.Log(LogLevel.LogInfo, "Unloading Fonts...");
        FontManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "Fonts unloaded !");
        
        DebugManager.Log(LogLevel.LogInfo, "Closing Window.");
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