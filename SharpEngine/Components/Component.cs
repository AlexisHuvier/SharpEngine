using Microsoft.Xna.Framework.Graphics;

namespace SharpEngine.Components
{
    /// <summary>
    /// Composant basique
    /// </summary>
    public class Component
    {
        internal Entity entity;

        /// <summary>
        /// Initialise le Composant.<para/>
        /// N'utilise aucun paramètre.
        /// </summary>
        /// <param name="parameters">Paramètres du Composant</param>
        public Component(params object[] parameters)
        {
            entity = null;
        }

        public virtual void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        public Entity GetEntity()
        {
            return entity;
        }

        public SpriteBatch GetSpriteBatch()
        {
            if (entity != null && entity.scene != null && entity.scene.window != null)
                return entity.scene.window.internalGame.spriteBatch;
            return null;
        }

        public Window GetWindow()
        {
            if (entity != null && entity.scene != null)
                return entity.scene.window;
            return null;
        }

        public virtual void Initialize() {}
        public virtual void LoadContent() {}
        public virtual void UnloadContent() {}
        public virtual void TextInput(object sender, Inputs.Key key, char Character) {}
        public virtual void Update(GameTime gameTime) {}
        public virtual void Draw(GameTime gameTime) {}
    }
}
