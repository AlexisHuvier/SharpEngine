using SharpEngine.Entities;
using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.Physic.Joints;

public class Joint
{
    public Entity Target;
    public JointType Type;
    public Vec2 FromPosition;
    public Vec2 TargetPosition;

    protected Joint(JointType type, Entity target, Vec2 fromPosition, Vec2 targetPosition)
    {
        Type = type;
        Target = target;
        FromPosition = fromPosition;
        TargetPosition = targetPosition;
    }
}