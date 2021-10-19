namespace SharpEngine.Widgets
{
    /// <summary>
    /// Image
    /// </summary>
    public class Image: Widget
    {
        public string texture;
        public Vec2 size;

        /// <summary>
        /// Initialise le Widget.<para/>
        /// -> Paramètre 1 : Position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 2 : Nom de la texture (string) ("")<para/>
        /// -> Paramètre 3 : Taille (<seealso cref="Vec2"/>) (null)<para/>
        /// </summary>
        /// <param name="parameters">Paramètres du Widget</param>
        public Image(params object[] parameters): base(parameters)
        {
            texture = "";
            size = null;

            if (parameters.Length >= 2 && parameters[1] is string text)
                texture = text;
            if (parameters.Length >= 3 && parameters[2] is Vec2 siz)
                size = siz;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if(displayed && texture.Length > 0) {
                var sprite = scene.window.textureManager.GetTexture(texture);
                if (size == null)
                    scene.window.internalGame.spriteBatch.Draw(sprite, new Rect(position - new Vec2(sprite.Width, sprite.Height) / 2, new Vec2(sprite.Width, sprite.Height)).ToMG(), Color.WHITE.ToMG());
                else
                    scene.window.internalGame.spriteBatch.Draw(sprite, new Rect(position - size / 2, size).ToMG(), Color.WHITE.ToMG());
            }
        }
    }
}
