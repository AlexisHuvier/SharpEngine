using System;
using System.Collections.Generic;
using System.Linq;
using SharpEngine.Utils;

namespace SharpEngine;

/// <summary>
/// Class which represents a Scene
/// </summary>
public class Scene
{
    /// <summary>
    /// Function which will be launched when Scene is opened
    /// </summary>
    public Action<Scene>? OpenScene = null;
    
    /// <summary>
    /// Function which will be launched when Scene is closed
    /// </summary>
    public Action<Scene>? CloseScene = null;
    
    /// <summary>
    /// Define if Scene is paused
    /// </summary>
    public bool Paused = false;

    /// <summary>
    /// Window which have this scene
    /// </summary>
    public Window? Window;

    /// <summary>
    /// All Widgets of Scene
    /// </summary>
    public List<Widget.Widget> Widgets => _widgets;
    
    /// <summary>
    /// All Entities of Scene
    /// </summary>
    public List<Entity.Entity> Entities => _entities;

    private readonly List<Widget.Widget> _widgets = new();
    private readonly List<Entity.Entity> _entities = new();

    /// <summary>
    /// Add Widget to Scene
    /// </summary>
    /// <param name="widget">Widget which be added</param>
    /// <typeparam name="T">Type of Widget</typeparam>
    /// <returns>Widget</returns>
    public T AddWidget<T>(T widget) where T : Widget.Widget
    {
        widget.Scene = this;
        _widgets.Add(widget);
        return widget;
    }

    /// <summary>
    /// Get All Widgets of one Type
    /// </summary>
    /// <typeparam name="T">Type of Widgets</typeparam>
    /// <returns>Widgets</returns>
    public List<T> GetWidgetsAs<T>() where T : Widget.Widget =>
        _widgets.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();

    /// <summary>
    /// Remove Widget
    /// </summary>
    /// <param name="widget">Widget which be removed</param>
    public void RemoveWidget(Widget.Widget widget)
    {
        widget.Scene = null;
        _widgets.Remove(widget);
    }

    /// <summary>
    /// Remove All Widgets
    /// </summary>
    public void RemoveAllWidgets()
    {
        foreach (var widget in _widgets)
            widget.Scene = null;
        _widgets.Clear();
    }

    /// <summary>
    /// Add Entity To Scene
    /// </summary>
    /// <param name="entity">Entity to be added</param>
    /// <typeparam name="T">Type of Widgets</typeparam>
    /// <returns>Entity</returns>
    public T AddEntity<T>(T entity) where T : Entity.Entity
    {
        entity.Scene = this;
        _entities.Add(entity);
        return entity;
    }

    /// <summary>
    /// Remove Entity From Scene
    /// </summary>
    /// <param name="entity">Entity to be removed</param>
    public void RemoveEntity(Entity.Entity entity)
    {
        entity.Scene = null;
        _entities.Remove(entity);
    }

    /// <summary>
    /// Remove all Entities
    /// </summary>
    public void RemoveAllEntities()
    {
        foreach (var entity in _entities)
            entity.Scene = null;
        _entities.Clear();
    }

    /// <summary>
    /// Load Scene
    /// </summary>
    public virtual void Load()
    {
        foreach (var entity in _entities)
            entity.Load();
        foreach (var widget in _widgets)
            widget.Load();
    }

    /// <summary>
    /// Unload Scene
    /// </summary>
    public virtual void Unload()
    {
        foreach (var entity in _entities)
            entity.Unload();
        foreach (var widget in _widgets)
            widget.Unload();
    }

    /// <summary>
    /// Update Scene
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    public virtual void Update(float delta)
    {
        for (var i = _entities.Count - 1; i > -1; i--)
            if(_entities[i].PauseState is PauseState.Enabled ||
               !Paused && _entities[i].PauseState is PauseState.Normal ||
               Paused && _entities[i].PauseState is PauseState.WhenPaused)
                _entities[i].Update(delta);
        
        for(var i = _widgets.Count - 1; i > -1; i--)
            if(_widgets[i].PauseState is PauseState.Enabled ||
               !Paused && _widgets[i].PauseState is PauseState.Normal ||
               Paused && _widgets[i].PauseState is PauseState.WhenPaused)
                _widgets[i].Update(delta);
    }

    /// <summary>
    /// Draw Scene
    /// </summary>
    public virtual void Draw()
    {
        foreach (var ent in _entities)
            ent.Draw();
        foreach (var widget in _widgets)
            widget.Draw();
    }
}