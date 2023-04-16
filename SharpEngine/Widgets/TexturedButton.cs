using System;
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

    public string Text;
    public string Font;
    public string Texture;
    public Vec2 Size;
    public Color FontColor;
    public Action<TexturedButton> Command;

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
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - (Size + new Vec2(4)) / 2, (Size + new Vec2(4))), Color.White);

        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), Color.Black);
        Renderer.RenderTexture(Scene.Window, Scene.Window.TextureManager.GetTexture(Texture), new Rect(realPosition - (Size - new Vec2(4)) / 2, (Size - new Vec2(4))), Color.White);

        var spriteFont = Scene.Window.FontManager.GetFont(Font);
        Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition, FontColor, 0, spriteFont.MeasureString(Text) / 2, Vec2.One, SpriteEffects.None, 1);

        if(_state == ButtonState.Click || !Active)
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), new Color(0, 0, 0, 128));
    }
}
