using System;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Slider
/// </summary>
public class Slider : Widget
{
    public Color Color { get; set; }
    public Vec2 Size { get; set; }
    public int Value { get; set; }
    public string Font { get; set; }
    public Color FontColor { get; set; }

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="color">Couleur de la barre (Color.GREEN)</param>
    /// <param name="size">Taille (Vec2(200, 30))</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="fontColor">Couleur de la police (Color.BLACK)</param>
    /// <param name="value">Valeur</param>
    public Slider(Vec2? position = null, Color color = null, Vec2? size = null, string font = "", Color fontColor = null, int value = 0) : base(position)
    {
        Color = color ?? Color.Green;
        Size = size ?? new Vec2(200, 30);
        Font = font;
        FontColor = fontColor ?? Color.Black;
        Value = value;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsMouseButtonDown(MouseButton.Left))
        {
            var realPosition = GetRealPosition() - Size / 2;
            if (!InputManager.MouseInRectangle(realPosition, Size)) return;
            
            
            var barSize = Size.X;
            var point = InputManager.GetMousePosition().X - realPosition.X;
            Value = (int)Math.Round(point * 100 / barSize, MidpointRounding.AwayFromZero);
        }
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Scene == null || Font == ""  || !Displayed)
            return;

        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;
        var blankTexture = Scene.Window.TextureManager.GetTexture("blank");
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), Color.Black, LayerDepth);
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - (Size - new Vec2(4)) / 2, (Size - new Vec2(4))), Color.White, LayerDepth);
        var barSize = (Size - new Vec2(8));
        var realSize = new Vec2(barSize.X * Value / 100, barSize.Y);
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - barSize / 2, realSize), Color, LayerDepth);
        var font = Scene.Window.FontManager.GetFont(Font);
        Renderer.RenderText(Scene.Window, font, Value.ToString(), realPosition - font.MeasureString(Value.ToString()) / 2, FontColor);
    }
}