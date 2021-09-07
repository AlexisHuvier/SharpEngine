namespace SharpEngine.Components
{
    public class Component
    {
        protected Entity entity;

        public Component()
        {
            entity = null;
        }

        public virtual void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        public virtual void Initialize() {}
        public virtual void LoadContent() {}
        public virtual void UnloadContent() {}
        public virtual void Update(GameTime gameTime) {}
        public virtual void Draw(GameTime gameTime) {}
    }
}
