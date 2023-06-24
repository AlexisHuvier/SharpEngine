using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Widget;

/// <summary>
/// Class which display frame
/// </summary>
public class Frame: Widget
{
    /// <summary>
    /// Color of Frame Border
    /// </summary>
    public Color BorderColor;
    
    /// <summary>
    /// Size of Frame
    /// </summary>
    public Vec2 Size;
    
    /// <summary>
    /// Size of Frame Border 
    /// </summary>
    public Vec2 BorderSize;
    
    /// <summary>
    /// Color of Frame Background
    /// </summary>
    public Color? BackgroundColor;

    /// <summary>
    /// Create Frame
    /// </summary>
    /// <param name="position">Frame Position</param>
    /// <param name="size">Frame Size</param>
    /// <param name="borderSize">Frame Border Size (Vec2(3))</param>
    /// <param name="borderColor">Frame Border Color (Color.Black)</param>
    /// <param name="backgroundColor">Frame Background Color (null)</param>
    public Frame(Vec2 position, Vec2 size, Vec2? borderSize = null, Color? borderColor = null,
        Color? backgroundColor = null) : base(position)
    {
        BorderColor = borderColor ?? Color.Black;
        Size = size;
        BorderSize = borderSize ?? new Vec2(3);
        BackgroundColor = backgroundColor;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(!Displayed || Size == Vec2.Zero) return;

        var position = RealPosition;
        if (BackgroundColor == null)
        {
            Raylib.DrawRectangle((int)(position.X - Size.X / 2), (int)(position.Y - Size.Y / 2), (int)Size.X,
                (int)BorderSize.Y, BorderColor);
            Raylib.DrawRectangle((int)(position.X - Size.X / 2), (int)(position.Y - Size.Y / 2), (int)BorderSize.X,
                (int)Size.Y, BorderColor);
            Raylib.DrawRectangle((int)(position.X + (Size.X - BorderSize.X) - Size.X / 2),
                (int)(position.Y - Size.Y / 2), (int)BorderSize.X,(int)Size.Y, BorderColor);
            Raylib.DrawRectangle((int)(position.X - Size.X / 2),
                (int)(position.Y + (Size.Y - BorderSize.Y) - Size.Y / 2), (int)Size.X, (int)BorderSize.Y, BorderColor);
        }
        else
        {
            Raylib.DrawRectanglePro(new Rectangle(position.X, position.Y, Size.X, Size.Y), Size / 2, 0, BorderColor);
            var innerSize = new Vec2(Size.X - BorderSize.X * 2, Size.Y - BorderSize.Y * 2);
            Raylib.DrawRectanglePro(
                new Rectangle(position.X, position.Y, innerSize.X, innerSize.Y),
                innerSize / 2, 0, BackgroundColor.Value);
        }
    }
}