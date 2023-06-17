using System.Numerics;
using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Component;

/// <summary>
/// Component which draw rectangle
/// </summary>
public class RectComponent: Component
{
    /// <summary>
    /// Color of Rectangle
    /// </summary>
    public Color Color;
    
    /// <summary>
    /// Size of Rectangle
    /// </summary>
    public Vec2 Size;
    
    /// <summary>
    /// If Rectangle is displayed
    /// </summary>
    public bool Displayed;
    
    /// <summary>
    /// Offset of Rectangle
    /// </summary>
    public Vec2 Offset;

    private TransformComponent? _transform;

    /// <summary>
    /// Create Rect Component
    /// </summary>
    /// <param name="color">Rectangle Color</param>
    /// <param name="size">Rectangle Size</param>
    /// <param name="displayed">If displayed</param>
    /// <param name="offset">Rectangle Offset</param>
    public RectComponent(Color color, Vec2 size, bool displayed = true, Vec2? offset = null)
    {
        Color = color;
        Size = size;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        _transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(_transform == null || !Displayed) return;

        var size = Size * _transform.Scale;
        var position = _transform.GetTransformedPosition(Offset);

        Raylib.DrawRectanglePro(new Rectangle(position.X, position.Y, size.X, size.Y), new Vector2(size.X, size.Y),
            _transform.Rotation, Color);
    }
}