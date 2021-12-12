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
        /// Initialise le Composant.<para/>
        /// -> Paramètre 1 : Nom de la texture (string) ("")<para/>
        /// -> Paramètre 2 : Est affiché (bool) (true)<para/>
        /// -> Paramètre 3 : Offset (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// </summary>
        /// <param name="parameters">Paramètres du Composant</param>
        public SpriteComponent(params object[] parameters): base(parameters)
        {
            sprite = "";
            displayed = true;
            offset = new Vec2(0);

            if (parameters.Length >= 1 && parameters[0] is string spr)
                sprite = spr;
            if (parameters.Length >= 2 && parameters[1] is bool disp)
                displayed = disp;
            if (parameters.Length >= 3 && parameters[2] is Vec2 off)
                offset = off;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && displayed && sprite.Length > 0) {
                var texture = GetWindow().textureManager.GetTexture(sprite);
                GetSpriteBatch().Draw(texture, (tc.position + offset - CameraManager.position).ToMG(), null, Color.WHITE.ToMG(), MathHelper.ToRadians(tc.rotation), new Vector2(texture.Width, texture.Height) / 2, tc.scale.ToMG(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
            }
        }

        public override string ToString()
        {
            return $"SpriteComponent(sprite={sprite}, displayed={displayed}, offset={offset})";
        }
    }
}
