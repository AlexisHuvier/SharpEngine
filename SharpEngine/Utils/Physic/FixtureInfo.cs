using SharpEngine.Utils.Math;

namespace SharpEngine.Utils.Physic;

public class FixtureInfo
{
    public float Density;
    public float Restitution;
    public float Friction;
    public FixtureType Type;
    public object Parameter;
    public Vec2 Offset;
    public FixtureTag Tag;
}