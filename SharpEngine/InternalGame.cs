using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.ImGui;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine;

/// <summary>
/// Classe interne du jeu
/// </summary>
public class InternalGame : Game
{
    public GraphicsDeviceManager Graphics { get; }
    public SpriteBatch SpriteBatch { get; set; }
    
    private readonly Window _window;
    private ImGuiRenderer _imGuiRenderer;

    public InternalGame(Window win)
    {
        _window = win;

        Graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = _window.MouseVisible;
    }

    internal void SetVSync(bool vsync)
    {
        
        Graphics.SynchronizeWithVerticalRetrace = vsync;
        IsFixedTimeStep = vsync;
        Graphics.ApplyChanges();
    }

    internal void SetFullscreen(FullScreenType fullScreenType)
    {
        switch(fullScreenType) {
            case FullScreenType.BorderlessFullscreen:
                if (Graphics.IsFullScreen)
                    Graphics.ToggleFullScreen();
                var displayWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                var displayHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                Graphics.PreferredBackBufferWidth = displayWidth;
                Graphics.PreferredBackBufferHeight = displayHeight;

                Window.IsBorderless = true;
                Graphics.ApplyChanges();
                break;
            case FullScreenType.HardwareFullscreen:
                SetFullscreen(FullScreenType.NoFullscreen);
                Graphics.ToggleFullScreen();
                Graphics.ApplyChanges();
                break;
            case FullScreenType.NoFullscreen:
                if (Graphics.IsFullScreen)
                    Graphics.ToggleFullScreen();
                Graphics.PreferredBackBufferWidth = (int)_window.ScreenSize.X;
                Graphics.PreferredBackBufferHeight = (int)_window.ScreenSize.Y;

                Window.IsBorderless = false;
                Graphics.ApplyChanges();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(fullScreenType), fullScreenType, null);
        }
    }

    internal void TakeScreenshot(string fileName)
    {
        var w = GraphicsDevice.PresentationParameters.BackBufferWidth;
        var h = GraphicsDevice.PresentationParameters.BackBufferHeight;

        Draw(new Microsoft.Xna.Framework.GameTime());

        var backBuffer = new int[w * h];
        GraphicsDevice.GetBackBufferData(backBuffer);

        var texture = new Texture2D(GraphicsDevice, w, h, false, GraphicsDevice.PresentationParameters.BackBufferFormat);
        texture.SetData(backBuffer);

        Stream stream = File.OpenWrite(fileName + ".jpg");

        texture.SaveAsJpeg(stream, w, h);
        stream.Dispose();
        texture.Dispose();
    }

    private void TextInputHandler(object sender, TextInputEventArgs args)
    {
        if (_window.IndexCurrentScene != -1)
            _window.Scenes[_window.IndexCurrentScene].TextInput(sender, (Key)args.Key, args.Character);
    }

    protected override void Initialize()
    {
        Graphics.PreferredBackBufferWidth = (int)_window.ScreenSize.X;
        Graphics.PreferredBackBufferHeight = (int)_window.ScreenSize.Y;
        Graphics.ApplyChanges();

        SetFullscreen(_window.Fullscreen);
        SetVSync(_window.VSync);

        Window.TextInput += TextInputHandler;

        _window.TextureManager.Load();
        _window.FontManager.Load();

        foreach (var scene in _window.Scenes)
            scene.Initialize();

        _imGuiRenderer = new ImGuiRenderer(this);
        _imGuiRenderer.RebuildFontAlias();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        var blank = new Texture2D(GraphicsDevice, 1, 1);
        blank.SetData(new[] { Color.White.ToMg() });
        _window.TextureManager.AddTexture("blank", blank);

        foreach (var scene in _window.Scenes)
            scene.LoadContent();

        base.LoadContent();
    }

    protected override void UnloadContent()
    {
        foreach (var scene in _window.Scenes)
            scene.UnloadContent();

        _window.TextureManager.Unload();
        MusicManager.Unload();
        SoundManager.Unload();

        base.UnloadContent();
    }

    protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
    {
        DebugManager.Update(GameTime.FromMonogameGameTime(gameTime));
        CameraManager.Update(_window.ScreenSize);

        if (Keyboard.GetState().IsKeyDown(Keys.Escape) && _window.ExitWithEscape)
            Exit();

        if (_window.IndexCurrentScene != -1)
            _window.Scenes[_window.IndexCurrentScene].Update(GameTime.FromMonogameGameTime(gameTime));

        InputManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
    {
        var gT = GameTime.FromMonogameGameTime(gameTime);
        
        DebugManager.Draw();

        GraphicsDevice.Clear(_window.BackgroundColor.ToMg());

        SpriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
        if (_window.IndexCurrentScene != -1)
            _window.Scenes[_window.IndexCurrentScene].Draw(gT);
        SpriteBatch.End();

        if (_window.Debug)
        {
            _imGuiRenderer.BeforeLayout(gT);
            _window.RenderImGui?.Invoke(_window);
            _imGuiRenderer.AfterLayout();
        }

        base.Draw(gameTime);
    }
}
