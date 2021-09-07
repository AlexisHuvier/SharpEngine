using System.Collections.Generic;
using SharpEngine.Components;

namespace SharpEngine
{
    public class Entity
    {
        protected Scene scene;
        protected List<Component> components;

        public Entity()
        {
            components = new List<Component>();
            scene = null;
        }

        public void AddComponent<T>() where T: Component, new()
        {
            T comp = new T();
            comp.SetEntity(this);
            components.Add(comp);
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
