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
            window = win;

            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = window.mouseVisible;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)window.screenSize.x;
            graphics.PreferredBackBufferHeight = (int)window.screenSize.y;
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
            GraphicsDevice.Clear(window.backgroundColor.ToMonoGameColor());

            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
