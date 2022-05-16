namespace SharpEngine.Components
{
    /// <summary>
    /// Composant ajoutant l'affichage d'un texte
    /// </summary>
    public class TextComponent: Component
    {
        public string text;
        public string font;
        public Color color;
        public bool displayed;
        public Vec2 offset;

        /// <summary>
        /// Initialise le Composant.
        /// </summary>
        /// <param name="text">Texte</param>
        /// <param name="font">Nom de la police</param>
        /// <param name="color">Couleur du texte (Color.BLACK)</param>
        /// <param name="displayed">Est affiché</param>
        /// <param name="offset">Décalage de la position du texte (Vec2(0))</param>
        public TextComponent(string text = "", string font = "", Color color = null, bool displayed = true, Vec2 offset = null) : base()
        {
            this.text = text;
            this.font = font;
            this.color = color ?? Color.BLACK;
            this.displayed = displayed;
            this.offset = offset ?? new Vec2(0);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && displayed && text.Length > 0 && font.Length > 0)
            {
                var spriteFont = GetWindow().fontManager.GetFont(font);
                GetSpriteBatch().DrawString(spriteFont, text, (tc.position + offset - CameraManager.position).ToMG(), color.ToMG(), tc.rotation, spriteFont.MeasureString(text) / 2, tc.scale.ToMG(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
            }
        }

        public override string ToString()
        {
            return $"TextComponent(text={text}, font={font}, color={color}, displayed={displayed}, offset={offset})";
        }
    }
}
