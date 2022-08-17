using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using RJoint = tainicom.Aether.Physics2D.Dynamics.Joints.RopeJoint;

namespace SharpEngine.Utils.Physic.Joints;

public class RopeJoint: Joint
{
    public float MaxLength { get; set; }

    public RopeJoint(Entity target, Vec2 fromPosition = null, Vec2 targetPosition = null,
        float maxLength = -1) : base(JointType.Rope, target,  
        fromPosition == null ? new Vec2(0): fromPosition, 
        targetPosition == null ? new Vec2(0) : targetPosition)
    {
        MaxLength = maxLength;
    }

    public RJoint ToAetherPhysics(Body from)
    {
        var joint = new RJoint(from, Target.GetComponent<PhysicsComponent>().Body, 
            FromPosition.ToAetherPhysics(), TargetPosition.ToAetherPhysics());
        if (System.Math.Abs(MaxLength - (-1)) > InternalUtils.FloatTolerance)
            joint.MaxLength = MaxLength;
        return joint;
    }
}