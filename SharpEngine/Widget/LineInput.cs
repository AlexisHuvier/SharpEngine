using System;
using Raylib_cs;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils.EventArgs;
using SharpEngine.Utils.Input;
using Color = SharpEngine.Utils.Color;
using MouseButton = SharpEngine.Utils.Input.MouseButton;

namespace SharpEngine.Widget;

/// <summary>
/// Class which represents Line Input
/// </summary>
public class LineInput: Widget
{
    /// <summary>
    /// Current Text of Line Input
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Font of Line Input
    /// </summary>
    public string Font { get; set; }
    
    /// <summary>
    /// Size of Line Input
    /// </summary>
    public Vec2 Size { get; set; }
    
    /// <summary>
    /// If Line Input is Focused
    /// </summary>
    public bool Focused { get; private set; }
    
    /// <summary>
    /// Font Size of Line Input (or null)
    /// </summary>
    public int? FontSize { get; set; }
    
    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<string>>? ValueChanged;

    /// <summary>
    /// Create Line Input
    /// </summary>
    /// <param name="position">Line Edit Position</param>
    /// <param name="text">Line Edit Text ("")</param>
    /// <param name="font">Line Edit Font ("")</param>
    /// <param name="size">Line Edit Size (Vec2(300, 50))</param>
    /// <param name="fontSize">Line Edit Font Size (null)</param>
    public LineInput(Vec2 position, string text = "", string font = "", Vec2? size = null, int? fontSize = null) : base(position)
    {
        Text = text;
        Font = font;
        Size = size ?? new Vec2(300, 50);
        FontSize = fontSize;
        Focused = false;
    }
    
    /// <inheritdoc />
    public override Rect GetDisplayedRect() => new (RealPosition - Size / 2, Size);

    /// <inheritdoc />
    public override void Update(float delta)
    {
        if (!Active)
        {
            Focused = false;
            return;
        }

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
            Focused = InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size));

        if(!Focused) return;

        #region Text Processing

        if (InputManager.IsKeyPressed(Key.Backspace) && Text.Length >= 1)
        {
            var old = Text;
            Text = Text[..^1];
            ValueChanged?.Invoke(this, new ValueEventArgs<string>
            {
                OldValue = old,
                NewValue = Text
            });
        }

        var font = Scene?.Window?.FontManager.GetFont(Font);
        if (font != null)
        {
            foreach (var pressedChar in InputManager.PressedChars)
            {
                if (char.IsSymbol(pressedChar) || char.IsWhiteSpace(pressedChar) ||
                    char.IsLetterOrDigit(pressedChar) || char.IsPunctuation(pressedChar))
                {
                    Text += pressedChar;
                    ValueChanged?.Invoke(this, new ValueEventArgs<string>
                    {
                        OldValue = Text,
                        NewValue = Text[..^1]
                    });
                }
            }
        }

        #endregion
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(!Displayed || Scene == null ) return;

        var position = RealPosition;

        Raylib.DrawRectanglePro(new Rectangle(position.X, position.Y, Size.X, Size.Y), Size / 2, 0, Color.Black);
        Raylib.DrawRectanglePro(new Rectangle(position.X + 2, position.Y + 2, Size.X - 4, Size.Y - 4), Size / 2, 0,
            Color.White);
        
        var font = Scene?.Window?.FontManager.GetFont(Font);
        
        if(Font.Length <= 0 || font == null) return;
        
        var fontSize = FontSize ?? font.Value.baseSize;
        var textSize = Raylib.MeasureTextEx(font.Value, Text, fontSize, 2);
        var offset = textSize.X - (Size.X - 20);

        if (Text.Length > 0)
        {
            var finalPosition = new Vec2(position.X - Size.X / 2 + 4, position.Y - textSize.Y / 2);

            Raylib.BeginScissorMode((int)finalPosition.X, (int)finalPosition.Y, (int)Size.X - 8, (int)textSize.Y);
            Raylib.DrawTextEx(font.Value, Text, new Vec2(finalPosition.X - (offset > 0 ? offset : 0), finalPosition.Y),
                fontSize, 2, Color.Black);
            Raylib.EndScissorMode();
        }

        if (Focused)
            Raylib.DrawRectangle((int)(position.X - Size.X / 2 + 10 + textSize.X - (offset > 0 ? offset : 0)), (int)(position.Y - textSize.Y / 2 + 4),
                5, (int)textSize.Y - 8, Color.Black);
    }
}