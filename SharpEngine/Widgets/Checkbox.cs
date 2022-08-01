using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;

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
    public Checkbox(Vec2 position = null, string text = "", string font = "", int scale = 1, Color fontColor = null,
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
            InputManager.MouseInRectangle(Position - size / 2, new Vec2(20) * Scale))
            IsChecked = !IsChecked;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Scene == null || !Displayed)
            return;

        var realPosition = Parent != null ? Position + Parent.Position : Position;
        var blankTexture = Scene.Window.TextureManager.GetTexture("blank");

        if (Text.Length > 0 && Font.Length > 0)
        {
            var size = new Vec2(20) * Scale + new Vec2(8, 0) +
                       new Vec2(Scene.Window.FontManager.GetFont(Font).MeasureString(Text).X, 0);
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - size / 2, new Vec2(20) * Scale), Color.Black);
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition + new Vec2(2 * Scale) - size / 2, new Vec2(16) * Scale), Color.White);
            if (IsChecked)
                Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition + new Vec2(3 * Scale) - size / 2, new Vec2(14) * Scale), Color.Black);
            var spriteFont = Scene.Window.FontManager.GetFont(Font);
            Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition - size / 2 + new Vec2(20 * Scale + 8, 20 * Scale / 2) + new Vec2(0, -spriteFont.MeasureString(Text).Y / 2), FontColor);
        }
        else
        {
            var size = new Vec2(20) * Scale;
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - new Vec2(20) * Scale / 2, new Vec2(20) * Scale), Color.Black);
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - new Vec2(16) * Scale / 2, new Vec2(16) * Scale), Color.White);
            if (IsChecked)
                Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - new Vec2(14) * Scale / 2, new Vec2(14) * Scale), Color.Black);
        }
    }
}