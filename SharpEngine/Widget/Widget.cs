using System.Collections.Generic;
using System.Linq;
using SharpEngine.Math;
using SharpEngine.Utils;

namespace SharpEngine.Widget;

/// <summary>
/// Abstract Class which represents Widget
/// </summary>
public abstract class Widget
{
    /// <summary>
    /// Position of Widget
    /// </summary>
    public Vec2 Position;
    
    /// <summary>
    /// If Widget is Display
    /// </summary>
    public bool Displayed = true;
    
    /// <summary>
    /// If Widget is Active
    /// </summary>
    public bool Active = true;
    
    /// <summary>
    /// Parent of Widget (can be null)
    /// </summary>
    public Widget? Parent;
    
    /// <summary>
    /// How Widget must be updated when paused
    /// </summary>
    public PauseState PauseState = PauseState.Normal;

    
    /// <summary>
    /// Get Real Position (Position + Parent RealPostion if widget has Parent)
    /// </summary>
    public Vec2 RealPosition => Parent != null ? Position + Parent.RealPosition : Position;
    
    /// <summary>
    /// Get All Children of Widget
    /// </summary>
    public List<Widget> Children => _children;

    
    /// <summary>
    /// Scene of Widget
    /// </summary>
    public Scene? Scene
    {
        get => _scene;
        set
        {
            _scene = value;
            foreach (var child in _children)
                child.Scene = value;
        }
    }

    private readonly List<Widget> _children = new();
    private Scene? _scene;

    /// <summary>
    /// Widget
    /// </summary>
    /// <param name="position">Position Widget</param>
    protected Widget(Vec2 position)
    {
        Position = position;
    }
    
    /// <summary>
    /// Get All Children of one Type
    /// </summary>
    /// <typeparam name="T">Type of Children</typeparam>
    /// <returns>Children of type T</returns>
    public List<T> GetChildrenAs<T>() where T : Widget =>
        _children.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();

    /// <summary>
    /// Get Scene as T
    /// </summary>
    /// <typeparam name="T">Scene Type</typeparam>
    /// <returns>Scene casted as T</returns>
    public T? GetSceneAs<T>() where T : Scene => (T?)Scene;

    /// <summary>
    /// Add Child and return it
    /// </summary>
    /// <param name="widget">Widget which be added</param>
    /// <typeparam name="T">Type of Widget</typeparam>
    /// <returns>Child</returns>
    public T AddChild<T>(T widget) where T : Widget
    {
        if (_scene != null)
            widget.Scene = _scene;
        widget.Parent = this;
        _children.Add(widget);
        return widget;
    }

    /// <summary>
    /// Remove Child
    /// </summary>
    /// <param name="widget">Child will be removed</param>
    public void RemoveChild(Widget widget)
    {
        widget.Scene = null;
        _children.Remove(widget);
    }

    /// <summary>
    /// Remove All Children
    /// </summary>
    public void RemoveAllChildren()
    {
        foreach (var child in _children)
            child.Scene = null;
        _children.Clear();
    }

    /// <summary>
    /// Load Widget
    /// </summary>
    public virtual void Load()
    {
        foreach (var child in _children)
            child.Load();
    }

    /// <summary>
    /// Unload Widget
    /// </summary>
    public virtual void Unload()
    {
        foreach (var child in _children)
            child.Unload();
    }

    /// <summary>
    /// Update Widget
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    public virtual void Update(float delta)
    {
        foreach (var child in _children)
            child.Update(delta);
    }

    /// <summary>
    /// Draw Widget
    /// </summary>
    public virtual void Draw()
    {
        foreach (var child in _children)
            child.Draw();
    }
}