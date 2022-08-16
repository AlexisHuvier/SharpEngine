﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using SharpEngine.Entities;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using tainicom.Aether.Physics2D.Dynamics.Joints;
using DistanceJoint = SharpEngine.Utils.Physic.DistanceJoint;
using Joint = SharpEngine.Utils.Physic.Joint;
using JointType = SharpEngine.Utils.Physic.JointType;

namespace SharpEngine.Components;

/// <summary>
/// Composant de physique basique
/// </summary>
public class PhysicsComponent : Component
{
    public Body Body;

    public Func<Fixture, Fixture, Contact, bool> CollisionCallback;
    public Action<Fixture, Fixture, Contact> SeparationCallback;
    
    private BodyType _bodyType;
    private List<FixtureInfo> _fixtures = new();
    private List<Joint> _joints = new();

    /// <summary>
    /// Initialise le Composant
    /// </summary>
    /// <param name="bodyType">Type de corps</param>
    public PhysicsComponent(BodyType bodyType = BodyType.Dynamic)
    {
        _bodyType = bodyType;
    }

    public Vec2 GetPosition() => new(Body.Position.X, Body.Position.Y);
    public void SetPosition(Vec2 position) => Body.Position = new Vector2(position.X, position.Y);

    public Vec2 GetLinearVelocity() => new(Body.LinearVelocity.X, Body.LinearVelocity.Y);
    public void SetLinearVelocity(Vec2 velocity) => Body.LinearVelocity = new Vector2(velocity.X, velocity.Y);

    public void ApplyLinearImpulse(Vec2 impulse) => Body.ApplyLinearImpulse(new Vector2(impulse.X, impulse.Y));

    public int GetRotation() => (int)(Body.Rotation * 180 / System.Math.PI);
    public void SetRotation(int rotation) => Body.Rotation = (float)(rotation * System.Math.PI / 180f);

    public void AddRectangleCollision(Vec2 size, Vec2 offset = null, float density = 1f, float restitution = 0.5f, float friction = 0.5f)
    {
        var fixture = new FixtureInfo()
        {
            Density = density,
            Restitution = restitution,
            Friction = friction,
            Type = FixtureType.Rectangle,
            Parameter = size,
            Offset = offset ?? new Vec2(0)
        };
        _fixtures.Add(fixture);
    }

    public void AddCircleCollision(float radius, Vec2 offset = null, float density = 1f, float restitution = 0.5f, float friction = 0.5f)
    {
        var fixture = new FixtureInfo()
        {
            Density = density,
            Restitution = restitution,
            Friction = friction,
            Type = FixtureType.Circle,
            Parameter = radius,
            Offset = offset ?? new Vec2(0)
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

        Body = Entity.Scene.World.CreateBody(bodyType: _bodyType);

        if (Entity.GetComponent<TransformComponent>() is not { } tc) return;
        
        Body.Rotation = (float)(tc.Rotation * System.Math.PI / 180f);
        Body.Position = new Vector2(tc.Position.X, tc.Position.Y);
        Body.OnCollision += OnCollision;
        Body.OnSeparation += OnSeparation;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        foreach(var info in _fixtures)
        {
            Fixture fixture;
            switch(info.Type)
            {
                case FixtureType.Rectangle:
                    var size = info.Parameter as Vec2;
                    Debug.Assert(size != null, nameof(size) + " != null");
                    fixture = Body.CreateRectangle(size.X, size.Y, info.Density, info.Offset.ToAetherPhysics());
                    break;
                case FixtureType.Circle:
                    var radius = Convert.ToSingle(info.Parameter);
                    fixture = Body.CreateCircle(radius, info.Density, info.Offset.ToAetherPhysics());
                    break;
                default:
                    throw new Exception($"Unknown Type of Fixture : {info.Type}");
            }
            fixture.Restitution = info.Restitution;
            fixture.Friction = info.Friction;
        }
        _fixtures.Clear();

        foreach (var joint in _joints)
        {
            switch (joint.Type)
            {
                case JointType.Distance:
                    var aetherJoint = new tainicom.Aether.Physics2D.Dynamics.Joints.DistanceJoint(
                        Body, joint.Target.GetComponent<PhysicsComponent>().Body,
                        new Vec2(0).ToAetherPhysics(), new Vec2(0).ToAetherPhysics());
                    var distance = ((DistanceJoint)joint).Length;
                    if (distance != -1)
                        aetherJoint.Length = distance;
                    Entity.Scene.World.Add(aetherJoint);
                    break;
                default:
                    throw new Exception($"Unknown Type of Joint : {joint.Type}");
            }
        }
        _joints.Clear();

        if (Entity.GetComponent<TransformComponent>() is not { } tc) return;
        
        tc.Position.X = Body.Position.X;
        tc.Position.Y = Body.Position.Y;
        tc.Rotation = (int)(Body.Rotation * 180 / Math.PI);
    }
    
    private bool OnCollision(Fixture sender, Fixture other, Contact contact) => CollisionCallback == null || CollisionCallback(sender, other, contact);
    private void OnSeparation(Fixture sender, Fixture other, Contact contact) => SeparationCallback?.Invoke(sender, other, contact);
    public override string ToString() => $"PhysicsComponent(body={Body}, nbFixtures={_fixtures.Count})";
}

