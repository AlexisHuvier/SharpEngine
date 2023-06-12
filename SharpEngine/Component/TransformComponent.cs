using SharpEngine.Manager;
using SharpEngine.Math;

namespace SharpEngine.Component;

/// <summary>
/// Component which define Transform (Position, Rotation, Scale)
/// </summary>
public class TransformComponent: Component
{
    /// <summary>
    /// Position of Component
    /// </summary>
    public Vec2 Position;
    
    /// <summary>
    /// Scale of Component
    /// </summary>
    public Vec2 Scale;
    
    /// <summary>
    /// Rotation of Component
    /// </summary>
    public int Rotation;

    /// <summary>
    /// Create TransformComponent
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="scale">Scale (Vec2(1))</param>
    /// <param name="rotation">Rotation (0)</param>
    public TransformComponent(Vec2? position = null, Vec2? scale = null, int rotation = 0)
    {
        Position = position ?? Vec2.Zero;
        Scale = scale ?? Vec2.One;
        Rotation = rotation;
    }

    /// <summary>
    /// Get transformed Position
    /// </summary>
    /// <param name="offset">Offset (Vec2(0))</param>
    /// <param name="useCameraPosition">Apply Camera Position (true)</param>
    /// <returns>Transformed Position</returns>
    public Vec2 GetTransformedPosition(Vec2? offset = null, bool useCameraPosition = true)
    {
        offset ??= Vec2.Zero;
        if (useCameraPosition)
            return new Vec2(
                Position.X + offset.Value.X,
                Position.Y + offset.Value.Y
            );
        return new Vec2(
            Position.X + offset.Value.X,
            Position.Y + offset.Value.Y
        );
    }
}