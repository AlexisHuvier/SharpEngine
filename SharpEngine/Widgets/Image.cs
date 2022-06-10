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
        /// Initialise le Widget.
        /// </summary>
        /// <param name="position">Position (Vec2(0))</param>
        /// <param name="texture">Nom de la texture</param>
        /// <param name="size">Taille de l'image</param>
        public Image(Vec2 position = null, string texture = "", Vec2 size = null): base(position)
        {
            this.texture = texture;
            this.size = size;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if(displayed && texture.Length > 0) {
                var sprite = scene.window.textureManager.GetTexture(texture);
                Vec2 position = parent != null ? this.position + parent.position : this.position;

                if (size == null)
                    scene.window.internalGame.spriteBatch.Draw(sprite, new Rect(position - new Vec2(sprite.Width, sprite.Height) / 2, new Vec2(sprite.Width, sprite.Height)).ToMG(), Color.WHITE.ToMG());
                else
                    scene.window.internalGame.spriteBatch.Draw(sprite, new Rect(position - size / 2, size).ToMG(), Color.WHITE.ToMG());
            }
        }
    }
}
