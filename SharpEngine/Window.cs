using System.Collections.Generic;
using System;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine;

/// <summary>
/// Fenêtre
/// </summary>
public class Window
{
    public readonly InternalGame InternalGame;
    private Vec2 _internalScreenSize;
    private bool _internalMouseVisible;
    private FullScreenType _internalFullScreen;
    private bool _internalVSync;
    private int _internalIndexCurrentScene = -1;
    internal readonly List<Scene> Scenes = new();
    
    public TextureManager TextureManager { get; }
    public FontManager FontManager { get; }
    public TilemapManager TileMapManager { get; }

    public Func<bool> StartCallback { get; set; }
    public Func<bool> StopCallback { get; set; }
    public Action<Window> RenderImGui { get; set; }

    public Color BackgroundColor { get; set; }
    public bool ExitWithEscape { get; set; }
    public bool Debug { get; set; }
    public bool ShowPhysicDebugView { get; set; }

    public Vec2 ScreenSize
    {
        get => _internalScreenSize;
        set
        {
            _internalScreenSize = value;
            if (InternalGame == null) return;
            InternalGame.Graphics.PreferredBackBufferWidth = (int)value.X;
            InternalGame.Graphics.PreferredBackBufferHeight = (int)value.Y;
            InternalGame.Graphics.ApplyChanges();
        }
    }

    public FullScreenType Fullscreen
    {
        get => _internalFullScreen;
        set 
        {
            _internalFullScreen = value;
            InternalGame?.SetFullscreen(value);
        }
    }

    public bool VSync
    {
        get => _internalVSync;
        set
        {
            _internalVSync = value;
            InternalGame?.SetVSync(value);
        }
    }

    public bool MouseVisible
    {
        get => _internalMouseVisible;
        set
        {
            _internalMouseVisible = value;
            if (InternalGame != null)
                InternalGame.IsMouseVisible = MouseVisible;
        }
    }

    public int IndexCurrentScene
    {
        get => _internalIndexCurrentScene;
        set
        {
            if(_internalIndexCurrentScene != -1)
                Scenes[_internalIndexCurrentScene].CloseScene?.Invoke(Scenes[_internalIndexCurrentScene]);
            _internalIndexCurrentScene = value;
            Scenes[_internalIndexCurrentScene].OpenScene?.Invoke(Scenes[_internalIndexCurrentScene]);
        }
    }

    public Scene CurrentScene
    {
        get => Scenes[_internalIndexCurrentScene];
        set
        {
            if(_internalIndexCurrentScene != -1)
                Scenes[_internalIndexCurrentScene].CloseScene?.Invoke(Scenes[_internalIndexCurrentScene]);
            _internalIndexCurrentScene = Scenes.IndexOf(value);
            Scenes[_internalIndexCurrentScene].OpenScene?.Invoke(Scenes[_internalIndexCurrentScene]);
        }
    }

    /// <summary>
    /// Initalise la Fenêtre.
    /// </summary>
    /// <param name="screenSize">Taille de la fenêtre (Vec2(800,600))</param>
    /// <param name="backgroundColor">Couleur de fond (Color.BLACK)</param>
    /// <param name="mouseVisible">Rend la souris visible</param>
    /// <param name="exitWithEscape">Quitte le jeu quand échap est appuyé</param>
    /// <param name="fullscreen">Lance le jeu avec ou sans fullscreen</param>
    /// <param name="vsync">Lance le jeu avec ou sans la vsync</param>
    /// <param name="debug">Lance le jeu en mode debug ou non</param>
    public Window(Vec2? screenSize = null, Color backgroundColor = null, bool mouseVisible = true,
        bool exitWithEscape = true, FullScreenType fullscreen = FullScreenType.NoFullscreen, bool vsync = false,
        bool debug = false, bool showPhysicDebugView = false)
    {
        ScreenSize = screenSize ?? new Vec2(800, 600);
        BackgroundColor = backgroundColor ?? Color.Black;
        MouseVisible = mouseVisible;
        ExitWithEscape = exitWithEscape;
        Fullscreen = fullscreen;
        VSync = vsync;
        Debug = debug;
        ShowPhysicDebugView = showPhysicDebugView;

        InternalGame = new InternalGame(this);
        TextureManager = new TextureManager(this);
        FontManager = new FontManager(this);
        TileMapManager = new TilemapManager(this);
    }

    public Scene GetScene(int index) => Scenes[index];
    public T GetCurrentScene<T>() where T : Scene => (T)Scenes[_internalIndexCurrentScene];
    public T GetScene<T>(int index) where T : Scene => (T)Scenes[index];

    public void TakeScreenshot(string fileName) => InternalGame.TakeScreenshot(fileName);

    public void AddScene(Scene scene)
    {
        scene.SetWindow(this);
        Scenes.Add(scene);
        _internalIndexCurrentScene = Scenes.Count - 1;
    }

    public void RemoveScene(Scene scene)
    {
        scene.SetWindow(null);
        Scenes.Remove(scene);
    }

    public void Run()
    {
        if(StartCallback == null || StartCallback())
            InternalGame.Run();
    }

    public void Stop()
    {
        if(StopCallback == null || StopCallback())
            InternalGame.Exit();
    }
}
