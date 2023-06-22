using SharpEngine.Component;
using SharpEngine.Math;
using tainicom.Aether.Physics2D.Dynamics;
using RJoint = tainicom.Aether.Physics2D.Dynamics.Joints.RevoluteJoint;

namespace SharpEngine.Utils.Physic.Joint;

/// <summary>
/// Revolution Joint
/// </summary>
public class RevoluteJoint: Joint
{
    /// <summary>
    /// Create Revolution Joint
    /// </summary>
    /// <param name="target">Joint Target</param>
    /// <param name="fromPosition">Joint From Position</param>
    public RevoluteJoint(Entity.Entity target, Vec2? fromPosition) : base(target, JointType.Revolute, fromPosition ?? Vec2.Zero, fromPosition ?? Vec2.Zero)
    {}

    public RJoint ToAetherPhysics(Body from) => new(from, Target.GetComponentAs<PhysicsComponent>()?.Body, FromPosition);
}