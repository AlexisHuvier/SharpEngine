using System;
using System.Collections.Generic;
using Raylib_cs;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using SharpEngine.Utils.EventArgs;
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
    /// Event which be called in Start of Window (can stop start by set result to false)
    /// </summary>
    public event EventHandler<BoolEventArgs>? StartCallback;
    
    /// <summary>
    /// Event which be called in Stop of Window (can stop stop by set result to false)
    /// </summary>
    public event EventHandler<BoolEventArgs>? StopCallback;
    
    /// <summary>
    /// Function which be called to render something in ImGui
    /// </summary>
    public Action<Window>? RenderImGui;

    /// <summary>
    /// Manage Debug Mode of Window
    /// </summary>
    public bool Debug
    {
        get => _debug;
        set
        {
            _debug = value;
            DebugManager.SetLogLevel(value ? LogLevel.LogAll : LogLevel.LogInfo);
        }
    }
    
    
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
    /// Sound Manager of Window
    /// </summary>
    public SoundManager SoundManager { get; }

    /// <summary>
    /// Music Manager of Window
    /// </summary>
    public MusicManager MusicManager { get; }

    /// <summary>
    /// Index of Current Scene
    /// </summary>
    public int IndexCurrentScene
    {
        get => _internalIndexCurrentScene;
        set
        {
            if(_internalIndexCurrentScene != -1)
                Scenes[_internalIndexCurrentScene].CloseScene();
            _internalIndexCurrentScene = value;
            Scenes[_internalIndexCurrentScene].OpenScene();
        }
    }

    /// <summary>
    /// Current Scene
    /// </summary>
    public Scene CurrentScene
    {
        get => Scenes[_internalIndexCurrentScene];
        set
        {
            if(_internalIndexCurrentScene != 1)
                Scenes[_internalIndexCurrentScene].CloseScene();
            _internalIndexCurrentScene = Scenes.IndexOf(value);
            Scenes[_internalIndexCurrentScene].OpenScene();
        }
    }

    /// <summary>
    /// Get All Scenes
    /// </summary>
    public List<Scene> Scenes { get; } = new();
    
    private Vec2I _screenSize;
    private string _title;
    private readonly SeImGui _seImGui;
    private bool _closeWindow;
    private int _internalIndexCurrentScene = -1;
    private bool _debug;

    private float _updateStepTimer;
    private const float UpdateStep = 1 / 60f;

    /// <summary>
    /// Create and Init Window
    /// </summary>
    /// <param name="width">Width of Window</param>
    /// <param name="height">Height of Window</param>
    /// <param name="title">Title of Window</param>
    /// <param name="backgroundColor">Background Color of Window (Black)</param>
    /// <param name="fps">Number of FPS (60)</param>
    /// <param name="debug">Debug Mode (false)</param>
    public Window(int width, int height, string title, Color? backgroundColor = null, int? fps = 60, bool debug = false) : 
        this(new Vec2I(width, height), title, backgroundColor, fps, debug) {}

    /// <summary>
    /// Create and Init Window
    /// </summary>
    /// <param name="screenSize">Size of Window</param>
    /// <param name="title">Title of Window</param>
    /// <param name="backgroundColor">Background Color of Window (Black)</param>
    /// <param name="fps">Number of FPS (60)</param>
    /// <param name="debug">Debug Mode (false)</param>
    public Window(Vec2I screenSize, string title, Color? backgroundColor = null, int? fps = 60, bool debug = false)
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
        SoundManager = new SoundManager();
        MusicManager = new MusicManager();
        CameraManager.SetScreenSize(screenSize);
        
        if(fps != null)
            Raylib.SetTargetFPS(fps.Value);
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
        Scenes.Add(scene);
        _internalIndexCurrentScene = Scenes.Count - 1;
    }

    /// <summary>
    /// Get Scene by Index
    /// </summary>
    /// <param name="index">Index of Scene</param>
    /// <returns>Scene</returns>
    public Scene GetScene(int index) => Scenes[index];

    /// <summary>
    /// Get Current Scene cast as T
    /// </summary>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Current Scene cast as T</returns>
    public T GetCurrentScene<T>() where T : Scene => (T)Scenes[_internalIndexCurrentScene];

    /// <summary>
    /// Get Scene cast as T
    /// </summary>
    /// <param name="index">Index of Scene</param>
    /// <typeparam name="T">Type as Scene</typeparam>
    /// <returns>Scene cast as T</returns>
    public T GetScene<T>(int index) where T : Scene => (T)Scenes[index];

    /// <summary>
    /// Run Window
    /// </summary>
    public void Run()
    {
        var args = new BoolEventArgs();
        StartCallback?.Invoke(this, args);
        if(!args.Result)
            return;

        #region Load

        DebugManager.Log(LogLevel.LogInfo, "SE: Loading Scenes...");
        foreach (var scene in Scenes)
            scene.Load();
        DebugManager.Log(LogLevel.LogInfo, "SE: Scenes loaded !");

        #endregion

        while (!Raylib.WindowShouldClose() && !_closeWindow)
        {
            #region Update Pressed Keys and Chars
            
            InputManager.InternalPressedChars.Clear();
            InputManager.InternalPressedKeys.Clear();

            var key = Raylib.GetKeyPressed();
            while (key > 0)
            {
                InputManager.InternalPressedKeys.Add(key);
                key = Raylib.GetKeyPressed();
            }
            
            var charGot = Raylib.GetCharPressed();
            while (charGot > 0)
            {
                InputManager.InternalPressedChars.Add(charGot);
                charGot = Raylib.GetCharPressed();
            }

            #endregion

            #region Update

            var delta = Raylib.GetFrameTime();
            
            _updateStepTimer += delta;

            if (_updateStepTimer >= UpdateStep)
            {
                _seImGui.Update(UpdateStep);
                
                CurrentScene.Update(UpdateStep);
                CameraManager.Update(UpdateStep);
                _updateStepTimer -= UpdateStep;

                if(Debug)
                    RenderImGui?.Invoke(this);
            }

            #endregion

            #region Draw
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColor);
            
            Raylib.BeginMode2D(CameraManager.Camera2D);
            CurrentScene.DrawEntities();
            Raylib.EndMode2D();

            CurrentScene.DrawWidgets();

            if (Debug)
                _seImGui.Draw();

            Raylib.EndDrawing();

            #endregion
        }

        #region Unload
        
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Scenes...");
        foreach (var scene in Scenes)
            scene.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Scenes unloaded !");
        
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Textures...");
        TextureManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Textures unloaded !");
        DebugManager.Log(LogLevel.LogInfo, "SE: Unloading Fonts...");
        FontManager.Unload();
        DebugManager.Log(LogLevel.LogInfo, "SE: Fonts unloaded !");
        
        DebugManager.Log(LogLevel.LogInfo, "SE: Closing Window.");
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();

        #endregion
    }

    /// <summary>
    /// Stop Window
    /// </summary>
    public void Stop()
    {
        var args = new BoolEventArgs();
        StopCallback?.Invoke(this, args);
        if(args.Result)
            _closeWindow = true;
    }
}