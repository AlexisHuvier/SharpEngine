using System;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

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
    public Func<Fixture, Fixture, Contact, bool> OnCollision;
}