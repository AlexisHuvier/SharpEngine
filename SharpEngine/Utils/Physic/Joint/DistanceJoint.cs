using SharpEngine.Component;
using SharpEngine.Math;
using tainicom.Aether.Physics2D.Dynamics;
using DJoint = tainicom.Aether.Physics2D.Dynamics.Joints.DistanceJoint;

namespace SharpEngine.Utils.Physic.Joint;

/// <summary>
/// Distance Joint
/// </summary>
public class DistanceJoint: Joint
{
    /// <summary>
    /// Length of Joint
    /// </summary>
    public float Length;
    
    /// <summary>
    /// Frequency of Joint
    /// </summary>
    public float Frequency;
    
    /// <summary>
    /// Damping Ratio of Joint
    /// </summary>
    public float DampingRatio;

    /// <summary>
    /// Create Distance Joint
    /// </summary>
    /// <param name="target">Joint Target</param>
    /// <param name="fromPosition">Joint From Position</param>
    /// <param name="targetPosition">Joint Target Position</param>
    /// <param name="length">Joint Length</param>
    /// <param name="frequency">Joint Frequency</param>
    /// <param name="dampingRatio">Joint Damping Ratio</param>
    public DistanceJoint(Entity.Entity target, Vec2? fromPosition = null, Vec2? targetPosition = null,
        float length = -1, float frequency = -1, float dampingRatio = -1) : base(target, JointType.Distance,
        fromPosition ?? Vec2.Zero, targetPosition ?? Vec2.Zero)
    {
        Length = length;
        Frequency = frequency;
        DampingRatio = dampingRatio;
    }

    public DJoint ToAetherPhysics(Body from)
    {
        var joint = new DJoint(from, Target.GetComponentAs<PhysicsComponent>()?.Body, FromPosition, TargetPosition);
        if (System.Math.Abs(Length + 1) > Internal.FloatTolerance)
            joint.Length = Length;
        if (System.Math.Abs(Frequency + 1) > Internal.FloatTolerance)
            joint.Frequency = Frequency;
        if (System.Math.Abs(DampingRatio + 1) > Internal.FloatTolerance)
            joint.DampingRatio = DampingRatio;
        return joint;
    }
}