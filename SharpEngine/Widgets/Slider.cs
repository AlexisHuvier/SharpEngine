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
    public Color Color;
    public Vec2 Size;
    public int Value;
    public string Font;
    public Color FontColor;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="color">Couleur de la barre (Color.GREEN)</param>
    /// <param name="size">Taille (Vec2(200, 30))</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="fontColor">Couleur de la police (Color.BLACK)</param>
    /// <param name="value">Valeur</param>
    public Slider(Vec2? position = null, Color? color = null, Vec2? size = null, string font = "", Color? fontColor = null, int value = 0) : base(position)
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
        Renderer.RenderTexture(
            Scene.Window, blankTexture, 
            new Rect(realPosition.X - Size.X / 2, realPosition.Y - Size.Y / 2, Size), 
            Color.Black, LayerDepth);
        Renderer.RenderTexture(
            Scene.Window, blankTexture,
            new Rect(realPosition.X - (Size.X - 4) / 2, realPosition.Y - (Size.Y - 4) / 2, Size.X - 4, Size.Y - 4),
            Color.White, LayerDepth + 0.00001f);
        Renderer.RenderTexture(
            Scene.Window, blankTexture, 
            new Rect(realPosition.X - (Size.X - 8) / 2, realPosition.Y - (Size.Y - 8) / 2, (Size.X - 8) * Value / 100, Size.Y - 8), 
            Color, LayerDepth + 0.00002f);
        var font = Scene.Window.FontManager.GetFont(Font);
        var fontSize = font.MeasureString(Value.ToString());
        Renderer.RenderText(Scene.Window, font, Value.ToString(),
            new Vec2(realPosition.X - fontSize.X / 2, realPosition.Y - fontSize.Y / 2), FontColor,
            LayerDepth + 0.00003f);
    }
}