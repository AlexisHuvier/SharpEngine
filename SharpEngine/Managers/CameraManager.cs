using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion de la Caméra
/// </summary>
public class CameraManager
{
    public static Vec2 Position { get; set; } = new(0);
    public static Entity FollowEntity { get; set; } = null;

    public static void Update(Vec2 windowSize)
    {
        if(FollowEntity?.GetComponent<TransformComponent>() is {} tc)
            Position = tc.Position - windowSize / 2;
    }
}
