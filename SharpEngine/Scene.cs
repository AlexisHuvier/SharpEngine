using System.Collections.Generic;

namespace SharpEngine
{
    public class Scene
    {
        internal Window window;
        protected List<Entity> entities;

        public Scene()
        {
            this.window = null;
            entities = new List<Entity>();
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

        public virtual void Initialize()
        {
            foreach (Entity ent in entities)
                ent.Initialize();
        }

        public virtual void LoadContent()
        {
            foreach (Entity ent in entities)
                ent.LoadContent();
        }

        public virtual void UnloadContent()
        {
            foreach (Entity ent in entities)
                ent.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Entity ent in entities)
                ent.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (Entity ent in entities)
                ent.Draw(gameTime);
        }
    }
}
