namespace SharpEngine.Widgets
{
    public class Label: Widget
    {
        public string text;
        public string font;
        public Color color;

        public Label(params object[] parameters): base(parameters)
        {
            text = "";
            font = "";
            color = Color.BLACK;


            if (parameters.Length >= 2 && parameters[1] is string txt)
                text = txt;
            if (parameters.Length >= 3 && parameters[2] is string fnt)
                font = fnt;
            if (parameters.Length >= 4 && parameters[3] is Color clr)
                color = clr;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!displayed || scene == null)
                return;

            if(text.Length > 0 && font.Length > 0)
            {
                var spriteFont = scene.window.fontManager.GetFont(font);
                scene.window.internalGame.spriteBatch.DrawString(spriteFont, text, position.ToMonoGameVector(), color.ToMonoGameColor(), 0, spriteFont.MeasureString(text) / 2, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
            }

            base.Draw(gameTime);
        }
    }
}
