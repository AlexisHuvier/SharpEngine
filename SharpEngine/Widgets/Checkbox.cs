namespace SharpEngine.Widgets
{
    public class Checkbox: Widget
    {
        public string text;
        public string font;
        public float scale;
        public Color fontColor;
        public bool isChecked;

        public Checkbox(params object[] parameters): base(parameters)
        {
            text = "";
            font = "";
            scale = 1;
            fontColor = Color.BLACK;
            isChecked = false;

            if (parameters.Length >= 2 && parameters[1] is string txt)
                text = txt;
            if (parameters.Length >= 3 && parameters[2] is string fnt)
                font = fnt;
            if (parameters.Length >= 4 && parameters[3] is float sca)
                scale = sca;
            if (parameters.Length >= 5 && parameters[4] is Color color)
                fontColor = color;
            if (parameters.Length >= 6 && parameters[5] is bool check)
                isChecked = check;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!active)
                return;

            Vec2 size;
            if (text.Length > 0 && font.Length > 0)
                size = new Vec2(20, 20) * scale + new Vec2(8, 0) + new Vec2(scene.window.fontManager.GetFont(font).MeasureString(text).X, 0);
            else
                size = new Vec2(20) * scale;

            if (InputManager.IsMouseButtonPressed(Inputs.MouseButton.LEFT) && InputManager.MouseInRectangle(position - size / 2, new Vec2(20) * scale))
                isChecked = !isChecked;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (scene == null || !displayed)
                return;

            if (text.Length > 0 && font.Length > 0)
            {
                Vec2 size = new Vec2(20) * scale + new Vec2(8, 0) + new Vec2(scene.window.fontManager.GetFont(font).MeasureString(text).X, 0);
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, new Vec2(20) * scale).ToMG(), Color.BLACK.ToMG());
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position + new Vec2(2 * scale) - size / 2, new Vec2(16) * scale).ToMG(), Color.WHITE.ToMG());
                if (isChecked)
                    scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position + new Vec2(3 * scale) - size / 2, new Vec2(14) * scale).ToMG(), Color.BLACK.ToMG());
                var spriteFont = scene.window.fontManager.GetFont(font);
                scene.window.internalGame.spriteBatch.DrawString(spriteFont, text, (position - size / 2 + new Vec2(20 * scale + 8, 20 * scale / 2) + new Vec2(0, -spriteFont.MeasureString(text).Y / 2)).ToMG(), fontColor.ToMG());
            }
            else
            {
                Vec2 size = new Vec2(20) * scale;
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), Color.BLACK.ToMG());
                scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size - new Vec2(4 * scale)) / 2, (size - new Vec2(4 * scale))).ToMG(), Color.WHITE.ToMG());
                if(isChecked)
                    scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size - new Vec2(8 * scale)) / 2, (size - new Vec2(8 * scale))).ToMG(), Color.BLACK.ToMG());
            }
        }
    }
}
