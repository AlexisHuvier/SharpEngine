using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Entrée de texte
/// </summary>
public class LineEdit : Widget
{
    public string Text { get; set; }
    public string Font { get; set; }
    public Vec2 Size { get; set; }
    public bool Focused { get; set; }
    
    private float _timer;
    private bool _cursor;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="size">Taille (Vec2(300, 50))</param>
    public LineEdit(Vec2? position = null, string text = "", string font = "", Vec2? size = null) : base(position)
    {
        Text = text;
        Font = font;
        Size = size ?? new Vec2(300, 50);
        Focused = false;
        
        _timer = 500;
        _cursor = false;
    }

    public override void TextInput(object sender, Key key, char character)
    {
        base.TextInput(sender, key, character);

        if (!Focused)
            return;

        if (key == Key.Back)
        {
            if (Text.Length >= 1)
                Text = Text[..^1];
        }
        else if ((char.IsSymbol(character) || char.IsWhiteSpace(character) || char.IsLetterOrDigit(character) ||
                  char.IsPunctuation(character)) && Font.Length > 1 &&
                 Scene.Window.FontManager.GetFont(Font).MeasureString(Text + character).X < Size.X - 8)
            Text += character;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!Active)
        {
            _cursor = false;
            return;
        }

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
        {
            Focused = InputManager.MouseInRectangle(Position - Size / 2, Size);
            if (!Focused && _cursor)
                _cursor = false;
        }

        if (!Focused) return;
        
        if (_timer <= 0)
        {
            _cursor = !_cursor;
            _timer = 500;
        }

        _timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Scene == null || !Displayed)
            return;

        var realPosition = Parent != null ? Position + Parent.GetRealPosition() : Position;
        var blankTexture = Scene.Window.TextureManager.GetTexture("blank");
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - Size / 2, Size), Color.Black);
        Renderer.RenderTexture(Scene.Window, blankTexture, new Rect(realPosition - (Size - new Vec2(4)) / 2, Size - new Vec2(4)), Color.White);
        
        if (Font.Length < 1) return;
        
        var spriteFont = Scene.Window.FontManager.GetFont(Font);
        if (_cursor)
            Renderer.RenderText(Scene.Window, spriteFont, Text + "I", realPosition - Size / 2 + new Vec2(4, Size.Y / 2 - spriteFont.MeasureString(Text + "I").Y / 2), Color.Black);
        else
            Renderer.RenderText(Scene.Window, spriteFont, Text, realPosition - Size / 2 + new Vec2(4, Size.Y / 2 - spriteFont.MeasureString(Text).Y / 2), Color.Black);
    }
}
