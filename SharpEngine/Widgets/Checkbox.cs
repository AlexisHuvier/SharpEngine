using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Checkbox
/// </summary>
public class Checkbox : Widget
{
    public string Text;
    public string Font;
    public float Scale;
    public Color FontColor;
    public bool IsChecked;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="scale">Echelle</param>
    /// <param name="fontColor">Couleur du texte (Color.BLACK)</param>
    /// <param name="isChecked">Est cochée</param>
    public Checkbox(Vec2? position = null, string text = "", string font = "", int scale = 1, Color? fontColor = null,
        bool isChecked = false) : base(position)
    {
        Text = text;
        Font = font;
        Scale = scale;
        FontColor = fontColor ?? Color.Black;
        IsChecked = isChecked;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!Active)
            return;

        Vec2 size;
        if (Text.Length > 0 && Font.Length > 0)
            size = new Vec2(20, 20) * Scale + new Vec2(8, 0) +
                   new Vec2(Scene.Window.FontManager.GetFont(Font).MeasureString(Text).X, 0);
        else
            size = new Vec2(20) * Scale;

        if (InputManager.IsMouseButtonPressed(MouseButton.Left) &&
            InputManager.MouseInRectangle(GetRealPosition() - size / 2, new Vec2(20) * Scale))
            IsChecked = !IsChecked;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Scene == null || !Displayed)
            return;

        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;
        var blankTexture = Scene.Window.TextureManager.GetTexture("blank");

        if (Text.Length > 0 && Font.Length > 0)
        {
            var size = new Vec2(
                20 * Scale + 8 + Scene.Window.FontManager.GetFont(Font).MeasureString(Text).X,
                20 * Scale
            );

            Renderer.RenderTexture(
                Scene.Window, blankTexture, 
                new Rect(realPosition.X - size.X / 2, realPosition.Y - size.Y / 2, 20 * Scale, 20 * Scale), Color.Black, 
                LayerDepth + 0.00001f);
            Renderer.RenderTexture(
                Scene.Window, blankTexture,
                new Rect(realPosition.X + 2 * Scale - size.X / 2, realPosition.Y + 2 * Scale - size.Y / 2, 16 * Scale, 16 * Scale)
                , Color.White, LayerDepth + 0.00002f);
            if (IsChecked)
                Renderer.RenderTexture(
                    Scene.Window, blankTexture, 
                    new Rect(realPosition.X + 3 * Scale - size.X / 2, realPosition.Y + 3 * Scale - size.Y / 2, 14 * Scale, 14 * Scale), 
                    Color.Black, LayerDepth + 0.00003f);
            var spriteFont = Scene.Window.FontManager.GetFont(Font);
            Renderer.RenderText(
                Scene.Window, spriteFont, Text,
                new Vec2(
                    realPosition.X - size.X / 2 + 20 * Scale + 8,
                    realPosition.Y - size.Y / 2 + 20 * Scale / 2 - spriteFont.MeasureString(Text).Y / 2), 
                FontColor, LayerDepth + 0.00004f);
        }
        else
        {
            Renderer.RenderTexture(
                Scene.Window, blankTexture, 
                new Rect(realPosition.X - 10 * Scale, realPosition.Y - 10 * Scale, 20 * Scale, 20 * Scale), 
                Color.Black, LayerDepth + 0.00005f);
            Renderer.RenderTexture(
                Scene.Window, blankTexture, 
                new Rect(realPosition.X - 8 * Scale, realPosition.Y - 8 * Scale, 16 * Scale, 16 * Scale),
                Color.White, LayerDepth + 0.00006f);
            if (IsChecked)
                Renderer.RenderTexture(
                    Scene.Window, blankTexture, 
                    new Rect(realPosition.X - 7 * Scale, realPosition.Y - 7 * Scale, 14 * Scale, 14 * Scale), 
                    Color.Black, LayerDepth + 0.00007f);
        }
    }
}