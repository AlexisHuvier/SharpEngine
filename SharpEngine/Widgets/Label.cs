namespace SharpEngine.Widgets
{
    /// <summary>
    /// Label
    /// </summary>
    public class Label: Widget
    {
        public string text;
        public string font;
        public Color color;

        /// <summary>
        /// Initialise le Widget.
        /// </summary>
        /// <param name="position">Position (Vec2(0))</param>
        /// <param name="text">Texte</param>
        /// <param name="font">Nom de la police</param>
        /// <param name="color">Couleur du texte (Color.BLACK)</param>
        public Label(Vec2 position = null, string text = "", string font = "", Color color = null): base(position)
        {
            this.text = text;
            this.font = font;
            this.color = color ?? Color.BLACK;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!displayed || scene == null)
                return;

            if(text.Length > 0 && font.Length > 0)
            {
                Vec2 position = parent != null ? this.position + parent.position : this.position;
                var spriteFont = scene.window.fontManager.GetFont(font);
                scene.window.internalGame.spriteBatch.DrawString(spriteFont, text, position.ToMG(), color.ToMG(), 0, spriteFont.MeasureString(text) / 2, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
            }
        }
    }
}
