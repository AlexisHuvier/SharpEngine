using System;
using SharpEngine.Math;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace SharpEngine.Utils.Physic;

/// <summary>
/// Informations of Fixture
/// </summary>
public struct FixtureInfo
{
    /// <summary>
    /// Density of Fixture
    /// </summary>
    public float Density;
    
    /// <summary>
    /// Restitution of Fixture
    /// </summary>
    public float Restitution;
    
    /// <summary>
    /// Friction of Fixture
    /// </summary>
    public float Friction;
    
    /// <summary>
    /// Type of Fixture
    /// </summary>
    public FixtureType Type;
    
    /// <summary>
    /// Additional parameter of Fixture
    /// </summary>
    public object Parameter;
    
    /// <summary>
    /// Offset of Fixture
    /// </summary>
    public Vec2 Offset;
    
    /// <summary>
    /// Tag of Fixture
    /// </summary>
    public FixtureTag Tag;
    
    /// <summary>
    /// Function which define if Collision must block fixture
    /// </summary>
    public Func<Fixture, Fixture, Contact, bool>? OnCollision;
}