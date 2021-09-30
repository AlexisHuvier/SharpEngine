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

        protected void TextInputHandler(object sender, TextInputEventArgs args)
        {
            if (window.currentScene != -1)
                window.scenes[window.currentScene].TextInput(sender, (Inputs.Key)args.Key, args.Character);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)window.screenSize.x;
            graphics.PreferredBackBufferHeight = (int)window.screenSize.y;
            graphics.ApplyChanges();

            Window.TextInput += TextInputHandler;

            window.textureManager.Load();
            window.fontManager.Load();

            foreach (Scene scene in window.scenes)
                scene.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D blank = new Texture2D(GraphicsDevice, 1, 1);
            blank.SetData(new[] { Color.WHITE.ToMG() });
            window.textureManager.AddTexture("blank", blank);

            foreach (Scene scene in window.scenes)
                scene.LoadContent();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            foreach (Scene scene in window.scenes)
                scene.UnloadContent();

            window.textureManager.Unload();

            base.UnloadContent();
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (window.currentScene != -1)
                window.scenes[window.currentScene].Update(GameTime.FromMonogameGameTime(gameTime));

            InputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(window.backgroundColor.ToMG());

            spriteBatch.Begin();
            if (window.currentScene != -1)
                window.scenes[window.currentScene].Draw(GameTime.FromMonogameGameTime(gameTime));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
