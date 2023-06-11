using System.Numerics;
using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Component;

/// <summary>
/// Component which display Text
/// </summary>
public class TextComponent: Component
{
    /// <summary>
    /// Text which be displayed
    /// </summary>
    public string Text;
    
    /// <summary>
    /// Name of Font
    /// </summary>
    public string Font;
    
    /// <summary>
    /// Color of Text
    /// </summary>
    public Color Color;
    
    /// <summary>
    /// Define if Text is displayed
    /// </summary>
    public bool Displayed;
    
    /// <summary>
    /// Offset of Text
    /// </summary>
    public Vec2 Offset;
    
    /// <summary>
    /// Font Size (can be null and use basic size of Font)
    /// </summary>
    public int? FontSize;
    
    private TransformComponent? _transformComponent;

    /// <summary>
    /// Create TextComponent
    /// </summary>
    /// <param name="text">Text</param>
/// <param name="font">Font Name (RAYLIB_DEFAULT)</param>
    /// <param name="color">Color (Color.Black)</param>
    /// <param name="displayed">If Text is Displayed (true)</param>
    /// <param name="fontSize">Font Size (null)</param>
    /// <param name="offset">Offset (Vec2(0))</param>
    public TextComponent(string text, string font = "RAYLIB_DEFAULT", Color? color = null, bool displayed = true,
        int? fontSize = null, Vec2? offset = null)
    {
        Text = text;
        Font = font;
        Color = color ?? Color.Black;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FontSize = fontSize;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transformComponent = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Entity?.Scene?.Window;
        
        if(_transformComponent == null || !Displayed || Text.Length <= 0 || Font.Length <= 0 || window == null) return;

        var font = window.FontManager.GetFont(Font);
        var fontSize = FontSize ?? font.baseSize;
        var position = _transformComponent.GetTransformedPosition(Offset);
        var textSize = Raylib.MeasureTextEx(font, Text, fontSize, 2);
        Raylib.DrawTextEx(font, Text, new Vector2(position.X - textSize.X / 2, position.Y - textSize.Y / 2),
            fontSize, 2, Color);
    }
}