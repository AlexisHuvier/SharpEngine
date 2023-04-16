using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
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
    public int ZLayer
    {
        get => _zLayer;
        set
        {
            _zLayer = value;
            foreach (var child in GetChilds().Where(child => child.ZLayer < value))
                child.ZLayer = value;
        }
    }
    
    internal Widget Parent;
    private readonly List<Widget> _childs;
    private int _zLayer;


    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="zLayer">Z Layer</param>
    protected Widget(Vec2? position = null, int zLayer = 1)
    {
        _childs = new List<Widget>();
        Position = position ?? new Vec2(0);
        Displayed = true;
        Active = true;
        Parent = null;
        ZLayer = zLayer;
    }

    public void SetParent(Widget widget) => Parent = widget;
    public Widget GetParent() => Parent;
    public Vec2 GetRealPosition() => Parent != null ? Position + Parent.GetRealPosition() : Position;

    public List<Widget> GetChilds() => _childs;

    public List<Widget> GetDisplayedChilds()
    {
        var childs = new List<Widget>();
        foreach (var child in _childs)
        {
            if(child.Displayed)
                childs.Add(child);
        }

        return childs;
    }

    public T AddChild<T>(T widget) where T : Widget
    {
        if (Scene != null)
            widget.SetScene(Scene);
        widget.SetParent(this);
        if (widget.ZLayer < ZLayer)
            widget.ZLayer = ZLayer;
        _childs.Add(widget);
        return widget;
    }

    public List<T> GetChilds<T>() where T : Widget
    {
        return _childs.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();
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
        if (!Displayed)
            return;
        
        for(var i = _childs.Count - 1; i > -1; i--)
            _childs[i].Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime)
    {
    }
}
