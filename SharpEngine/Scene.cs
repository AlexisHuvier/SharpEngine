using System.Collections.Generic;
using System.Linq;
using SharpEngine.Component;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using tainicom.Aether.Physics2D.Dynamics;

namespace SharpEngine;

/// <summary>
/// Class which represents a Scene
/// </summary>
public class Scene
{
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
    public List<Widget.Widget> Widgets { get; } = new();

    /// <summary>
    /// All Entities of Scene
    /// </summary>
    public List<Entity.Entity> Entities { get; } = new();

    internal readonly World World;
    private float _worldStepTimer;
    private const float WorldStep = 1 / 60f;

    /// <summary>
    /// Create Scene
    /// </summary>
    /// <param name="gravity">Gravity (Vec2(0, 200))</param>
    public Scene(Vec2? gravity = null)
    {
        var finalGrav = gravity ?? new Vec2(0, 25);
        World = new World(finalGrav)
        {
            ContactManager =
            {
                VelocityConstraintsMultithreadThreshold = 256,
                PositionConstraintsMultithreadThreshold = 256,
                CollideMultithreadThreshold = 256
            }
        };
    }

    /// <summary>
    /// Add Widget to Scene
    /// </summary>
    /// <param name="widget">Widget which be added</param>
    /// <typeparam name="T">Type of Widget</typeparam>
    /// <returns>Widget</returns>
    public T AddWidget<T>(T widget) where T : Widget.Widget
    {
        widget.Scene = this;
        Widgets.Add(widget);
        return widget;
    }

    /// <summary>
    /// Get All Widgets of one Type
    /// </summary>
    /// <typeparam name="T">Type of Widgets</typeparam>
    /// <returns>Widgets</returns>
    public List<T> GetWidgetsAs<T>() where T : Widget.Widget =>
        Widgets.OfType<T>().ToList();

    /// <summary>
    /// Remove Widget
    /// </summary>
    /// <param name="widget">Widget which be removed</param>
    public void RemoveWidget(Widget.Widget widget)
    {
        widget.Scene = null;
        Widgets.Remove(widget);
    }

    /// <summary>
    /// Remove All Widgets
    /// </summary>
    public void RemoveAllWidgets()
    {
        foreach (var widget in Widgets)
            widget.Scene = null;
        Widgets.Clear();
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
        Entities.Add(entity);
        return entity;
    }

    /// <summary>
    /// Remove Entity From Scene
    /// </summary>
    /// <param name="entity">Entity to be removed</param>
    public void RemoveEntity(Entity.Entity entity)
    {
        if(entity.GetComponentAs<PhysicsComponent>() is { } physics)
            physics.RemoveBody();
        entity.Scene = null;
        Entities.Remove(entity);
    }

    /// <summary>
    /// Remove all Entities
    /// </summary>
    public void RemoveAllEntities()
    {
        foreach (var entity in Entities)
            entity.Scene = null;
        Entities.Clear();
    }

    /// <summary>
    /// Load Scene
    /// </summary>
    public virtual void Load()
    {
        foreach (var entity in Entities)
            entity.Load();
        foreach (var widget in Widgets)
            widget.Load();
    }

    /// <summary>
    /// Unload Scene
    /// </summary>
    public virtual void Unload()
    {
        foreach (var entity in Entities)
            entity.Unload();
        foreach (var widget in Widgets)
            widget.Unload();
    }

    /// <summary>
    /// Update Scene
    /// </summary>
    /// <param name="delta">Time since last update</param>
    public virtual void Update(float delta)
    {
        if (!Paused)
        {
            _worldStepTimer += delta;

            while (_worldStepTimer >= WorldStep)
            {
                World.Step(WorldStep);
                _worldStepTimer -= WorldStep;
            }
            World.ClearForces();
        }

        for (var i = Entities.Count - 1; i > -1; i--)
            if(Entities[i].PauseState is PauseState.Enabled ||
               !Paused && Entities[i].PauseState is PauseState.Normal ||
               Paused && Entities[i].PauseState is PauseState.WhenPaused)
                Entities[i].Update(delta);
        
        for(var i = Widgets.Count - 1; i > -1; i--)
            if(Widgets[i].PauseState is PauseState.Enabled ||
               !Paused && Widgets[i].PauseState is PauseState.Normal ||
               Paused && Widgets[i].PauseState is PauseState.WhenPaused)
                Widgets[i].Update(delta);
    }

    /// <summary>
    /// Draw all Entities Scene
    /// </summary>
    public virtual void DrawEntities()
    {
        foreach (var ent in Entities)
            ent.Draw();
    }

    /// <summary>
    /// Draw all Widgets Scene
    /// </summary>
    public virtual void DrawWidgets()
    {
        foreach (var widget in Widgets)
            widget.Draw();
    }

    /// <summary>
    /// Function call when Scene is opened
    /// </summary>
    public virtual void OpenScene() { }

    /// <summary>
    /// Function call when Scene is closed
    /// </summary>
    public virtual void CloseScene() { }
}