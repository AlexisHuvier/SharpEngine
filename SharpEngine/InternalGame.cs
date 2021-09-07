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

            foreach (Scene scene in window.scenes)
                scene.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (Scene scene in window.scenes)
                scene.LoadContent();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            foreach (Scene scene in window.scenes)
                scene.UnloadContent();

            base.UnloadContent();
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (window.currentScene != -1)
                window.scenes[window.currentScene].Update(GameTime.FromMonogameGameTime(gameTime));

            base.Update(gameTime);
        }

        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(window.backgroundColor.ToMonoGameColor());

            spriteBatch.Begin();
            if (window.currentScene != -1)
                window.scenes[window.currentScene].Draw(GameTime.FromMonogameGameTime(gameTime));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
