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
    public Button(Vec2? position = null, string text = "", string font = "", Vec2? size = null, Color? fontColor = null, Color? backgroundColor = null): base(position)
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
        
        if (InputManager.MouseInRectangle(new Rect(GetRealPosition() - Size / 2, Size)))
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

        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;
        var blankTexture = Scene.Window.TextureManager.GetTexture("blank");
        var whiteSize = Size - 4;

        if (_state != ButtonState.Click && Active && _state == ButtonState.Hovered)
        {
            var hoverSize = Size + 4;
            var hoverPosition = new Vec2(realPosition.X - hoverSize.X / 2, realPosition.Y - hoverSize.Y / 2);
            Renderer.RenderTexture(Scene.Window, blankTexture,
                new Rect(hoverPosition, hoverSize), Color.White, LayerDepth);
        }

        Renderer.RenderTexture(Scene.Window, blankTexture,
            new Rect(realPosition.X - Size.X / 2, realPosition.Y - Size.Y / 2, Size), Color.Black,
            LayerDepth + 0.00001f);
        Renderer.RenderTexture(Scene.Window, blankTexture,
            new Rect(realPosition.X - whiteSize.X / 2, realPosition.Y - whiteSize.Y / 2, whiteSize),
            BackgroundColor, LayerDepth + 0.00002f);

        var spriteFont = Scene.Window.FontManager.GetFont(Font);
        Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition, FontColor, 0,
            spriteFont.MeasureString(Text) / 2, Vec2.One, SpriteEffects.None, LayerDepth + 0.00003f);

        if (_state == ButtonState.Click || !Active)
            Renderer.RenderTexture(Scene.Window, blankTexture,
                new Rect(realPosition.X - Size.X / 2, realPosition.Y - Size.Y / 2, Size), 
                new Color(0, 0, 0, 128), LayerDepth + 0.00004f);
    }
}
