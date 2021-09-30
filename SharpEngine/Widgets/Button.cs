using System;

namespace SharpEngine.Widgets
{

    public class Button: Widget
    {
        private enum ButtonState
        {
            IDLE,
            CLICK,
            HOVERED
        }

        public string text;
        public string font;
        public Vec2 size;
        public Color fontColor;
        public Color backgroundColor;
        public Action<Button> command;

        private ButtonState state;

        public Button(params object[] parameters): base(parameters)
        {
            text = "";
            font = "";
            size = new Vec2(200, 40);
            fontColor = Color.BLACK;
            backgroundColor = Color.GRAY;
            state = ButtonState.IDLE;

            if (parameters.Length >= 2 && parameters[1] is string txt)
                text = txt;
            if (parameters.Length >= 3 && parameters[2] is string fnt)
                font = fnt;
            if (parameters.Length >= 4 && parameters[3] is Vec2 siz)
                size = siz;
            if (parameters.Length >= 5 && parameters[4] is Color fC)
                fontColor = fC;
            if (parameters.Length >= 4 && parameters[5] is Color bgC)
                backgroundColor = bgC;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

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

            if (!displayed || scene == null)
                return;

            if (state != ButtonState.CLICK && active && state == ButtonState.HOVERED)
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size + new Vec2(4)) / 2, (size + new Vec2(4))).ToMG(), Color.WHITE.ToMG());

            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), Color.BLACK.ToMG());
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size - new Vec2(4)) / 2, (size - new Vec2(4))).ToMG(), backgroundColor.ToMG());

            var spriteFont = scene.window.fontManager.GetFont(font);
            scene.window.internalGame.spriteBatch.DrawString(spriteFont, text, position.ToMG(), fontColor.ToMG(), 0, spriteFont.MeasureString(text) / 2, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);

            if(state == ButtonState.CLICK || !active)
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), new Color(0, 0, 0, 128).ToMG());
        }
    }
}
