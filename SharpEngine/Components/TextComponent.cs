﻿namespace SharpEngine.Components
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
        /// Initialise le Composant.<para/>
        /// -> Paramètre 1 : Texte (string) ("")<para/>
        /// -> Paramètre 2 : Nom de la police (string) ("")<para/>
        /// -> Paramètre 3 : Couleur du texte (<seealso cref="Color"/>) (Color.BLACK)<para/>
        /// -> Paramètre 4 : Est affiché (bool) (true)<para/>
        /// -> Paramètre 5 : Offset (<seealso cref="Vec2"/>) (Vec2(0))
        /// </summary>
        /// <param name="parameters">Paramètres du Composant</param>
        public TextComponent(params object[] parameters) : base(parameters)
        {
            text = "";
            font = "";
            color = Color.BLACK;
            displayed = true;
            offset = new Vec2(0);

            if (parameters.Length >= 1 && parameters[0] is string txt)
                text = txt;
            if (parameters.Length >= 2 && parameters[1] is string fnt)
                font = fnt;
            if (parameters.Length >= 3 && parameters[2] is Color clr)
                color = clr;
            if (parameters.Length >= 4 && parameters[3] is bool disp)
                displayed = disp;
            if (parameters.Length >= 5 && parameters[4] is Vec2 off)
                offset = off;
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
