using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using RJoint = tainicom.Aether.Physics2D.Dynamics.Joints.RevoluteJoint;

namespace SharpEngine.Utils.Physic.Joints;

public class RevoluteJoint: Joint
{
    public RevoluteJoint(Entity target, Vec2 fromPosition = null) : base(JointType.Revolute, target, 
        fromPosition == null ? new Vec2(0): fromPosition,
        fromPosition == null ? new Vec2(0): fromPosition)
    {}

    public RJoint ToAetherPhysics(Body from) => 
        new(from, Target.GetComponent<PhysicsComponent>().Body, FromPosition.ToAetherPhysics());
}