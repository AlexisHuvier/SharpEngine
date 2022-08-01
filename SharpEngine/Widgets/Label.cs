using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Utils;

namespace SharpEngine.Widgets;

/// <summary>
/// Label
/// </summary>
public class Label : Widget
{
    public string Text;
    public string Font;
    public Color Color;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="color">Couleur du texte (Color.BLACK)</param>
    public Label(Vec2 position = null, string text = "", string font = "", Color color = null) : base(position)
    {
        Text = text;
        Font = font;
        Color = color ?? Color.Black;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (!Displayed || Scene == null)
            return;

        if (Text.Length <= 0 || Font.Length <= 0) return;
        
        var realPosition = Parent != null ? Position + Parent.Position : Position;
        var spriteFont = Scene.Window.FontManager.GetFont(Font);
        Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition, Color, 0, spriteFont.MeasureString(Text) / 2, new Vec2(1), SpriteEffects.None, 1);
    }
}
