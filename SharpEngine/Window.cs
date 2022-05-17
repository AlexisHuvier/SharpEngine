using System.Collections.Generic;
using System;

namespace SharpEngine
{
    /// <summary>
    /// Fenêtre
    /// </summary>
    public class Window
    {
        public InternalGame internalGame;
        private Vec2 _internalScreenSize;
        private bool _internalMouseVisible;
        private FullScreenType _internalFullScreen;
        internal int currentScene = -1;
        internal List<Scene> scenes = new List<Scene>();
        public TextureManager textureManager;
        public FontManager fontManager;

        public Func<bool> startCallback = null;
        public Func<bool> stopCallback = null;

        public Color backgroundColor;
        public bool exitWithEscape;

        public Vec2 screenSize
        {
            get => _internalScreenSize;
            set
            {
                _internalScreenSize = value;
                if (internalGame != null)
                {
                    internalGame.graphics.PreferredBackBufferWidth = (int)value.x;
                    internalGame.graphics.PreferredBackBufferHeight = (int)value.y;
                    internalGame.graphics.ApplyChanges();
                }
            }
        }

        public FullScreenType fullscreen
        {
            get => _internalFullScreen;
            set 
            {
                _internalFullScreen = value;
                if(internalGame != null)
                    internalGame.SetFullscreen(value);
            }
        }

        public bool mouseVisible
        {
            get => _internalMouseVisible;
            set
            {
                _internalMouseVisible = value;
                if (internalGame != null)
                    internalGame.IsMouseVisible = mouseVisible;
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
        public Window(Vec2 screenSize = null, Color backgroundColor = null, bool mouseVisible = true, bool exitWithEscape = true, FullScreenType fullscreen = FullScreenType.NO_FULLSCREEN)
        {
            this.screenSize = screenSize ?? new Vec2(800, 600);
            this.backgroundColor = backgroundColor ?? Color.BLACK;
            this.mouseVisible = mouseVisible;
            this.exitWithEscape = exitWithEscape;
            this.fullscreen = fullscreen;

            internalGame = new InternalGame(this);
            textureManager = new TextureManager(this);
            fontManager = new FontManager(this);
        }

        public Scene GetScene(int index) => scenes[index];
        public void SetCurrentScene(Scene scene) => currentScene = scenes.IndexOf(scene);
        public Scene GetCurrentScene() => scenes[currentScene];

        public void TakeScreenshot(string fileName) => internalGame.TakeScreenshot(fileName);

        public void AddScene(Scene scene)
        {
            scene.SetWindow(this);
            scenes.Add(scene);
            SetCurrentScene(scene);
        }

        public void RemoveScene(Scene scene)
        {
            scene.SetWindow(null);
            scenes.Remove(scene);
        }

        public void Run()
        {
            if(startCallback == null || startCallback())
                internalGame.Run();
        }

        public void Stop()
        {
            if(stopCallback == null || stopCallback())
                internalGame.Exit();
        }
    }
}
