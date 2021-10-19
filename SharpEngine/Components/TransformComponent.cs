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
        /// Initialise le Composant.<para/>
        /// -> Paramètre 1 : Position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 2 : Echelle (<seealso cref="Vec2"/>) (Vec2(1))<para/>
        /// -> Paramètre 3 : Rotation (int) (0)<para/>
        /// -> Paramètre 3 : Layer Z (int) (0)
        /// </summary>
        /// <param name="parameters">Paramètres du Composant</param>
        public TransformComponent(params object[] parameters): base(parameters)
        {
            position = new Vec2(0);
            scale = new Vec2(1);
            rotation = 0;
            zLayer = 0;

            if (parameters.Length >= 1 && parameters[0] is Vec2 pos)
                position = pos;
            if (parameters.Length >= 2 && parameters[1] is Vec2 sca)
                scale = sca;
            if (parameters.Length >= 3 && parameters[2] is int rot)
                rotation = rot;
            if (parameters.Length >= 4 && parameters[3] is int z)
                zLayer = z;
        }

        public override string ToString()
        {
            return $"TransformComponent(pos={position}, scale={scale}, rotation={rotation}, zLayer={zLayer})";
        }
    }
}
