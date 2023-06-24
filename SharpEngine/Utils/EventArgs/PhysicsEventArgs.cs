using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace SharpEngine.Utils.EventArgs;

/// <summary>
/// Event Args for physics
/// </summary>
public class PhysicsEventArgs: BoolEventArgs
{
    /// <summary>
    /// Sender Fixture of Event
    /// </summary>
    public Fixture Sender { get; set; }
    
    /// <summary>
    /// Other Fixture of Event
    /// </summary>
    public Fixture Other { get; set; }
    
    /// <summary>
    /// Contact of Event
    /// </summary>
    public Contact Contact { get; set; }
}