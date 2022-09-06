using System;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Bouton
/// </summary>
public class Button: Widget
{
    private enum ButtonState
    {
        Idle,
        Click,
        Hovered
    }

    public string Text;
    public string Font;
    public Vec2 Size;
    public Color FontColor;
    public Color BackgroundColor;
    public Action<Button> Command;

    private ButtonState _state;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="size">Taille (Vec2(200, 40))</param>
    /// <param name="fontColor">Couleur du texte (Color.BLACK)</param>
    /// <param name="backgroundColor">Couleur du fond (Color.GRAY)</param>
    public Button(Vec2 position = null, string text = "", string font = "", Vec2 size = null, Color fontColor = null, Color backgroundColor = null): base(position)
    {
        Text = text;
        Font = font;
        Size = size ?? new Vec2(200, 40);
        FontColor = fontColor ?? Color.Black;
        BackgroundColor = backgroundColor ?? Color.Gray;
        _state = ButtonState.Idle;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

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

        if (!Displayed || Scene == null) return;

        var realPosition = Parent != null ? Position + Parent.Position : Position;
        var blankTexture = Scene.Window.TextureManager.GetTexture("blank");

        if (_state != ButtonState.Click && Active && _state == ButtonState.Hovered)
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - (Size + new Vec2(4)) / 2, (Size + new Vec2(4))), Color.White);
        
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), Color.Black);
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - (Size - new Vec2(4)) / 2, (Size - new Vec2(4))), BackgroundColor);

        var spriteFont = Scene.Window.FontManager.GetFont(Font);
        Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition, FontColor, 0, spriteFont.MeasureString(Text) / 2, Vec2.One, SpriteEffects.None, 1);

        if(_state == ButtonState.Click || !Active)
            Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), new Color(0, 0, 0, 128));
    }
}
