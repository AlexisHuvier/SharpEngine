using System.Collections.Generic;
using System.Linq;
using SharpEngine.Component;
using SharpEngine.Utils;

namespace SharpEngine.Entity;

/// <summary>
/// Class which represents Entity
/// </summary>
public class Entity
{
    /// <summary>
    /// How Entity must be updated when paused
    /// </summary>
    public PauseState PauseState = PauseState.Normal;

    /// <summary>
    /// Tag of Entity
    /// </summary>
    public string Tag = "";

    /// <summary>
    /// Scene of Entity
    /// </summary>
    public Scene? Scene = null;
    
    /// <summary>
    /// Get All Components of Entity
    /// </summary>
    public List<Component.Component> Components => _components;
    
    private readonly List<Component.Component> _components = new();

    /// <summary>
    /// Get All Components of one Type
    /// </summary>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Components of type T</returns>
    public List<T> GetComponentsAs<T>() where T : Component.Component =>
        _components.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();
    
    /// <summary>
    /// Get Component of one Type
    /// </summary>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Component of type T</returns>
    public T? GetComponentAs<T>() where T: Component.Component =>
        (T?)_components.Find(w => w.GetType() == typeof(T));

    /// <summary>
    /// Get Scene as T
    /// </summary>
    /// <typeparam name="T">Scene Type</typeparam>
    /// <returns>Scene casted as T</returns>
    public T? GetSceneAs<T>() where T : Scene => (T?)Scene;

    /// <summary>
    /// Add Component and return it
    /// </summary>
    /// <param name="component">Component which be added</param>
    /// <typeparam name="T">Type of Component</typeparam>
    /// <returns>Component</returns>
    public T AddComponent<T>(T component) where T : Component.Component
    {
        _components.Add(component);
        component.Entity = this;
        return component;
    }

    /// <summary>
    /// Remove Component
    /// </summary>
    /// <param name="component">Component will be removed</param>
    public void RemoveComponent(Component.Component component)
    {
        component.Entity = null;
        _components.Remove(component);
    }

    /// <summary>
    /// Load Entity
    /// </summary>
    public virtual void Load()
    {
        foreach (var component in _components)
            component.Load();
    }

    /// <summary>
    /// Unload Entity
    /// </summary>
    public virtual void Unload()
    {
        foreach (var component in _components)
            component.Unload();
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    public virtual void Update(float delta)
    {
        foreach (var component in _components)
            component.Update(delta);
    }

    /// <summary>
    /// Draw Entity
    /// </summary>
    public virtual void Draw()
    {
        foreach (var component in _components)
            component.Draw();
    }
}