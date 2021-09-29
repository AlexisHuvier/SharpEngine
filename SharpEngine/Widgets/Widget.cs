namespace SharpEngine.Widgets
{
    public class Widget
    {
        internal Scene scene;
        public Vec2 position;
        public bool displayed;
        public bool active;

        public Widget(params object[] parameters)
        {
            position = new Vec2(0);
            displayed = true;
            active = true;

            if (parameters.Length >= 1 && parameters[0] is Vec2 pos)
                position = pos;
        }
        public virtual void SetScene(Scene scene)
        {
            this.scene = scene;
        }

        public virtual void Initialize()
        {}

        public virtual void LoadContent()
        {}

        public virtual void UnloadContent()
        {}

        public virtual void Update(GameTime gameTime)
        {}

        public virtual void Draw(GameTime gameTime)
        {}
    }
}
