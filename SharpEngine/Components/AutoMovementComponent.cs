using SharpEngine.Utils.Math;

namespace SharpEngine.Components;

/// <summary>
/// Composant permettant un mouvement ou une rotation automatique
/// </summary>
public class AutoMovementComponent : Component
{
    public Vec2 Direction;
    public int Rotation;

    private TransformComponent _transformComponent;
    private PhysicsComponent _physicsComponent;

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

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = Entity.GetComponent<TransformComponent>();
        _physicsComponent = Entity.GetComponent<PhysicsComponent>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);


        if (_transformComponent == null) return;
        
        if (Direction.Length != 0)
        {
            var pos = new Vec2(_transformComponent.Position.X, _transformComponent.Position.Y) + Direction;
            if (_physicsComponent != null)
                _physicsComponent.SetPosition(pos);
            else
                _transformComponent.Position = pos;
        }

        if (Rotation == 0) return;
        
        var rot = _transformComponent.Rotation + Rotation;
        _transformComponent.Rotation = rot;
    }

    public override string ToString() => $"AutoMovementComponent(direction={Direction}, rotation={Rotation})";
}
