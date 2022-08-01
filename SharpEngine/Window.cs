using System.Collections.Generic;
using System;
using SharpEngine.Managers;
using SharpEngine.Utils;

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
    internal int CurrentScene = -1;
    internal readonly List<Scene> Scenes = new();
    public readonly TextureManager TextureManager;
    public readonly FontManager FontManager;

    public Func<bool> StartCallback = null;
    public Func<bool> StopCallback = null;

    public Color BackgroundColor;
    public bool ExitWithEscape;

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

    /// <summary>
    /// Initalise la Fenêtre.
    /// </summary>
    /// <param name="screenSize">Taille de la fenêtre (Vec2(800,600))</param>
    /// <param name="backgroundColor">Couleur de fond (Color.BLACK)</param>
    /// <param name="mouseVisible">Rend la souris visible</param>
    /// <param name="exitWithEscape">Quitte le jeu quand échap est appuyé</param>
    /// <param name="fullscreen">Lance le jeu avec ou sans fullscreen</param>
    /// <param name="vsync">Lance le jeu avec ou sans la vsync</param>
    public Window(Vec2 screenSize = null, Color backgroundColor = null, bool mouseVisible = true, bool exitWithEscape = true, FullScreenType fullscreen = FullScreenType.NoFullscreen, bool vsync = false)
    {
        ScreenSize = screenSize ?? new Vec2(800, 600);
        BackgroundColor = backgroundColor ?? Color.Black;
        MouseVisible = mouseVisible;
        ExitWithEscape = exitWithEscape;
        Fullscreen = fullscreen;
        VSync = vsync;

        InternalGame = new InternalGame(this);
        TextureManager = new TextureManager(this);
        FontManager = new FontManager(this);
    }

    public Scene GetScene(int index) => Scenes[index];
    public void SetCurrentScene(Scene scene) => CurrentScene = Scenes.IndexOf(scene);
    public void SetCurrentScene(int sceneId) => CurrentScene = sceneId;
    public Scene GetCurrentScene() => Scenes[CurrentScene];

    public void TakeScreenshot(string fileName) => InternalGame.TakeScreenshot(fileName);

    public void AddScene(Scene scene)
    {
        scene.SetWindow(this);
        Scenes.Add(scene);
        CurrentScene = Scenes.Count - 1;
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
