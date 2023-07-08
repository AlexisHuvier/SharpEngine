﻿using System;
using Raylib_cs;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils.EventArgs;
using SharpEngine.Utils.Input;
using MouseButton = SharpEngine.Utils.Input.MouseButton;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Widget;
    
/// <summary>
/// Class which represents Multi Line Input
/// </summary>
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

        var textSize = Raylib.MeasureTextEx(font.Value, Text.Split("\n")[^1], fontSize, 2);
        
        var finalPosition = new Vec2(position.X - Size.X / 2 + 4, position.Y - Size.Y / 2 + 4);
        
        var lines = Text.Split("\n");
        var offsetX = textSize.X - (Size.X - 20);
        var offsetY = textSize.Y * lines.Length - (Size.Y - 8);
        
        Raylib.BeginScissorMode((int)finalPosition.X, (int)finalPosition.Y, (int)Size.X - 8, (int)Size.Y - 8);
        for (var i = 0; i < lines.Length; i++)
        {
            var lineSize = Raylib.MeasureTextEx(font.Value, lines[i], fontSize, 2);
            var pos = new Vec2(finalPosition.X - (offsetX > 0 ? offsetX : 0),finalPosition.Y + i * lineSize.Y - (offsetY > 0 ? offsetY : 0));
            Raylib.DrawTextEx(font.Value, lines[i], pos, fontSize, 2, Color.Black);

        }
        Raylib.EndScissorMode();

        if (Focused)
            Raylib.DrawRectangle((int)(finalPosition.X + 6 + textSize.X - (offsetX > 0 ? offsetX : 0)),
                (int)(finalPosition.Y + textSize.Y * (lines.Length - 1)  - (offsetY > 0 ? offsetY : 0)),
                5, (int)textSize.Y, Color.Black);
    }
}