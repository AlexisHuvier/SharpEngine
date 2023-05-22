using System.Collections.Generic;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Sélecteur
/// </summary>
public class Selector : Widget
{
    public string Value => _texts[_selected];

    private readonly List<string> _texts;
    private Label _text;
    private int _selected;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="sizeBetweenButtons">Taille entre les boutons</param>
    /// <param name="texts">Liste des possibilités</param>
    public Selector(Vec2? position = null, string font = "", int sizeBetweenButtons = 0, List<string> texts = null) : base(position)
    {
        _texts = texts ?? new List<string>();
        _selected = 0;

        var leftButton = AddChild(new Button(new Vec2(- 20 - sizeBetweenButtons / 2f, 0), "<", font,
            new Vec2(20)));
        leftButton.Command = _ =>
        {
            if (_selected == 0)
                _selected = _texts.Count - 1;
            else
                _selected--;
            _text.Text = _texts[_selected];
        };

        var rightButton = AddChild(new Button(new Vec2(20 + sizeBetweenButtons / 2f, 0), ">", font, new Vec2(20)));
        rightButton.Command = _ =>
        {
            if (_selected == _texts.Count - 1)
                _selected = 0;
            else
                _selected++;
            _text.Text = _texts[_selected];
        };

        _text = AddChild(new Label(Vec2.Zero, _texts[_selected], font));
    }
}
