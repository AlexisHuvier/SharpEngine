using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SharpEngine
{
    internal class InternalGame : Game
    {
        internal readonly GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;
        private readonly Window window;

        public InternalGame(Window win) : base()
        {
            graphics = new GraphicsDeviceManager(this);
            window = win;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int) window.ScreenSize().x;
            graphics.PreferredBackBufferHeight = (int) window.ScreenSize().y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

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
