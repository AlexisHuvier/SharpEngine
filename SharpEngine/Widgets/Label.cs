using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Label
/// </summary>
public class Label : Widget
{
    public string Text;
    public string Font;
    public Color Color;
    public bool CenterAllLines;
    public Vec2 Scale;
    public int Rotation;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="color">Couleur du texte (Color.BLACK)</param>
    /// <param name="rotation">Rotation du texte</param>
    /// <param name="scale">Scale du texte</param>
    /// <param name="centerAllLines">Centrer toutes les lignes (false)</param>
    public Label(Vec2? position = null, string text = "", string font = "", Color? color = null, int rotation = 0, Vec2? scale = null, bool centerAllLines = false) : base(position)
    {
        Text = text;
        Font = font;
        Color = color ?? Color.Black;
        Scale = scale ?? Vec2.One;
        Rotation = rotation;
        CenterAllLines = centerAllLines;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (!Displayed || Scene == null || Text.Length <= 0 || Font.Length <= 0)
            return;
        
        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;
        var spriteFont = Scene.Window.FontManager.GetFont(Font);

        if (!CenterAllLines)
        {
            Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition, Color, Rotation,
                spriteFont.MeasureString(Text) / 2, Scale, SpriteEffects.None, LayerDepth);
        }
        else
        {
            var fullTextSizeY = spriteFont.MeasureString(Text).Y * Scale.Y;
            var lines = Text.Split("\n");
            var nbLines = lines.Length;
            for(var i = 0; i < nbLines; i++)
            {
                var finalPosition = new Vec2(realPosition.X,
                    realPosition.Y - fullTextSizeY / 2 + i * fullTextSizeY / nbLines + fullTextSizeY / (2 * nbLines));
                Renderer.RenderText(Scene.Window, spriteFont, lines[i], finalPosition, Color, Rotation,
                    spriteFont.MeasureString(lines[i]) / 2, Scale, SpriteEffects.None, LayerDepth);
            }
        }
    }
}
