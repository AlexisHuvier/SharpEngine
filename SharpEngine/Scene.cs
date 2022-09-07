using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SharpEngine.Widgets;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils.Control;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine;

/// <summary>
/// Scène
/// </summary>
public class Scene
{
    public Action<Scene> OpenScene;
    public Action<Scene> CloseScene;
    internal Window Window;
    protected readonly List<Entity> Entities;
    protected readonly List<Widget> Widgets;
    internal World World;
    private DebugView _debugView;

    public Scene(int gravity = 200)
    {
        Window = null;
        Entities = new List<Entity>();
        Widgets = new List<Widget>();

        World = new World(new Vector2(0, gravity))
        {
            ContactManager =
            {
                VelocityConstraintsMultithreadThreshold = 256,
                PositionConstraintsMultithreadThreshold = 256,
                CollideMultithreadThreshold = 256
            }
        };
        _debugView = new DebugView(World);
        _debugView.AppendFlags(DebugViewFlags.Shape);
        _debugView.AppendFlags(DebugViewFlags.CenterOfMass);
        _debugView.AppendFlags(DebugViewFlags.DebugPanel);
        _debugView.AppendFlags(DebugViewFlags.PerformanceGraph);
    }

    public List<Widget> GetWidgets() => Widgets;

    public T AddWidget<T>(T widget) where T : Widget
    {
        widget.SetScene(this);
        Widgets.Add(widget);
        return widget;
    }

    public List<T> GetWidgets<T>() where T : Widget
    {
        return Widgets.FindAll(w => w.GetType() == typeof(T)).Cast<T>().ToList();
    }

    public void RemoveWidget(Widget widget)
    {
        widget.SetScene(null);
        Widgets.Remove(widget);
    }

    public List<Entity> GetEntities() => Entities;

    public virtual Entity AddEntity(Entity ent)
    {
        ent.SetScene(this);
        Entities.Add(ent);
        return ent;
    }

    public virtual void RemoveEntity(Entity ent)
    {
        ent.SetScene(null);
        Entities.Remove(ent);
    }

    public virtual void SetWindow(Window window) => Window = window;
    
    public Window GetWindow() => Window;

    public virtual void Initialize()
    {
        foreach (var ent in Entities)
            ent.Initialize();
        foreach (var widget in Widgets)
            widget.Initialize();
    }

    public virtual void LoadContent()
    {
        _debugView.LoadContent(Window.InternalGame.GraphicsDevice, Window.InternalGame.Content);
        foreach (var ent in Entities)
            ent.LoadContent();
        foreach (var widget in Widgets)
            widget.LoadContent();
    }

    public virtual void UnloadContent()
    {
        foreach (var ent in Entities)
            ent.UnloadContent();
        foreach (var widget in Widgets)
            widget.UnloadContent();
    }

    public virtual void Update(GameTime gameTime)
    {
        World.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
        
        if(Window.Debug)
            _debugView.UpdatePerformanceGraph(World.UpdateTime);
        
        for (var i = Entities.Count - 1; i > -1; i--)
            Entities[i].Update(gameTime);
        for (var i = Widgets.Count - 1; i > -1; i--)
            Widgets[i].Update(gameTime);
    }

    public virtual void TextInput(object sender, Key key, char character)
    {
        foreach (var e in Entities)
            e.TextInput(sender, key, character);
        foreach (var widget in Widgets)
            widget.TextInput(sender, key, character);
    }

    public List<Entity> GetEntitySortByZ()
    {
        var temp = new List<Entity>(Entities);
        temp.Sort((a, b) => {
            if (a.GetComponent<TransformComponent>() is { } atc)
            {
                if (b.GetComponent<TransformComponent>() is { } btc)
                    return atc.ZLayer - btc.ZLayer;
                return 1;
            }
            if (b.GetComponent<TransformComponent>() is { })
                return -1;
            return 0;
        });
        return temp;
    }

    public virtual void Draw(GameTime gameTime)
    {
        foreach (var ent in GetEntitySortByZ())
            ent.Draw(gameTime);
        foreach (var widget in Widgets)
            widget.Draw(gameTime);
        
        if (Window.Debug)
        {
            var i = Matrix.CreateOrthographicOffCenter(0, Window.ScreenSize.X, Window.ScreenSize.Y, 0, -100, 10);
            var view = Matrix.CreateScale(1);
            _debugView.RenderDebugData(ref i, ref view);
        }
    }
}
