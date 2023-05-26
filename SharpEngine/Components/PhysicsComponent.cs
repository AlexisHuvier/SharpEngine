using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using SharpEngine.Entities;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using SharpEngine.Utils.Physic.Joints;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Components;

/// <summary>
/// Composant de physique basique
/// </summary>
public class PhysicsComponent : Component
{
    public Body Body;

    public Func<Fixture, Fixture, Contact, bool> CollisionCallback;
    public Action<Fixture, Fixture, Contact> SeparationCallback;
    
    private readonly BodyType _bodyType;
    private readonly List<FixtureInfo> _fixtures = new();
    private readonly List<Joint> _joints = new();
    private readonly bool _fixedRotation;
    private readonly bool _ignoreGravity;

    private TransformComponent _transformComponent;

    /// <summary>
    /// Initialise le Composant
    /// </summary>
    /// <param name="bodyType">Type de corps</param>
    /// <param name="ignoreGravity">Bloquage de la gravité</param>
    /// <param name="fixedRotation">Bloquage de la rotation</param>
    public PhysicsComponent(BodyType bodyType = BodyType.Dynamic, bool ignoreGravity = false, bool fixedRotation = false)
    {
        _bodyType = bodyType;
        _fixedRotation = fixedRotation;
        _ignoreGravity = ignoreGravity;
    }

    public Vec2 GetPosition() => new(Body.Position.X, Body.Position.Y);
    public void SetPosition(Vec2 position) => Body.Position = new Vector2(position.X, position.Y);
    public Vec2 GetLinearVelocity() => new(Body.LinearVelocity.X, Body.LinearVelocity.Y);
    public void SetLinearVelocity(Vec2 velocity) => Body.LinearVelocity = new Vector2(velocity.X, velocity.Y);
    public void ApplyLinearImpulse(Vec2 impulse) => Body.ApplyLinearImpulse(new Vector2(impulse.X, impulse.Y));
    public int GetRotation() => (int)(Body.Rotation * 180 / Math.PI);
    public void SetRotation(int rotation) => Body.Rotation = (float)(rotation * Math.PI / 180f);

    public void AddRectangleCollision(Vec2 size, Vec2? offset = null, float density = 1f, float restitution = 0.5f, 
        float friction = 0.5f, FixtureTag tag = FixtureTag.Normal, Func<Fixture, Fixture, Contact, bool> onCollision = null)
    {
        var fixture = new FixtureInfo()
        {
            Density = density,
            Restitution = restitution,
            Friction = friction,
            Type = FixtureType.Rectangle,
            Parameter = size,
            Offset = offset ?? Vec2.Zero,
            Tag = tag,
            OnCollision = onCollision
        };
        _fixtures.Add(fixture);
    }

    public void AddCircleCollision(float radius, Vec2? offset = null, float density = 1f, float restitution = 0.5f, 
        float friction = 0.5f, FixtureTag tag = FixtureTag.Normal, Func<Fixture, Fixture, Contact, bool> onCollision = null)
    {
        var fixture = new FixtureInfo()
        {
            Density = density,
            Restitution = restitution,
            Friction = friction,
            Type = FixtureType.Circle,
            Parameter = radius,
            Offset = offset ?? Vec2.Zero,
            Tag = tag,
            OnCollision = onCollision
        };
        _fixtures.Add(fixture);
    }

    public void AddJoin(Joint joint)
    {
        _joints.Add(joint);
    }

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = Entity.GetComponent<TransformComponent>();
        Body = Entity.Scene.World.CreateBody(bodyType: _bodyType);

        if (_transformComponent == null) return;
        
        Body.Rotation = (float)(_transformComponent.Rotation * Math.PI / 180f);
        Body.Position = new Vector2(_transformComponent.Position.X, _transformComponent.Position.Y);
        Body.FixedRotation = _fixedRotation;
        Body.IgnoreGravity = _ignoreGravity;
        Body.OnCollision += OnCollision;
        Body.OnSeparation += OnSeparation;
    }

    public virtual void RemoveBody()
    {
        if(Body != null)
            Entity?.Scene?.World?.Remove(Body);
        Body = null;
    }

    public override void SetEntity(Entity entity)
    {
        if(entity == null)
            RemoveBody();
        base.SetEntity(entity);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        if(Body == null)
            return;
        
        foreach(var info in _fixtures)
        {
            Fixture fixture;
            switch(info.Type)
            {
                case FixtureType.Rectangle:
                    var size = info.Parameter as Vec2?;
                    Debug.Assert(size != null, nameof(size) + " != null");
                    fixture = Body.CreateRectangle(size.Value.X, size.Value.Y, info.Density, info.Offset);
                    break;
                case FixtureType.Circle:
                    var radius = Convert.ToSingle(info.Parameter);
                    fixture = Body.CreateCircle(radius, info.Density, info.Offset);
                    break;
                default:
                    throw new Exception($"Unknown Type of Fixture : {info.Type}");
            }
            fixture.OnCollision += (sender, other, contact) => info.OnCollision?.Invoke(sender, other, contact) ?? true;
            fixture.Tag = info.Tag;
            fixture.Restitution = info.Restitution;
            fixture.Friction = info.Friction;
        }
        _fixtures.Clear();

        foreach (var joint in _joints)
        {
            switch (joint.Type)
            {
                case JointType.Distance:
                    Entity.Scene.World.Add(((DistanceJoint)joint).ToAetherPhysics(Body));
                    break;
                case JointType.Revolute:
                    Entity.Scene.World.Add(((RevoluteJoint)joint).ToAetherPhysics(Body));
                    break;
                case JointType.Rope:
                    Entity.Scene.World.Add(((RopeJoint)joint).ToAetherPhysics(Body));
                    break;
                default:
                    throw new Exception($"Unknown Type of Joint : {joint.Type}");
            }
        }
        _joints.Clear();

        if (_transformComponent == null) return;

        _transformComponent.Position = new Vec2(Body.Position.X, Body.Position.Y);
        _transformComponent.Rotation = (int)(Body.Rotation * 180 / Math.PI);
    }

    private bool OnCollision(Fixture sender, Fixture other, Contact contact)
    {
        var result = true;
        if (CollisionCallback != null)
            result = CollisionCallback(sender, other, contact);

        if ((FixtureTag)sender.Tag == FixtureTag.IgnoreCollisions || (FixtureTag)other.Tag == FixtureTag.IgnoreCollisions)
            result = false;
        return result;
    }
    
    private void OnSeparation(Fixture sender, Fixture other, Contact contact) => SeparationCallback?.Invoke(sender, other, contact);
    public override string ToString() => $"PhysicsComponent(body={Body}, nbFixtures={_fixtures.Count})";
}

