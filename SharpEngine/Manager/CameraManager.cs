using SharpEngine.Component;
using SharpEngine.Math;

namespace SharpEngine.Manager;

/// <summary>
/// Static class which manager Camera information
/// </summary>
public static class CameraManager
{
    /// <summary>
    /// Position of Camera
    /// </summary>
    public static Vec2 Position = Vec2.Zero;
    
    /// <summary>
    /// Entity followed by Camera (will always be in center of screen)
    /// </summary>
    public static Entity.Entity? FollowEntity = null;

    /// <summary>
    /// Update CameraManager
    /// </summary>
    /// <param name="screenSize"></param>
    public static void Update(Vec2I screenSize)
    {
        if (FollowEntity?.GetComponentAs<TransformComponent>() is { } tc)
            Position = new Vec2(
                tc.Position.X - screenSize.X / 2f,
                tc.Position.Y - screenSize.Y / 2f
            );
    }
}