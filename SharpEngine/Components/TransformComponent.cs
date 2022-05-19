namespace SharpEngine.Components
{
    /// <summary>
    /// Composant ajoutant la position, la rotation et l'échelle
    /// </summary>
    public class TransformComponent: Component
    {
        public Vec2 position;
        public Vec2 scale;
        public int rotation;
        public int zLayer;

        /// <summary>
        /// Initialise le Composant.
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="scale">Echelle</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="zLayer">Layer Z</param>
        public TransformComponent(Vec2 position = null, Vec2 scale = null, int rotation = 0, int zLayer = 0): base()
        {
            this.position = position ?? new Vec2(0);
            this.scale = scale ?? new Vec2(1);
            this.rotation = rotation;
            this.zLayer = zLayer;
        }

        public override string ToString() => $"TransformComponent(pos={position}, scale={scale}, rotation={rotation}, zLayer={zLayer})";
    }
}
