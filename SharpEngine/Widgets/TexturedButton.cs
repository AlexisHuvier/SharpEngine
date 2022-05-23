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
        /// Initialise le Widget.
        /// </summary>
        /// <param name="position">Position (Vec2(0))</param>
        /// <param name="text">Texte</param>
        /// <param name="font">Nom de la police</param>
        /// <param name="texture">Nom de la texture</param>
        /// <param name="size">Taille</param>
        /// <param name="fontColor">Couleur du texte (Color.BLACK)</param>
        public TexturedButton(Vec2 position = null, string text = "", string font = "", string texture = "", Vec2 size = null, 
            Color fontColor = null) : base(position)
        {
            this.text = text;
            this.font = font;
            this.texture = texture;
            this.size = size;
            this.fontColor = fontColor ?? Color.BLACK;
            state = ButtonState.IDLE;
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
