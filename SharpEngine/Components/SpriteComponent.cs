using Microsoft.Xna.Framework;

namespace SharpEngine.Components
{
    /// <summary>
    /// Composant ajoutant l'affichage d'un sprite
    /// </summary>
    public class SpriteComponent: Component
    {
        public string sprite;
        public bool displayed;
        public Vec2 offset;

        /// <summary>
        /// Initialise le Composant.
        /// </summary>
        /// <param name="sprite">Nom de la texture</param>
        /// <param name="displayed">Est affiché</param>
        /// <param name="offset">Décalage de la position du Sprite (Vec2(0))</param>
        public SpriteComponent(string sprite, bool displayed = true, Vec2 offset = null): base()
        {
            this.sprite = sprite;
            this.displayed = displayed;
            this.offset = offset ?? new Vec2(0);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && displayed && sprite.Length > 0) {
                var texture = GetWindow().textureManager.GetTexture(sprite);
                GetSpriteBatch().Draw(texture, (tc.position + offset - CameraManager.position).ToMG(), null, Color.WHITE.ToMG(), MathHelper.ToRadians(tc.rotation), new Vector2(texture.Width, texture.Height) / 2, tc.scale.ToMG(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
            }
        }

        public override string ToString() => $"SpriteComponent(sprite={sprite}, displayed={displayed}, offset={offset})";
    }
}
