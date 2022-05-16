using System.Collections.Generic;
using SharpEngine.Components;
using System;

namespace SharpEngine
{
    /// <summary>
    /// Entité basique
    /// </summary>
    public class Entity
    {
        internal Scene scene;
        protected List<Component> components;

        public Entity()
        {
            components = new List<Component>();
            scene = null;
        }

        public List<Component> GetComponents()
        {
            return components;
        }

        public T AddComponent<T>(T comp) where T: Component
        {
            comp.SetEntity(this);
            components.Add(comp);
            return comp;
        }

        public T GetComponent<T>() where T: Component
        {
            foreach(Component comp in components)
            {
                if (comp.GetType() == typeof(T))
                    return (T) comp;
            }
            return null;
        }

        public void RemoveComponent(Component component)
        {
            component.SetEntity(null);
            components.Remove(component);
        }

        public virtual void SetScene(Scene scene)
        {
            this.scene = scene;
        }

        public Scene GetScene()
        {
            return scene;
        }

        public virtual void Initialize() 
        {
            foreach (Component comp in components)
                comp.Initialize();
        }

        public virtual void LoadContent()
        {
            foreach (Component comp in components)
                comp.LoadContent();
        }

        public virtual void UnloadContent()
        {
            foreach (Component comp in components)
                comp.UnloadContent();
        }

        public virtual void TextInput(object sender, Inputs.Key key, char Character)
        {
            foreach (Component comp in components)
                comp.TextInput(sender, key, Character);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Component comp in components)
                comp.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (Component comp in components)
                comp.Draw(gameTime);
        }
    }
}
