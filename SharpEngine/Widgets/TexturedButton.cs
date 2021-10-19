using System;

namespace SharpEngine.Widgets
{
    /// <summary>
    /// Bouton texturé
    /// </summary>
    public class TexturedButton : Widget
    {
        private enum ButtonState
        {
            IDLE,
            CLICK,
            HOVERED
        }

        public string text;
        public string font;
        public string texture;
        public Vec2 size;
        public Color fontColor;
        public Action<TexturedButton> command;

        private ButtonState state;

        /// <summary>
        /// Initialise le Widget.<para/>
        /// -> Paramètre 1 : Position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 2 : Texte (string) ("")<para/>
        /// -> Paramètre 3 : Nom de la police (string) ("")<para/>
        /// -> Paramètre 4 : Nom de la texture (string) ("")<para/>
        /// -> Paramètre 5 : Taille (<seealso cref="Vec2"/>) (null)<para/>
        /// -> Paramètre 6 : Couleur du texte (<seealso cref="Color"/>) (Color.BLACK)
        /// </summary>
        /// <param name="parameters">Paramètres du Widget</param>
        public TexturedButton(params object[] parameters) : base(parameters)
        {
            text = "";
            font = "";
            texture = "";
            size = null;
            fontColor = Color.BLACK;
            state = ButtonState.IDLE;

            if (parameters.Length >= 2 && parameters[1] is string txt)
                text = txt;
            if (parameters.Length >= 3 && parameters[2] is string fnt)
                font = fnt;
            if (parameters.Length >= 4 && parameters[3] is string textu)
                texture = textu;
            if (parameters.Length >= 5 && parameters[4] is Vec2 siz)
                size = siz;
            if (parameters.Length >= 6 && parameters[5] is Color fC)
                fontColor = fC;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (size == null)
            {
                var temp = scene.window.textureManager.GetTexture(texture);
                size = new Vec2(temp.Width, temp.Height);
            }

            if (!active)
                return;

            if (InputManager.MouseInRectangle(new Rect(position - size / 2, size)))
            {
                if (InputManager.IsMouseButtonPressed(Inputs.MouseButton.LEFT) && command != null)
                    command(this);

                if (InputManager.IsMouseButtonDown(Inputs.MouseButton.LEFT))
                    state = ButtonState.CLICK;
                else
                    state = ButtonState.HOVERED;
            }
            else
                state = ButtonState.IDLE;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (size == null)
            {
                var temp = scene.window.textureManager.GetTexture(texture);
                size = new Vec2(temp.Width, temp.Height);
            }

            if (!displayed || scene == null)
                return;

            if (state != ButtonState.CLICK && active && state == ButtonState.HOVERED)
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size + new Vec2(4)) / 2, (size + new Vec2(4))).ToMG(), Color.WHITE.ToMG());

            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), Color.BLACK.ToMG());
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture(texture), new Rect(position - (size - new Vec2(4)) / 2, (size - new Vec2(4))).ToMG(), Color.WHITE.ToMG());

            var spriteFont = scene.window.fontManager.GetFont(font);
            scene.window.internalGame.spriteBatch.DrawString(spriteFont, text, position.ToMG(), fontColor.ToMG(), 0, spriteFont.MeasureString(text) / 2, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);

            if (state == ButtonState.CLICK || !active)
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), new Color(0, 0, 0, 128).ToMG());
        }
    }
}
