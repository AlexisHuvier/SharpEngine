using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Image
/// </summary>
public class Image : Widget
{
    public string Texture;
    public Vec2 Size;
    public Rect SourceRect;
    public bool FlipX;
    public bool FlipY;
    public Vec2 Scale;
    public int Rotation;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="texture">Nom de la texture</param>
    /// <param name="size">Taille de l'image</param>
    /// <param name="sourceRect">Partie de la texture à afficher</param>
    /// <param name="flipX">Si l'image est retourné en X</param>
    /// <param name="flipY">Si l'image est retourné en Y</param>
    /// <param name="scale">Scale de l'image</param>
    /// <param name="rotation">Rotation de l'image</param>
    public Image(Vec2 position = null, string texture = "", Vec2 size = null, Rect sourceRect = null, 
        bool flipX = false, bool flipY = false, Vec2 scale = null, int rotation = 0) : base(position)
    {
        Texture = texture;
        Size = size;
        SourceRect = sourceRect;
        FlipX = flipX;
        FlipY = flipY;
        Scale = scale ?? Vec2.One;
        Rotation = rotation;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (!Displayed || Texture.Length <= 0) return;
        
        var sprite = Scene.Window.TextureManager.GetTexture(Texture);
        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;

        var effects = SpriteEffects.None;
        if (FlipX)
            effects |= SpriteEffects.FlipHorizontally;
        if (FlipY)
            effects |= SpriteEffects.FlipVertically;

        if (SourceRect == null)
        { 
            var scale = Size == null ? Scale : new Vec2(Size.X / sprite.Width * Scale.X, Size.Y / sprite.Height * Scale.Y);
            Renderer.RenderTexture(Scene.Window, sprite, realPosition, null, Color.White, Rotation,
                new Vec2(sprite.Width, sprite.Height) / 2, scale, effects, 1);
        }
        else
            Renderer.RenderTexture(Scene.Window, sprite, realPosition, SourceRect, Color.Wheat, Rotation, SourceRect.Size / 2,
                Scale, effects, 1);
    }
}
