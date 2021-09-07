

namespace SharpEngine
{
    public class Window
    {
        private InternalGame internalGame;
        private Vec2 size;

        public Window(Vec2 screenSize)
        {
            size = screenSize;

            internalGame = new InternalGame(this);
        }

        public Vec2 ScreenSize()
        {
            return size;
        }

        public void SetScreenSize()
        {
            internalGame.graphics.PreferredBackBufferWidth = (int)size.x;
            internalGame.graphics.PreferredBackBufferHeight = (int)size.y;
            internalGame.graphics.ApplyChanges();
        }

        public void Run()
        {
            internalGame.Run();
        }
    }
}
