using System.Collections.Generic;

namespace SharpEngine
{
    public class Window
    {
        public InternalGame internalGame;
        private Vec2 _internalScreenSize;
        private bool _internalMouseVisible;
        internal int currentScene = -1;
        internal List<Scene> scenes = new List<Scene>();
        public TextureManager textureManager;
        public FontManager fontManager;

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

        public Window(Vec2 screenSize, Color backgroundColor, bool mouseVisible, bool exitWithEscape)
        {
            this.screenSize = screenSize;
            this.backgroundColor = backgroundColor;
            this.mouseVisible = mouseVisible;
            this.exitWithEscape = exitWithEscape;

            internalGame = new InternalGame(this);
            textureManager = new TextureManager(this);
            fontManager = new FontManager(this);
        }

        public Window(Vec2 screenSize, Color backgroundColor) : this(screenSize, backgroundColor, true, true) {}
        public Window(Vec2 screenSize): this(screenSize, Color.BLACK, true, true) {}
        public Window(): this(new Vec2(800, 600), Color.BLACK, true, true) {}

        public void SetCurrentScene(Scene scene)
        {
            currentScene = scenes.IndexOf(scene);
        }

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
            internalGame.Run();
        }
    }
}
