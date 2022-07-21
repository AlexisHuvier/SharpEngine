using SharpEngine.Utils;

namespace SharpEngine.Widgets;

/// <summary>
/// Barre de progresssion
/// </summary>
public class ProgressBar : Widget
{
    public Color Color;
    public Vec2 Size;
    public int Value;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="color">Couleur de la barre (Color.GREEN)</param>
    /// <param name="size">Taille (Vec2(200, 30))</param>
    /// <param name="value">Valeur</param>
    public ProgressBar(Vec2 position = null, Color color = null, Vec2 size = null, int value = 0) : base(position)
    {
        Color = color ?? Color.Green;
        Size = size ?? new Vec2(200, 30);
        Value = value;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Scene == null || !Displayed)
            return;

        var realPosition = Parent != null ? Position + Parent.Position : Position;
        Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
            new Rect(realPosition - Size / 2, Size).ToMg(), Color.Black.ToMg());
        Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
            new Rect(realPosition - (Size - new Vec2(4)) / 2, (Size - new Vec2(4))).ToMg(), Color.White.ToMg());
        var barSize = (Size - new Vec2(8));
        var realSize = new Vec2(barSize.X * Value / 100, barSize.Y);
        Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
            new Rect(realPosition - barSize / 2, realSize).ToMg(), Color.ToMg());
    }
}
