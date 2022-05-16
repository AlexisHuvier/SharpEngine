using Microsoft.Xna.Framework.Graphics;

namespace SharpEngine.Widgets
{
    /// <summary>
    /// Widget basique
    /// </summary>
    public class Widget
    {
        internal Scene scene;
        public Vec2 position;
        public bool displayed;
        public bool active;

        /// <summary>
        /// Initialise le Widget.
        /// </summary>
        /// <param name="position">Position (Vec2(0))</param>
        public Widget(Vec2 position = null)
        {
            this.position = position ?? new Vec2(0);
            displayed = true;
            active = true;
        }
        public virtual void SetScene(Scene scene)
        {
            this.scene = scene;
        }

        public Scene GetScene()
        {
            return scene;
        }

        public SpriteBatch GetSpriteBatch()
        {
            if (scene != null && scene.window != null)
                return scene.window.internalGame.spriteBatch;
            return null;
        }

        public Window GetWindow()
        {
            if (scene != null)
                return scene.window;
            return null;
        }

        public virtual void Initialize()
        {}

        public virtual void LoadContent()
        {}

        public virtual void UnloadContent()
        {}

        public virtual void TextInput(object sender, Inputs.Key key, char Character)
        {}

        public virtual void Update(GameTime gameTime)
        {}

        public virtual void Draw(GameTime gameTime)
        {}
    }
}
