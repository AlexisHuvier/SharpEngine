using System.Numerics;
using Raylib_cs;
using SharpEngine.Math;

namespace SharpEngine.Widget;

/// <summary>
/// Class which display Image
/// </summary>
public class Image: Widget
{
    /// <summary>
    /// Name of Texture which be displayed
    /// </summary>
    public string Texture;

    /// <summary>
    /// Scale of Image
    /// </summary>
    public Vec2 Scale;
    
    /// <summary>
    /// Rotation of Image
    /// </summary>
    public int Rotation;

    /// <summary>
    /// Create Image
    /// </summary>
    /// <param name="position">Image Position</param>
    /// <param name="texture">Image Texture ("")</param>
    /// <param name="scale">Image Scale (Vec2(1))</param>
    /// <param name="rotation">Image Rotation (0)</param>
    public Image(Vec2 position, string texture = "", Vec2? scale = null, int rotation = 0) : base(position)
    {
        Texture = texture;
        Scale = scale ?? Vec2.One;
        Rotation = rotation;
    }

    /// <inheritdoc />
    public override Rect GetDisplayedRect()
    {
        var window = Scene?.Window;
        
        if(!Displayed || Texture.Length <= 0 || window == null) return new Rect();

        var texture = window.TextureManager.GetTexture(Texture);
        var size = new Vec2(texture.width * Scale.X, texture.height * Scale.Y);
        return new Rect(RealPosition - size / 2, size);
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        var window = Scene?.Window;
        
        if(!Displayed || Texture.Length <= 0 || window == null) return;

        var texture = window.TextureManager.GetTexture(Texture);
        var position = RealPosition;
        Raylib.DrawTexturePro(texture, new Rectangle(0, 0, texture.width, texture.height),
            new Rectangle(position.X, position.Y, texture.width * Scale.X, texture.height * Scale.Y),
            new Vector2(texture.width / 2f * Scale.X, texture.height / 2f * Scale.Y), Rotation, Color.WHITE);
    }
}