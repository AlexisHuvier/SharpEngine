﻿using System;
using System.Collections.Generic;
using Raylib_cs;
using SharpEngine.Math;
using SharpEngine.Utils.EventArgs;

namespace SharpEngine.Widget;

/// <summary>
/// CLass which display a selector
/// </summary>
public class Selector: Widget
{
    /// <summary>
    /// Values of Selector
    /// </summary>
    public List<string> Values { get; }
    
    /// <summary>
    /// Selected Index of Selector
    /// </summary>
    public int SelectedIndex { get; set; }
    
    /// <summary>
    /// Left Button of Selector
    /// </summary>
    public Button LeftButton { get; }
    
    /// <summary>
    /// Right Button of Selector
    /// </summary>
    public Button RightButton { get; }
    
    /// <summary>
    /// Label of Selector
    /// </summary>
    public Label LabelValue { get; }
    
    /// <summary>
    /// Font of Selector
    /// </summary>
    public string Font { get; set; }
    
    /// <summary>
    /// Font Size of Selector
    /// </summary>
    public int? FontSize { get; set; }
    
    /// <summary>
    /// Event trigger when value is changed
    /// </summary>
    public event EventHandler<ValueEventArgs<string>>? ValueChanged; 
    
    /// <summary>
    /// Selected Value
    /// </summary>
    public string Selected => Values[SelectedIndex];
    
    /// <summary>
    /// Create Selector
    /// </summary>
    /// <param name="position"></param>
    /// <param name="values"></param>
    /// <param name="font"></param>
    /// <param name="currentIndex"></param>
    /// <param name="fontSize"></param>
    public Selector(Vec2 position, List<string>? values = null, string font = "", int currentIndex = 0, int? fontSize = null) : base(position)
    {
        Values = values ?? new List<string>();
        SelectedIndex = currentIndex;
        Font = font;
        FontSize = fontSize;

        LabelValue = new Label(Vec2.Zero, Selected, font, fontSize: fontSize);
        LeftButton = new Button(Vec2.Zero, "<", font, new Vec2(30), fontSize: fontSize);
        RightButton = new Button(Vec2.Zero, ">", font, new Vec2(30), fontSize: fontSize);

        AddChild(LabelValue);
        AddChild(LeftButton).Clicked += LeftButtonClick;
        AddChild(RightButton).Clicked += RightButtonClick;
    }

    private void LeftButtonClick(object? sender, EventArgs e)
    {
        var old = Selected;
        if (SelectedIndex == 0)
            SelectedIndex = Values.Count - 1;
        else
            SelectedIndex--;
        LabelValue.Text = Selected;
        
        ValueChanged?.Invoke(this, new ValueEventArgs<string>
        {
            OldValue = old,
            NewValue = Selected
        });
    }

    private void RightButtonClick(object? sender, EventArgs e)
    {
        var old = Selected;
        if (SelectedIndex == Values.Count - 1)
            SelectedIndex = 0;
        else
            SelectedIndex++;
        LabelValue.Text = Selected;
        
        ValueChanged?.Invoke(this, new ValueEventArgs<string>
        {
            OldValue = old,
            NewValue = Selected
        });
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        var font = Scene?.Window?.FontManager.GetFont(Font);
        
        if (font != null)
        {
            var fontSize = FontSize ?? font.Value.baseSize;
            var maxSizeX = 0f;
            foreach (var value in Values)
            {
                var size = Raylib.MeasureTextEx(font.Value, value, fontSize, 2);
                if (maxSizeX < size.X)
                    maxSizeX = size.X;
            }

            LeftButton.Position = new Vec2(-maxSizeX / 2 - 30, 0);
            RightButton.Position = new Vec2(maxSizeX / 2 + 30, 0);
        }
    }
}