using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using DJoint = tainicom.Aether.Physics2D.Dynamics.Joints.DistanceJoint;

namespace SharpEngine.Utils.Physic.Joints;

public class DistanceJoint: Joint
{
    public float Length;
    public float Frequency;
    public float DampingRatio;

    public DistanceJoint(Entity target, Vec2? fromPosition = null, Vec2? targetPosition = null,
        float length = -1, float frequency = -1, float dampingRatio = -1) : base(JointType.Distance, target,
        fromPosition ?? Vec2.Zero, targetPosition ?? Vec2.Zero)
    {
        Length = length;
        Frequency = frequency;
        DampingRatio = dampingRatio;
    }

    public DJoint ToAetherPhysics(Body from)
    {
        var joint = new DJoint(from, Target.GetComponent<PhysicsComponent>().Body, 
            FromPosition, TargetPosition);
        if (System.Math.Abs(Length + 1) > InternalUtils.FloatTolerance)
            joint.Length = Length;
        if (System.Math.Abs(Frequency + 1) > InternalUtils.FloatTolerance)
            joint.Frequency = Frequency;
        if (System.Math.Abs(DampingRatio + 1) > InternalUtils.FloatTolerance)
            joint.DampingRatio = DampingRatio;
        return joint;
    }
}