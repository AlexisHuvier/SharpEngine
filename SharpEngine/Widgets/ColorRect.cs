using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

public class ColorRect: Widget
{
    public Color Color;
    public Vec2 Size;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="size">Taille du rectangle (Vec2(1))</param>
    /// <param name="color">Couleur du rectangle</param>
    public ColorRect(Vec2? position = null, Vec2? size = null, Color? color = null) : base(position)
    {
        Color = color ?? Color.Black;
        Size = size ?? Vec2.One;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        if (!Displayed || Scene == null) return;

        var texture = Scene.Window.TextureManager.GetTexture("blank");
        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;
        Renderer.RenderTexture(Scene.Window, texture,
            new Rect(realPosition.X - Size.X / 2, realPosition.Y - Size.Y / 2, Size), Color, LayerDepth);
    }
}