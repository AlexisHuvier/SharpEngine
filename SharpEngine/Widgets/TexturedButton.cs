using System;
using SharpEngine.Managers;
using SharpEngine.Utils;

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
    public TexturedButton(Vec2 position = null, string text = "", string font = "", string texture = "",
        Vec2 size = null, Color fontColor = null) : base(position)
    {
        Text = text;
        Font = font;
        Texture = texture;
        Size = size;
        FontColor = fontColor ?? Color.Black;
        _state = ButtonState.Idle;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Size == null)
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

        if (Size == null)
        {
            var temp = Scene.Window.TextureManager.GetTexture(Texture);
            Size = new Vec2(temp.Width, temp.Height);
        }

        if (!Displayed || Scene == null)
            return;

        var realPosition = Parent != null ? Position + Parent.Position : Position;

        if (_state != ButtonState.Click && Active && _state == ButtonState.Hovered)
            Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                new Rect(realPosition - (Size + new Vec2(4)) / 2, (Size + new Vec2(4))).ToMg(), Color.White.ToMg());

        Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
            new Rect(realPosition - Size / 2, Size).ToMg(), Color.Black.ToMg());
        Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture(Texture),
            new Rect(realPosition - (Size - new Vec2(4)) / 2, (Size - new Vec2(4))).ToMg(), Color.White.ToMg());

        var spriteFont = Scene.Window.FontManager.GetFont(Font);
        Scene.Window.InternalGame.SpriteBatch.DrawString(spriteFont, Text, realPosition.ToMg(), FontColor.ToMg(), 0,
            spriteFont.MeasureString(Text) / 2, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);

        if (_state == ButtonState.Click || !Active)
            Scene.Window.InternalGame.SpriteBatch.Draw(Scene.Window.TextureManager.GetTexture("blank"),
                new Rect(realPosition - Size / 2, Size).ToMg(), new Color(0, 0, 0, 128).ToMg());
    }
}
