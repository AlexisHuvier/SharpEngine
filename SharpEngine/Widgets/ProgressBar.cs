namespace SharpEngine.Widgets
{
    /// <summary>
    /// Barre de progresssion
    /// </summary>
    public class ProgressBar: Widget
    {
        public Color color;
        public Vec2 size;
        public int value;

        /// <summary>
        /// Initialise le Widget.
        /// </summary>
        /// <param name="position">Position (Vec2(0))</param>
        /// <param name="color">Couleur de la barre (Color.GREEN)</param>
        /// <param name="size">Taille (Vec2(200, 30))</param>
        /// <param name="value">Valeur</param>
        public ProgressBar(Vec2 position = null, Color color = null, Vec2 size = null, int value = 0): base(position)
        {
            this.color = color ?? Color.GREEN;
            this.size = size ?? new Vec2(200, 30);
            this.value = value;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (scene == null || !displayed)
                return;

            Vec2 position = parent != null ? this.position + parent.position : this.position;
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), Color.BLACK.ToMG());
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size - new Vec2(4)) / 2, (size - new Vec2(4))).ToMG(), Color.WHITE.ToMG());
            Vec2 barSize = (size - new Vec2(8));
            Vec2 realSize = new Vec2(barSize.x * value / 100, barSize.y);
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - barSize / 2, realSize).ToMG(), color.ToMG());
        }
    }
}
