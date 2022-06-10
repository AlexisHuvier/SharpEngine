using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SharpEngine.Widgets
{
    /// <summary>
    /// Widget basique
    /// </summary>
    public class Widget
    {
        internal Scene scene;
        public Vec2 position;
        public bool displayed;
        public bool active;
        internal Widget parent;
        internal List<Widget> childs;


        /// <summary>
        /// Initialise le Widget.
        /// </summary>
        /// <param name="position">Position (Vec2(0))</param>
        public Widget(Vec2 position = null)
        {
            childs = new List<Widget>();
            this.position = position ?? new Vec2(0);
            displayed = true;
            active = true;
            parent = null;
        }

        public void SetParent(Widget widget) => parent = widget;
        public Widget GetParent() => parent;

        public List<Widget> GetChilds() => childs;

        public T AddChild<T>(T widget) where T : Widget
        {
            if (scene != null)
                widget.SetScene(scene);
            widget.SetParent(this);
            childs.Add(widget);
            return widget;
        }

        public List<T> GetChilds<T>() where T : Widget
        {
            List<T> temp = new List<T>();
            foreach (Widget widget in childs.FindAll((Widget w) => w.GetType() == typeof(T)))
                temp.Add((T)widget);
            return temp;
        }

        public void RemoveChild(Widget widget)
        {
            widget.SetScene(null);
            childs.Remove(widget);
        }

        public virtual void SetScene(Scene scene)
        {
            this.scene = scene;
            foreach (Widget child in childs)
                child.SetScene(scene);
        }

        public Scene GetScene() => scene;

        public SpriteBatch GetSpriteBatch()
        {
            if (scene != null && scene.window != null)
                return scene.window.internalGame.spriteBatch;
            return null;
        }

        public Window GetWindow()
        {
            if (scene != null)
                return scene.window;
            return null;
        }

        public virtual void Initialize()
        {
            foreach (Widget widget in childs)
                widget.Initialize();
        }

        public virtual void LoadContent()
        {
            foreach (Widget widget in childs)
                widget.LoadContent();
        }

        public virtual void UnloadContent()
        {
            foreach (Widget widget in childs)
                widget.UnloadContent();
        }

        public virtual void TextInput(object sender, Inputs.Key key, char Character)
        {
            foreach (Widget widget in childs)
                widget.TextInput(sender, key, Character);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Widget widget in childs)
                widget.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (Widget widget in childs)
                widget.Draw(gameTime);
        }
    }
}
