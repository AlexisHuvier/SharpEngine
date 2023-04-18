using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Widgets;

/// <summary>
/// Image
/// </summary>
public class Image : Widget
{
    public string Texture { get; set; }
    public Vec2 Size { get; set; }
    public Rect? SourceRect { get; set; }
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }
    public Vec2 Scale { get; set; }
    public int Rotation { get; set; }

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
    public Image(Vec2? position = null, string texture = "", Vec2? size = null, Rect? sourceRect = null, 
        bool flipX = false, bool flipY = false, Vec2? scale = null, int rotation = 0) : base(position)
    {
        Texture = texture;
        Size = size ?? Vec2.Zero;
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
            var scale = Size == Vec2.Zero ? Scale : new Vec2(Size.X / sprite.Width * Scale.X, Size.Y / sprite.Height * Scale.Y);
            Renderer.RenderTexture(Scene.Window, sprite, realPosition, null, Color.White, 
                MathHelper.ToRadians(Rotation), new Vec2(sprite.Width, sprite.Height) / 2, scale, 
                effects, 1);
        }
        else
            Renderer.RenderTexture(Scene.Window, sprite, realPosition, SourceRect, Color.Wheat, 
                MathHelper.ToRadians(Rotation), SourceRect.Value.Size / 2, Scale, effects, 1);
    }
}
