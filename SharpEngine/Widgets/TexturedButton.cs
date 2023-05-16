﻿using System;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Bouton texturé
/// </summary>
public class TexturedButton : Widget
{
    private enum ButtonState
    {
        Idle,
        Click,
        Hovered
    }

    public string Text { get; set; }
    public string Font { get; set; }
    public string Texture { get; set; }
    public Vec2 Size { get; set; }
    public Color FontColor { get; set; }
    public Action<TexturedButton> Command { get; set; }

    private ButtonState _state;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="texture">Nom de la texture</param>
    /// <param name="size">Taille</param>
    /// <param name="fontColor">Couleur du texte (Color.BLACK)</param>
    public TexturedButton(Vec2? position = null, string text = "", string font = "", string texture = "",
        Vec2? size = null, Color fontColor = null) : base(position)
    {
        Text = text;
        Font = font;
        Texture = texture;
        Size = size ?? Vec2.Zero;
        FontColor = fontColor ?? Color.Black;
        _state = ButtonState.Idle;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Size == Vec2.Zero)
        {
            var temp = Scene.Window.TextureManager.GetTexture(Texture);
            Size = new Vec2(temp.Width, temp.Height);
        }

        if (!Active)
            return;

        if (InputManager.MouseInRectangle(new Rect(Position - Size / 2, Size)))
        {
            if (InputManager.IsMouseButtonPressed(MouseButton.Left) && Command != null)
                Command(this);

            _state = InputManager.IsMouseButtonDown(MouseButton.Left) ? ButtonState.Click : ButtonState.Hovered;
        }
        else
            _state = ButtonState.Idle;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Size == Vec2.Zero)
        {
            var temp = Scene.Window.TextureManager.GetTexture(Texture);
            Size = new Vec2(temp.Width, temp.Height);
        }

        if (!Displayed || Scene == null)
            return;

        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;
        var blankTexture = Scene.Window.TextureManager.GetTexture("blank");

        if (_state != ButtonState.Click && Active && _state == ButtonState.Hovered)
        {
            var size = Size + 4;
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - size / 2, size), Color.White,
                LayerDepth);
        }

        var textureSize = Size - 4;
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), Color.Black, LayerDepth + 0.00001f);
        Renderer.RenderTexture(Scene.Window, Scene.Window.TextureManager.GetTexture(Texture), new Rect(realPosition - textureSize / 2, textureSize), Color.White, LayerDepth + 0.00002f);

        var spriteFont = Scene.Window.FontManager.GetFont(Font);
        Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition, FontColor, 0, spriteFont.MeasureString(Text) / 2, Vec2.One, SpriteEffects.None, LayerDepth + 0.00003f);

        if(_state == ButtonState.Click || !Active)
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), new Color(0, 0, 0, 128), LayerDepth + 0.00004f);
    }
}
