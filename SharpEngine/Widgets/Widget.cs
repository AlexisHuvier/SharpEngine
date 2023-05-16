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
    public Vec2 Position { get; set; }
    public bool Displayed { get; set; }
    public bool Active { get; set; }
    public Widget Parent { get; set; }
    public int ZLayer
    {
        get => (int)(LayerDepth * 4096);
        set
        {
            LayerDepth = value / 4096f;
            foreach (var child in GetChildren().Where(child => child.ZLayer < value))
                child.ZLayer = value;
        }
    }

    protected Scene Scene { get; private set; }
    protected float LayerDepth;
    
    private readonly List<Widget> _children;


    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="zLayer">Z Layer</param>
    protected Widget(Vec2? position = null, int zLayer = 1)
    {
        _children = new List<Widget>();
        Position = position ?? new Vec2(0);
        Displayed = true;
        Active = true;
        Parent = null;
        ZLayer = zLayer;
    }

    public Vec2 GetRealPosition() => Parent != null ? Position + Parent.GetRealPosition() : Position;

    public List<Widget> GetChildren() => _children;

    public List<T> GetChildren<T>() where T : Widget =>
        _children.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();

    public List<Widget> GetDisplayedChildren()
    {
        var children = new List<Widget>();
        foreach (var child in _children)
        {
            if(child.Displayed)
                children.Add(child);
        }

        return children;
    }

    public T AddChild<T>(T widget) where T : Widget
    {
        if (Scene != null)
            widget.SetScene(Scene);
        widget.Parent = this;
        if (widget.ZLayer < ZLayer)
            widget.ZLayer = ZLayer;
        _children.Add(widget);
        return widget;
    }

    public void RemoveChild(Widget widget)
    {
        widget.SetScene(null);
        _children.Remove(widget);
    }

    public virtual void SetScene(Scene scene)
    {
        Scene = scene;
        foreach (var child in _children)
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
        foreach (var widget in _children)
            widget.Initialize();
    }

    public virtual void LoadContent()
    {
        foreach (var widget in _children)
            widget.LoadContent();
    }

    public virtual void UnloadContent()
    {
        foreach (var widget in _children)
            widget.UnloadContent();
    }

    public virtual void TextInput(object sender, Key key, char character)
    {
        foreach (var widget in _children)
            widget.TextInput(sender, key, character);
    }

    public virtual void Update(GameTime gameTime)
    {
        if (!Displayed)
            return;
        
        for(var i = _children.Count - 1; i > -1; i--)
            _children[i].Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime)
    {
    }
}
