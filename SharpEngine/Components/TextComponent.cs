using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Components
{
    public class TextComponent: Component
    {
        public string text;
        public string font;
        public Color color;
        public bool displayed;

        public TextComponent(params object[] parameters) : base(parameters)
        {
            text = "";
            font = "";
            color = Color.BLACK;
            displayed = true;

            if (parameters.Length >= 1 && parameters[0] is string txt)
                text = txt;
            if (parameters.Length >= 2 && parameters[1] is string fnt)
                font = fnt;
            if (parameters.Length >= 3 && parameters[2] is Color clr)
                color = clr;
            if (parameters.Length >= 4 && parameters[3] is bool disp)
                displayed = disp;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && displayed && text.Length > 0 && font.Length > 0)
            {
                var spriteFont = GetWindow().fontManager.GetFont(font);
                GetSpriteBatch().DrawString(spriteFont, text, (tc.position - CameraManager.position).ToMG(), color.ToMG(), tc.rotation, spriteFont.MeasureString(text) / 2, tc.scale.ToMG(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
            }
        }

        public override string ToString()
        {
            return $"TextComponent(text={text}, font={font}, color={color}, displayed={displayed})";
        }
    }
}
