using System;
using System.Text;
using Raylib_cs;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils.EventArgs;
using SharpEngine.Utils.Input;
using MouseButton = SharpEngine.Utils.Input.MouseButton;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Widget;

public class MultiLineInput: Widget
{
    /// <summary>
    /// Current Text of Multi Line Input
    /// </summary>
    public string Text;
    
    /// <summary>
    /// Font of Multi Line Input
    /// </summary>
    public string Font;
    
    /// <summary>
    /// Size of Multi Line Input
    /// </summary>
    public Vec2 Size;
    
    /// <summary>
    /// If Multi Line Input is Focused
    /// </summary>
    public bool Focused { get; private set; }
    
    /// <summary>
    /// Font Size of Multi Line Input (or null)
    /// </summary>
    public int? FontSize;
    
    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<string>>? ValueChanged; 

    private float _timer;
    private bool _cursor;
    private string? _displayText;

    /// <summary>
    /// Create Multi Line Input
    /// </summary>
    /// <param name="position">Multi Line Edit Position</param>
    /// <param name="text">Multi Line Edit Text ("")</param>
    /// <param name="font">Multi Line Edit Font ("")</param>
    /// <param name="size">Multi Line Edit Size (Vec2(500, 200))</param>
    /// <param name="fontSize">Multi Line Edit Font Size (null)</param>
    public MultiLineInput(Vec2 position, string text = "", string font = "", Vec2? size = null, int? fontSize = null) : base(position)
    {
        Text = text;
        Font = font;
        Size = size ?? new Vec2(500, 200);
        FontSize = fontSize;
        Focused = false;
        _timer = 0.5f;

        ValueChanged += (_, _) => UpdateDisplayText();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        if (!Active)
        {
            _cursor = false;
            return;
        }

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
        {
            Focused = InputManager.IsMouseInRectangle(new Rect(RealPosition - Size / 2, Size));
            if (!Focused && _cursor)
                _cursor = false;
        }
        
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

        if (InputManager.IsKeyPressed(Key.Enter))
        {
            Text += '\n';
            ValueChanged?.Invoke(this, new ValueEventArgs<string>
            {
                OldValue = Text[..^1],
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

        if (_timer <= 0)
        {
            _cursor = !_cursor;
            _timer = 0.5f;
        }

        _timer -= delta;
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
        
        if(Text.Length <= 0 || Font.Length <= 0 || font == null) return;

        var fontSize = FontSize ?? font.Value.baseSize;
        
        if(_displayText == null)
            UpdateDisplayText();

        var textSize = Raylib.MeasureTextEx(font.Value, _displayText.Split("\n")[^1], fontSize, 2);
        
        var finalPosition = new Vec2(position.X - Size.X / 2 + 4, position.Y - Size.Y / 2 + 4);
        
        var lines = _displayText.Split("\n");
        for (var i = 0; i < lines.Length; i++)
        {
            var lineSize = Raylib.MeasureTextEx(font.Value, lines[i], fontSize, 2);
            var pos = new Vec2(finalPosition.X,finalPosition.Y + i * lineSize.Y);
            Raylib.DrawTextEx(font.Value, lines[i], pos, fontSize, 2, Color.Black);

        }

        if (_cursor)
            Raylib.DrawRectangle((int)(finalPosition.X + 6 + textSize.X),
                (int)(finalPosition.Y + textSize.Y * (lines.Length - 1)),
                5, (int)textSize.Y, Color.Black);
    }

    /// <summary>
    /// Update Displayed Text
    /// </summary>
    public void UpdateDisplayText()
    {
        var fontLocal = Scene?.Window?.FontManager.GetFont(Font);
        if (fontLocal == null) return;
            
        var fontSizeLocal = FontSize ?? fontLocal.Value.baseSize;
        _displayText = GetDisplayedText(fontLocal, fontSizeLocal, Size.X - 18, Size.Y, Text);
    }

    private string GetDisplayedText(Font? font, int fontSize, float sizeX, float sizeY, string text)
    {
        var current = new StringBuilder();
        var index = text.Length - 1;
        var textSize = Raylib.MeasureTextEx(font.Value, current.ToString(), fontSize, 2);

        while (index >= 0 && textSize.X <= sizeX && textSize.Y <= sizeY)
        {
            current.Insert(0, text[index]);
            textSize = Raylib.MeasureTextEx(font.Value, current.ToString(), fontSize, 2);
            index--;
        }

        return index == -1 ? text : current.Remove(0, 1).ToString();
    }
}