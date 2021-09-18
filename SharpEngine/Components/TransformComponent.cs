namespace SharpEngine.Components
{
    public class TransformComponent: Component
    {
        public Vec2 position;
        public Vec2 scale;
        public int rotation;

        public TransformComponent(params object[] parameters): base(parameters)
        {
            this.position = new Vec2(0);
            this.scale = new Vec2(1);
            this.rotation = 0;

            if (parameters.Length >= 1 && parameters[0] is Vec2 pos)
                this.position = pos;
            if (parameters.Length >= 2 && parameters[1] is Vec2 sca)
                this.scale = sca;
            if (parameters.Length >= 3 && parameters[2] is int rot)
                this.rotation = rot;
        }

        public override string ToString()
        {
            return $"TransformComponent(pos={position}, scale={scale}, rotation={rotation})";
        }
    }
}
