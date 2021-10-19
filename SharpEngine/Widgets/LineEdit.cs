using SharpEngine.Inputs;

namespace SharpEngine.Widgets
{
    /// <summary>
    /// Entrée de texte
    /// </summary>
    public class LineEdit: Widget
    {
        public string text;
        public string font;
        public Vec2 size;

        private bool focused;
        private float timer;
        private bool cursor;

        /// <summary>
        /// Initialise le Widget.<para/>
        /// -> Paramètre 1 : Position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 2 : Texte (string) ("")<para/>
        /// -> Paramètre 3 : Nom de la police (string) ("")<para/>
        /// -> Paramètre 4 : Taille (<seealso cref="Vec2"/>) (Vec2(300, 500))<para/>
        /// </summary>
        /// <param name="parameters">Paramètres du Widget</param>
        public LineEdit(params object[] parameters): base(parameters)
        {
            text = "";
            font = "";
            size = new Vec2(300, 50);

            if (parameters.Length >= 2 && parameters[1] is string txt)
                text = txt;
            if (parameters.Length >= 3 && parameters[2] is string fnt)
                font = fnt;
            if (parameters.Length >= 4 && parameters[3] is Vec2 siz)
                size = siz;

            focused = false;
            timer = 500;
            cursor = false;
        }

        public override void TextInput(object sender, Key key, char Character)
        {
            base.TextInput(sender, key, Character);

            if (key == Key.BACK)
            {
                if (text.Length >= 1)
                    text = text[..^1];
            }
            else if ((char.IsSymbol(Character) || char.IsWhiteSpace(Character) || char.IsLetterOrDigit(Character) || char.IsPunctuation(Character)) && font.Length > 1 &&
                scene.window.fontManager.GetFont(font).MeasureString(text + Character).X < size.x - 8)
                text += Character;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(!active)
            {
                cursor = false;
                return;
            }

            if(InputManager.IsMouseButtonPressed(Inputs.MouseButton.LEFT))
            {
                focused = InputManager.MouseInRectangle(position - size / 2, size);
                if (!focused && cursor)
                    cursor = false;
            }

            if(focused)
            {
                if(timer <= 0)
                {
                    cursor = !cursor;
                    timer = 500;
                }
                timer -= (float)gameTime.elapsedGameTime.TotalMilliseconds;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (scene == null || !displayed)
                return;

            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), Color.BLACK.ToMG());
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size - new Vec2(4)) / 2, size - new Vec2(4)).ToMG(), Color.WHITE.ToMG());
            if (font.Length >= 1) {
                var spriteFont = scene.window.fontManager.GetFont(font);
                if (cursor)
                    scene.window.internalGame.spriteBatch.DrawString(spriteFont, text + "I", (position - size / 2 + new Vec2(4, size.y / 2 - spriteFont.MeasureString(text + "I").Y / 2)).ToMG(), Color.BLACK.ToMG());
                else
                    scene.window.internalGame.spriteBatch.DrawString(spriteFont, text, (position - size / 2 + new Vec2(4, size.y / 2 - spriteFont.MeasureString(text).Y / 2)).ToMG(), Color.BLACK.ToMG());
            }
        }
    }
}
