using System;
using Raylib_cs;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils.EventArgs;
using Color = SharpEngine.Utils.Color;
using MouseButton = SharpEngine.Utils.Input.MouseButton;

namespace SharpEngine.Widget;

/// <summary>
/// Class which display Checkbox
/// </summary>
public class Checkbox: Widget
{
    /// <summary>
    /// Size of Checkbox
    /// </summary>
    public Vec2 Size;
    
    /// <summary>
    /// If Checkbox is Checked
    /// </summary>
    public bool IsChecked;
    
    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<bool>>? ValueChanged; 

    /// <summary>
    /// Create Checkbox
    /// </summary>
    /// <param name="position">Checkbox Position</param>
    /// <param name="size">Checkbox Size</param>
    /// <param name="isChecked">If Checkbox is Checked</param>
    public Checkbox(Vec2 position, Vec2? size = null, bool isChecked = false) : base(position)
    {
        Size = size ?? new Vec2(20);
        IsChecked = isChecked;
    }
    
    /// <inheritdoc />
    public override Rect GetDisplayedRect() => new (RealPosition - Size / 2, Size);

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);
        
        if(!Active) return;

        if (InputManager.IsMouseButtonPressed(MouseButton.Left) &&
            InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size)))
        {
            IsChecked = !IsChecked;
            ValueChanged?.Invoke(this, new ValueEventArgs<bool>()
            {
                OldValue = !IsChecked,
                NewValue = IsChecked
            });
        }
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(!Displayed || Size == Vec2.Zero) return;
        
        var position = RealPosition;
        Raylib.DrawRectanglePro(new Rectangle(position.X, position.Y, Size.X, Size.Y), Size / 2, 0, Color.Black);
        Raylib.DrawRectanglePro(new Rectangle(position.X, position.Y, Size.X - 4, Size.Y - 4), (Size - 4) / 2,
            0, Color.White);
        
        if(IsChecked)
            Raylib.DrawRectanglePro(new Rectangle(position.X, position.Y, Size.X - 6, Size.Y - 6), (Size - 6) / 2,
            0, Color.Black);
    }
}