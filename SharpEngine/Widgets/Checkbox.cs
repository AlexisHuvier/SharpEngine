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

        if (Text.Length > 0 && Font.Length > 0)
        {
            var size = new Vec2(20) * Scale + new Vec2(8, 0) +
                       new Vec2(Scene.Window.FontManager.GetFont(Font).MeasureString(Text).X, 0);
            Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                new Rect(realPosition - size / 2, new Vec2(20) * Scale).ToMg(), Color.Black.ToMg());
            Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                new Rect(realPosition + new Vec2(2 * Scale) - size / 2, new Vec2(16) * Scale).ToMg(), Color.White.ToMg());
            if (IsChecked)
                Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                    new Rect(realPosition + new Vec2(3 * Scale) - size / 2, new Vec2(14) * Scale).ToMg(),
                    Color.Black.ToMg());
            var spriteFont = Scene.Window.FontManager.GetFont(Font);
            Scene.Window.InternalGame.SpriteBatch.DrawString(spriteFont, Text,
                (realPosition - size / 2 + new Vec2(20 * Scale + 8, 20 * Scale / 2) +
                 new Vec2(0, -spriteFont.MeasureString(Text).Y / 2)).ToMg(), FontColor.ToMg());
        }
        else
        {
            var size = new Vec2(20) * Scale;
            Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                new Rect(realPosition - size / 2, size).ToMg(), Color.Black.ToMg());
            Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                new Rect(realPosition - (size - new Vec2(4 * Scale)) / 2, (size - new Vec2(4 * Scale))).ToMg(),
                Color.White.ToMg());
            if (IsChecked)
                Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                    new Rect(realPosition - (size - new Vec2(8 * Scale)) / 2, (size - new Vec2(8 * Scale))).ToMg(),
                    Color.Black.ToMg());
        }
    }
}