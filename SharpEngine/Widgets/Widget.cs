using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

/// <summary>
/// Widget basique
/// </summary>
public class Widget
{
    internal Scene Scene;
    public Vec2 Position;
    public bool Displayed;
    public bool Active;
    internal Widget Parent;
    private readonly List<Widget> _childs;


    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    protected Widget(Vec2 position = null)
    {
        _childs = new List<Widget>();
        Position = position ?? new Vec2(0);
        Displayed = true;
        Active = true;
        Parent = null;
    }

    public void SetParent(Widget widget) => Parent = widget;
    public Widget GetParent() => Parent;
    public Vec2 GetRealPosition() => Parent != null ? Position + Parent.Position : Position;

    public List<Widget> GetChilds() => _childs;

    public T AddChild<T>(T widget) where T : Widget
    {
        if (Scene != null)
            widget.SetScene(Scene);
        widget.SetParent(this);
        _childs.Add(widget);
        return widget;
    }

    public List<T> GetChilds<T>() where T : Widget
    {
        return _childs.FindAll((w) => w.GetType() == typeof(T)).Cast<T>().ToList();
    }

    public void RemoveChild(Widget widget)
    {
        widget.SetScene(null);
        _childs.Remove(widget);
    }

    public virtual void SetScene(Scene scene)
    {
        Scene = scene;
        foreach (var child in _childs)
            child.SetScene(scene);
    }

    public Scene GetScene() => Scene;

    public SpriteBatch GetSpriteBatch()
    {
        return Scene?.Window?.InternalGame.SpriteBatch;
    }

    public Window GetWindow()
    {
        return Scene?.Window;
    }

    public virtual void Initialize()
    {
        foreach (var widget in _childs)
            widget.Initialize();
    }

    public virtual void LoadContent()
    {
        foreach (var widget in _childs)
            widget.LoadContent();
    }

    public virtual void UnloadContent()
    {
        foreach (var widget in _childs)
            widget.UnloadContent();
    }

    public virtual void TextInput(object sender, Key key, char character)
    {
        foreach (var widget in _childs)
            widget.TextInput(sender, key, character);
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (var widget in _childs)
            widget.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime)
    {
        foreach (var widget in _childs)
            widget.Draw(gameTime);
    }
}
