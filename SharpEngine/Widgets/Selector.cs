using System.Collections.Generic;
using System.Linq;
using SharpEngine.Utils;

namespace SharpEngine.Widgets;

/// <summary>
/// Sélecteur
/// </summary>
public class Selector : Widget
{
    private readonly List<string> _texts;
    private Button _leftButton;
    private Button _rightButton;
    private Label _text;
    private string _font;
    private int _selected;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="texts">Liste des possibilités</param>
    public Selector(Vec2 position = null, string font = "", List<string> texts = null) : base(position)
    {
        _font = font;
        _texts = texts ?? new List<string>();
        _selected = 0;
    }

    public string GetValue() => _texts[_selected];

    public override void LoadContent()
    {
        base.LoadContent();
        
        var maxsize = _texts.Select(text => Scene.Window.FontManager.GetFont(_font).MeasureString(text).X).Prepend(0).Max();

        _leftButton = new Button(Position + new Vec2(20) / 2 - new Vec2(maxsize / 2 + 25, 10), "<", _font,
            new Vec2(20))
        {
            Command = _ =>
            {
                if (_selected == 0)
                    _selected = _texts.Count - 1;
                else
                    _selected--;
                _text.Text = _texts[_selected];
            }
        };
        _rightButton = new Button(Position + new Vec2(20) / 2 + new Vec2(maxsize / 2 + 10, -10), ">", _font,
            new Vec2(20))
        {
            Command = _ =>
            {
                if (_selected == _texts.Count - 1)
                    _selected = 0;
                else
                    _selected++;
                _text.Text = _texts[_selected];
            }
        };
        _text = new Label(Position, _texts[_selected], _font);

        _leftButton.SetScene(Scene);
        _rightButton.SetScene(Scene);
        _text.SetScene(Scene);

        _leftButton.SetParent(Parent);
        _rightButton.SetParent(Parent);
        _text.SetParent(Parent);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _leftButton.Update(gameTime);
        _rightButton.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Scene == null)
            return;

        _leftButton.Draw(gameTime);
        _rightButton.Draw(gameTime);
        _text.Draw(gameTime);
    }
}
