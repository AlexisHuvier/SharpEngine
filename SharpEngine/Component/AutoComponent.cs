using SharpEngine.Math;

namespace SharpEngine.Component;

/// <summary>
/// Component which create automatic movements
/// </summary>
public class AutoComponent: Component
{
    /// <summary>
    /// Automatic Direction
    /// </summary>
    public Vec2 Direction;
    
    /// <summary>
    /// Automatic Rotation
    /// </summary>
    public int Rotation;

    private TransformComponent? _transform;
    private PhysicsComponent? _physics;

    /// <summary>
    /// Create Auto Component
    /// </summary>
    /// <param name="direction">Direction</param>
    /// <param name="rotation">Rotation</param>
    public AutoComponent(Vec2? direction = null, int rotation = 0)
    {
        Direction = direction ?? Vec2.Zero;
        Rotation = rotation;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transform = Entity?.GetComponentAs<TransformComponent>();
        _physics = Entity?.GetComponentAs<PhysicsComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);
        
        if(_transform == null) return;

        if (Direction.Length != 0)
        {
            if (_physics != null)
                _physics.SetLinearVelocity(Direction);
            else
                _transform.Position = new Vec2(
                    _transform.Position.X + Direction.X * delta,
                    _transform.Position.Y + Direction.Y * delta);
        }

        if (Rotation != 0)
            _transform.Rotation += Rotation;
    }
}