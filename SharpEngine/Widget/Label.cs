using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Widget;

/// <summary>
/// Class which display Label
/// </summary>
public class Label: Widget
{
    /// <summary>
    /// Text of Label
    /// </summary>
    public string Text;
    
    /// <summary>
    /// Font of Label
    /// </summary>
    public string Font;
    
    /// <summary>
    /// Color of Label
    /// </summary>
    public Color Color;
    
    /// <summary>
    /// If Lines is Centered
    /// </summary>
    public bool CenterAllLines;
    
    /// <summary>
    /// Rotation of Label
    /// </summary>
    public int Rotation;

    /// <summary>
    /// Font Size of Label (or Null)
    /// </summary>
    public int? FontSize;

    /// <summary>
    /// Create Label
    /// </summary>
    /// <param name="position">Label Position</param>
    /// <param name="text">Label Text</param>
    /// <param name="font">Label Font</param>
    /// <param name="color">Label Color</param>
    /// <param name="rotation">Label Rotation</param>
    /// <param name="centerAllLines">If Label Lines is Centered</param>
    /// <param name="fontSize">Label Font Size (or null)</param>
    public Label(Vec2 position, string text = "", string font = "", Color? color = null, int rotation = 0,
        bool centerAllLines = false, int? fontSize = null) : base(position)
    {
        Text = text;
        Font = font;
        Color = color ?? Color.Black;
        Rotation = rotation;
        CenterAllLines = centerAllLines;
        FontSize = fontSize;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        var font = Scene?.Window?.FontManager.GetFont(Font);
        
        if(!Displayed || Scene == null || Text.Length <= 0 || Font.Length <= 0 || font == null) return;

        var realPosition = RealPosition;
        var fontSize = FontSize ?? font.Value.baseSize;
        var textSize = Raylib.MeasureTextEx(font.Value, Text, fontSize, 2);
        
        if(!CenterAllLines)
            Raylib.DrawTextPro(font.Value, Text, realPosition, textSize / 2, Rotation, fontSize, 2, Color);
        else
        {
            var lines = Text.Split("\n");
            for (var i = 0; i < lines.Length; i++)
            {
                var finalPosition = new Vec2(
                    realPosition.X,
                    realPosition.Y - textSize.Y / 2 + i * textSize.Y / lines.Length + textSize.Y / (2 * lines.Length));
                Raylib.DrawTextPro(font.Value, lines[i], finalPosition,
                    Raylib.MeasureTextEx(font.Value, lines[i], fontSize, 2) / 2, Rotation, fontSize, 2, Color);

            }
        }
    }
}