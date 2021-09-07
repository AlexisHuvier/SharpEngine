namespace SharpEngine
{
    public class Scene
    {
        internal Window window;

        public int id;

        public Scene()
        {
            this.window = null;
            id = -1;
        }

        public virtual void SetWindow(Window window)
        {
            this.window = window;
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
