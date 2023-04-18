using SharpEngine.Entities;
using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.Physic.Joints;

public class Joint
{
    public Entity Target { get; set; }
    public JointType Type { get; set; }
    public Vec2 FromPosition { get; set; }
    public Vec2 TargetPosition { get; set; }

    protected Joint(JointType type, Entity target, Vec2 fromPosition, Vec2 targetPosition)
    {
        Type = type;
        Target = target;
        FromPosition = fromPosition;
        TargetPosition = targetPosition;
    }
}