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
        /// Initialise le Widget.<para/>
        /// -> Paramètre 1 : Position (<seealso cref="Vec2"/>) (Vec2(0))
        /// </summary>
        /// <param name="parameters">Paramètres du Widget</param>
        public Widget(params object[] parameters)
        {
            position = new Vec2(0);
            displayed = true;
            active = true;

            if (parameters.Length >= 1 && parameters[0] is Vec2 pos)
                position = pos;
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
