using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Components;

/// <summary>
/// Composant ajoutant la position, la rotation et l'échelle
/// </summary>
public class TransformComponent: Component
{
    public Vec2 Position { get; set; }
    public Vec2 Scale { get; set; }
    public int Rotation { get; set; }
    public int ZLayer { 
        get => (int)(LayerDepth * 4096);
        set => LayerDepth = value / 4096f;
    }

    protected internal float LayerDepth;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="scale">Echelle</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="zLayer">Layer Z</param>
    public TransformComponent(Vec2? position = null, Vec2? scale = null, int rotation = 0, int zLayer = 0)
    {
        Position = position ?? Vec2.Zero;
        Scale = scale ?? Vec2.One;
        Rotation = rotation;
        ZLayer = zLayer;
    }

    public override string ToString() => $"TransformComponent(pos={Position}, scale={Scale}, rotation={Rotation}, zLayer={ZLayer})";
}
