using System.Collections.Generic;
using SharpEngine.Widgets;
using SharpEngine.Components;
using tainicom.Aether.Physics2D.Dynamics;

namespace SharpEngine
{
    /// <summary>
    /// Scène
    /// </summary>
    public class Scene
    {
        internal Window window;
        protected List<Entity> entities;
        protected List<Widget> widgets;
        internal World world;

        public Scene(int gravity = 200)
        {
            window = null;
            entities = new List<Entity>();
            widgets = new List<Widget>();

            world = new World(new tainicom.Aether.Physics2D.Common.Vector2(0, gravity));
            world.ContactManager.VelocityConstraintsMultithreadThreshold = 256;
            world.ContactManager.PositionConstraintsMultithreadThreshold = 256;
            world.ContactManager.CollideMultithreadThreshold = 256;
        }

        public List<Widget> GetWidgets()
        {
            return widgets;
        }

        public T AddWidget<T>(T widget) where T : Widget
        {
            widget.SetScene(this);
            widgets.Add(widget);
            return widget;
        }

        public List<T> GetWidgets<T>() where T : Widget
        {
            List<T> temp = new List<T>();
            foreach (Widget widget in widgets.FindAll((Widget w) => w.GetType() == typeof(T)))
                temp.Add((T) widget);
            return temp;
        }

        public void RemoveWidget(Widget widget)
        {
            widget.SetScene(null);
            widgets.Remove(widget);
        }

        public List<Entity> GetEntities()
        {
            return entities;
        }

        public virtual void AddEntity(Entity ent)
        {
            ent.SetScene(this);
            entities.Add(ent);
        }

        public virtual void RemoveEntity(Entity ent)
        {
            ent.SetScene(null);
            entities.Remove(ent);
        }

        public virtual void SetWindow(Window window)
        {
            this.window = window;
        }

        public Window GetWindow()
        {
            return window;
        }

        public virtual void Initialize()
        {
            foreach (Entity ent in entities)
                ent.Initialize();
            foreach (Widget widget in widgets)
                widget.Initialize();
        }

        public virtual void LoadContent()
        {
            foreach (Entity ent in entities)
                ent.LoadContent();
            foreach (Widget widget in widgets)
                widget.LoadContent();
        }

        public virtual void UnloadContent()
        {
            foreach (Entity ent in entities)
                ent.UnloadContent();
            foreach (Widget widget in widgets)
                widget.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            world.Step((float)gameTime.elapsedGameTime.TotalSeconds);
            for (int i = entities.Count - 1; i > -1; i--)
                entities[i].Update(gameTime);
            for (int i = widgets.Count - 1; i > -1; i--)
                widgets[i].Update(gameTime);
        }

        public virtual void TextInput(object sender, Inputs.Key key, char Character)
        {
            foreach (Entity e in entities)
                e.TextInput(sender, key, Character);
            foreach (Widget widget in widgets)
                widget.TextInput(sender, key, Character);
        }

        public List<Entity> GetEntitySortByZ()
        {
            List<Entity> temp = new List<Entity>(entities);
            temp.Sort((Entity a, Entity b) => {
                if (a.GetComponent<TransformComponent>() is TransformComponent atc)
                {
                    if (b.GetComponent<TransformComponent>() is TransformComponent btc)
                    {
                        return atc.zLayer - btc.zLayer;
                    }
                    else
                        return 1;
                }
                else if (b.GetComponent<TransformComponent>() is TransformComponent btc)
                    return -1;
                else
                    return 0;
            });
            return temp;
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (Entity ent in GetEntitySortByZ())
                ent.Draw(gameTime);
            foreach (Widget widget in widgets)
                widget.Draw(gameTime);
        }
    }
}
