using System.Collections.Generic;
using System;
using SharpEngine.Widgets;

namespace SharpEngine
{
    public class Scene
    {
        internal Window window;
        protected List<Entity> entities;
        protected List<Widget> widgets;

        public Scene()
        {
            window = null;
            entities = new List<Entity>();
            widgets = new List<Widget>();
        }

        public List<Widget> GetWidgets()
        {
            return widgets;
        }

        public T AddWidget<T>(params object[] parameters) where T : Widget
        {
            T wid = Activator.CreateInstance(typeof(T), parameters) as T;
            wid.SetScene(this);
            widgets.Add(wid);
            return wid;
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
            foreach (Entity ent in entities)
                ent.Update(gameTime);
            foreach (Widget widget in widgets)
                widget.Update(gameTime);
        }

        public virtual void TextInput(object sender, Inputs.Key key, char Character)
        {
            foreach (Widget widget in widgets)
                widget.TextInput(sender, key, Character);
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (Entity ent in entities)
                ent.Draw(gameTime);
            foreach (Widget widget in widgets)
                widget.Draw(gameTime);
        }
    }
}
