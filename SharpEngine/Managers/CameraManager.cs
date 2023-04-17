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
    public static Vec2 Position { get; set; } = new(0);
    public static Entity FollowEntity { get; set; }

    public static void Update(Vec2 windowSize)
    {
        if(FollowEntity?.GetComponent<TransformComponent>() is {} tc)
            Position = tc.Position - windowSize / 2;
    }
}
