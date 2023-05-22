using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion de la Caméra
/// </summary>
public static class CameraManager
{
    public static Vec2 Position = Vec2.Zero;
    public static Entity FollowEntity;

    public static void Update(Vec2 windowSize)
    {
        if (FollowEntity?.GetComponent<TransformComponent>() is { } tc)
            Position = new Vec2(
                tc.Position.X - windowSize.X / 2,
                tc.Position.Y - windowSize.Y / 2
            );
    }
}
