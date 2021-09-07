

namespace SharpEngine
{
    public class Window
    {
        private InternalGame internalGame;
        private Vec2 _internalScreenSize;
        private bool _internalMouseVisible;

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
        }

        public void Run()
        {
            internalGame.Run();
        }
    }
}
