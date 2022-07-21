using SharpEngine.Utils;

namespace SharpEngine.Widgets;

/// <summary>
/// Image
/// </summary>
public class Image : Widget
{
    public string Texture;
    public Vec2 Size;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="texture">Nom de la texture</param>
    /// <param name="size">Taille de l'image</param>
    public Image(Vec2 position = null, string texture = "", Vec2 size = null) : base(position)
    {
        Texture = texture;
        Size = size;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (!Displayed || Texture.Length <= 0) return;
        
        var sprite = Scene.Window.TextureManager.GetTexture(Texture);
        var realPosition = Parent != null ? Position + Parent.Position : Position;

        if (Size == null)
            Scene.Window.InternalGame.SpriteBatch.Draw(sprite,
                new Rect(realPosition - new Vec2(sprite.Width, sprite.Height) / 2,
                    new Vec2(sprite.Width, sprite.Height)).ToMg(), Color.White.ToMg());
        else
            Scene.Window.InternalGame.SpriteBatch.Draw(sprite, new Rect(realPosition - Size / 2, Size).ToMg(),
                Color.White.ToMg());
    }
}
