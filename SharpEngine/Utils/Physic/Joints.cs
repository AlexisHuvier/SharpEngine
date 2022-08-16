using SharpEngine.Entities;

namespace SharpEngine.Utils.Physic;

public class Joint
{
    public Entity Target;
    public JointType Type;

    protected Joint(JointType type, Entity target)
    {
        Type = type;
        Target = target;
    }
}

public class DistanceJoint : Joint
{
    public int Length;

    public DistanceJoint(JointType type, Entity target) : base(type, target)
    {
        Length = -1;
    }

    public DistanceJoint(JointType type, Entity target, int length) : base(type, target)
    {
        Length = length;
    }
}