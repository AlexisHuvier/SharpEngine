using System;
using System.Collections.Generic;
using SharpEngine.Math;
using SharpEngine.Utils.Physic;
using SharpEngine.Utils.Physic.Joint;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace SharpEngine.Component;

public class PhysicsComponent: Component
{
    public Body Body;

    public Func<Fixture, Fixture, Contact, bool> CollisionCallback;
    public Action<Fixture, Fixture, Contact> SeparationCallback;

    private readonly BodyType _bodyType;
    private readonly List<FixtureInfo> _fixtures = new();
    private readonly List<Joint> _joints = new();
    private readonly bool _fixedRotation;
    private readonly bool _ignoreGravity;

    private TransformComponent _transform;

    public PhysicsComponent(BodyType bodyType = BodyType.Dynamic, bool ignoreGravity = false,
        bool fixedRotation = false)
    {
        _bodyType = bodyType;
        _ignoreGravity = ignoreGravity;
        _fixedRotation = fixedRotation;
    }

    public Vec2 GetPosition() => new(Body.Position.X, Body.Position.Y);
    public void SetPosition(Vec2 position) => Body.Position = position;
    public Vec2 GetLinearVelocity() => new(Body.LinearVelocity.X, Body.LinearVelocity.Y);
    public void SetLinearVelocity(Vec2 velocity) => Body.LinearVelocity = velocity;
    public void ApplyLinearImpulse(Vec2 impulse) => Body.ApplyLinearImpulse(impulse);
    public int GetRotation() => (int)(Body.Rotation * 180 / MathHelper.Pi);
    public void SetRotation(int rotation) => Body.Rotation = rotation * MathHelper.Pi / 180f;
    
}