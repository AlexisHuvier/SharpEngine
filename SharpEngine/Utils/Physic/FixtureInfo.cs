using SharpEngine.Math;

namespace SharpEngine.Utils.Physic;

/// <summary>
/// Informations of Fixture
/// </summary>
public struct FixtureInfo
{
    /// <summary>
    /// Density of Fixture
    /// </summary>
    public float Density { get; set; }
    
    /// <summary>
    /// Restitution of Fixture
    /// </summary>
    public float Restitution { get; set; }
    
    /// <summary>
    /// Friction of Fixture
    /// </summary>
    public float Friction { get; set; }
    
    /// <summary>
    /// Type of Fixture
    /// </summary>
    public FixtureType Type { get; set; }
    
    /// <summary>
    /// Additional parameter of Fixture
    /// </summary>
    public object Parameter { get; set; }
    
    /// <summary>
    /// Offset of Fixture
    /// </summary>
    public Vec2 Offset { get; set; }
    
    /// <summary>
    /// Tag of Fixture
    /// </summary>
    public FixtureTag Tag { get; set; }
}