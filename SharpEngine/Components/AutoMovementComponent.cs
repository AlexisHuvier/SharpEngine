using System.Collections.Specialized;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Components;

/// <summary>
/// Composant permettant un mouvement ou une rotation automatique
/// </summary>
public class AutoMovementComponent : Component
{
    public Vec2 Direction { get; set; }
    public int Rotation { get; set; }

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="direction">Mouvement automatique (Vec2(0))</param>
    /// <param name="rotation">Rotation automatique</param>
    public AutoMovementComponent(Vec2? direction = null, int rotation = 0)
    {
        Direction = direction ?? Vec2.Zero;
        Rotation = rotation;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);


        if (Entity.GetComponent<TransformComponent>() is not { } tc) return;
        
        if (Direction.Length() != 0)
        {
            var pos = new Vec2(tc.Position.X, tc.Position.Y) + Direction;
            if (Entity.GetComponent<PhysicsComponent>() is { } pc)
                pc.SetPosition(pos);
            else
                tc.Position = pos;
        }

        if (Rotation == 0) return;
        
        var rot = tc.Rotation + Rotation;
        tc.Rotation = rot;
    }

    public override string ToString() => $"AutoMovementComponent(direction={Direction}, rotation={Rotation})";
}
