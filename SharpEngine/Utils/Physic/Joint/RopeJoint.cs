using SharpEngine.Math;

namespace SharpEngine.Utils.Physic.Joint;

/// <summary>
/// Rope Joint
/// </summary>
public class RopeJoint: Joint
{
    /// <summary>
    /// Max Length of Joint
    /// </summary>
    public float MaxLength;

    /// <summary>
    /// Create Rope Joint
    /// </summary>
    /// <param name="target">Joint Target</param>
    /// <param name="fromPosition">Joint From Position</param>
    /// <param name="targetPosition">Joint Target Position</param>
    /// <param name="maxLength">Joint Max Length</param>
    public RopeJoint(Entity.Entity target, Vec2? fromPosition = null, Vec2? targetPosition = null, float maxLength = -1)
        : base(target, JointType.Rope, fromPosition ?? Vec2.Zero, targetPosition ?? Vec2.Zero)
    {
        MaxLength = maxLength;
    }
    
    // TODO: Create ToAetherPhysics
}