using System;
using System.Collections.Generic;
using System.Diagnostics;
using Raylib_cs;
using SharpEngine.Math;
using SharpEngine.Renderer;
using SharpEngine.Utils.EventArgs;
using SharpEngine.Utils.Physic;
using SharpEngine.Utils.Physic.Joint;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Component;

/// <summary>
/// Components which add physics
/// </summary>
public class PhysicsComponent: Component
{
    /// <summary>
    /// Body Physics
    /// </summary>
    public Body? Body { get; set; }
    
    /// <summary>
    /// Draw debug information
    /// </summary>
    public bool DebugDraw { get; set; }

    /// <summary>
    /// Event which be called when collision
    /// </summary>
    public EventHandler<PhysicsEventArgs>? CollisionCallback { get; set; }
    
    /// <summary>
    /// Event which be called when separation
    /// </summary>
    public EventHandler<PhysicsEventArgs>? SeparationCallback { get; set; }

    private readonly BodyType _bodyType;
    private readonly List<FixtureInfo> _fixtures = new();
    private readonly List<Joint> _joints = new();
    private readonly bool _fixedRotation;
    private readonly bool _ignoreGravity;
    private readonly List<List<object>> _debugDrawings = new();
    private readonly List<Contact> _contacts = new();

    private TransformComponent? _transform;

    /// <summary>
    /// Create PhysicsComponent
    /// </summary>
    /// <param name="bodyType">Type of Body</param>
    /// <param name="ignoreGravity">Ignore Gravity</param>
    /// <param name="fixedRotation">Rotation fixed</param>
    /// <param name="debugDraw">Debug Draw</param>
    public PhysicsComponent(BodyType bodyType = BodyType.Dynamic, bool ignoreGravity = false,
        bool fixedRotation = false, bool debugDraw = false)
    {
        _bodyType = bodyType;
        _ignoreGravity = ignoreGravity;
        _fixedRotation = fixedRotation;
        DebugDraw = debugDraw;
    }

    /// <summary>
    /// Return Position of Body
    /// </summary>
    /// <returns>Body Position</returns>
    public Vec2 GetPosition() => new(Body.Position.X * 50, Body.Position.Y / 50);
    
    /// <summary>
    /// Define Position of Body
    /// </summary>
    /// <param name="position">Body Position</param>
    public void SetPosition(Vec2 position) => Body.Position = position * 0.02f;
    
    /// <summary>
    /// Return Linear Velocity of Body
    /// </summary>
    /// <returns>Body Linear Velocity</returns>
    public Vec2 GetLinearVelocity() => new(Body.LinearVelocity.X * 50, Body.LinearVelocity.Y * 50);
    
    /// <summary>
    /// Define Linear Velocity of Body
    /// </summary>
    /// <param name="velocity">Body Linear Velocity</param>
    public void SetLinearVelocity(Vec2 velocity) => Body.LinearVelocity = velocity * 0.02f;

    /// <summary>
    /// Apply Impulse to Body
    /// </summary>
    /// <param name="impulse">Linear Impulse</param>
    public void ApplyLinearImpulse(Vec2 impulse) => Body.ApplyLinearImpulse(impulse * 0.02f);
    
    /// <summary>
    /// Return Rotation of Body
    /// </summary>
    /// <returns>Body Rotation</returns>
    public int GetRotation() => (int)(Body.Rotation * 180 / MathHelper.Pi);
    
    /// <summary>
    /// Define Rotation of Body
    /// </summary>
    /// <param name="rotation">Body Rotation</param>
    public void SetRotation(int rotation) => Body.Rotation = rotation * MathHelper.Pi / 180f;
    
    /// <summary>
    /// Add Rectangle Collision
    /// </summary>
    /// <param name="size">Collision Size</param>
    /// <param name="offset">Collision Offset</param>
    /// <param name="density">Collision Density</param>
    /// <param name="restitution">Collision Restitution</param>
    /// <param name="friction">Collision Friction</param>
    /// <param name="tag">Collision Tag</param>
    public void AddRectangleCollision(Vec2 size, Vec2? offset = null, float density = 1f, float restitution = 0.5f, 
        float friction = 0.5f, FixtureTag tag = FixtureTag.Normal)
    {
        var fixture = new FixtureInfo
        {
            Density = density,
            Restitution = restitution,
            Friction = friction,
            Type = FixtureType.Rectangle,
            Parameter = size * 0.02f,
            Offset = offset * 0.02f ?? Vec2.Zero,
            Tag = tag
        };
        _debugDrawings.Add(new List<object> { "rectangle", size, offset ?? Vec2.Zero});
        _fixtures.Add(fixture);
    }

    /// <summary>
    /// Add Circle Collision
    /// </summary>
    /// <param name="radius">Collision Radius</param>
    /// <param name="offset">Collision Offset</param>
    /// <param name="density">Collision Density</param>
    /// <param name="restitution">Collision Restitution</param>
    /// <param name="friction">Collision Friction</param>
    /// <param name="tag">Collision Tag</param>
    public void AddCircleCollision(float radius, Vec2? offset = null, float density = 1f, float restitution = 0.5f, 
        float friction = 0.5f, FixtureTag tag = FixtureTag.Normal)
    {
        var fixture = new FixtureInfo
        {
            Density = density,
            Restitution = restitution,
            Friction = friction,
            Type = FixtureType.Circle,
            Parameter = radius * 0.02f,
            Offset = offset * 0.02f ?? Vec2.Zero,
            Tag = tag
        };
        _debugDrawings.Add(new List<object> { "circle", radius, offset ?? Vec2.Zero});
        _fixtures.Add(fixture);
    }

    /// <summary>
    /// Return if entity is on ground
    /// </summary>
    /// <returns>If is on ground</returns>
    public bool IsOnGround()
    {
        if (GetLinearVelocity().Y != 0)
            return false;
        foreach (var contact in _contacts)
        {
            contact.GetWorldManifold(out var normal, out _);
            if (normal.X == 0 && System.Math.Abs(normal.Y + 1) < Internal.FloatTolerance)
                return true;
        }
        return false;
    }
    
    /// <summary>
    /// Add joint
    /// </summary>
    /// <param name="joint">Joint</param>
    public void AddJoint(Joint joint) => _joints.Add(joint);

    /// <summary>
    /// Remove Body
    /// </summary>
    public void RemoveBody()
    {
        if (Body != null)
            Entity?.Scene?.World.Remove(Body);
        Body = null;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transform = Entity?.GetComponentAs<TransformComponent>();
        
        if(Entity == null || _transform == null ) return;

        var body = Entity.Scene?.World.CreateBody(_transform.Position * 0.02f, MathHelper.ToRadians(_transform.Rotation), _bodyType);
        
        if(body == null) return;
        Body = body;
        Body.FixedRotation = _fixedRotation;
        Body.IgnoreGravity = _ignoreGravity;
        Body.OnCollision += OnCollision;
        Body.OnSeparation += OnSeparation;
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);
        
        if(Body == null)
            return;

        // Create Fixtures
        foreach (var info in _fixtures)
        {
            Fixture fixture;
            switch (info.Type)
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

            fixture.Tag = info.Tag;
            fixture.Restitution = info.Restitution;
            fixture.Friction = info.Friction;
        }
        _fixtures.Clear();

        // Create Joint
        foreach (var joint in _joints)
        {
            switch (joint.Type)
            {
                case JointType.Distance:
                    Entity?.Scene?.World.Add(((DistanceJoint)joint).ToAetherPhysics(Body));
                    break;
                case JointType.Revolute:
                    Entity?.Scene?.World.Add(((RevoluteJoint)joint).ToAetherPhysics(Body));
                    break;
                case JointType.Rope:
                    Entity?.Scene?.World.Add(((RopeJoint)joint).ToAetherPhysics(Body));
                    break;
                default:
                    throw new Exception($"Unknown Type of Joint : {joint.Type}");
            }
        }
        _joints.Clear();
        
        if(_transform == null) return;

        _transform.Position = Body.Position * 50f;
        _transform.Rotation = (int)MathHelper.ToDegrees(Body.Rotation);
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();
        
        if(_transform == null) return;

        if (DebugDraw)
        {
            foreach (var drawing in _debugDrawings)
            {
                switch ((string)drawing[0])
                {
                    case "rectangle":
                        var size = (Vec2)drawing[1];
                        var offset = (Vec2)drawing[2];
                        var rect = new Rect(
                            _transform.Position.X + offset.X - size.X / 2,
                            _transform.Position.Y + offset.Y - size.Y / 2,
                            size.X, size.Y
                        );
                        DMRender.DrawRectangleLines(rect, 2, Color.DarkRed, InstructionSource.Entity, int.MaxValue);
                        break;
                    case "circle":
                        var radius = (float)drawing[1];
                        var offsetCirc = (Vec2)drawing[2];
                        DMRender.DrawCircleLines(
                            (int)(_transform.Position.X + offsetCirc.X),
                            (int)(_transform.Position.Y + offsetCirc.Y), radius, Color.DarkRed,
                            InstructionSource.Entity, int.MaxValue);
                        break;
                }
            }
        }
    }

    private bool OnCollision(Fixture sender, Fixture other, Contact contact)
    {
        var eventArgs = new PhysicsEventArgs
        {
            Sender = sender,
            Other = other,
            Contact = contact,
            Result = true
        };

        CollisionCallback?.Invoke(this, eventArgs);

        if ((FixtureTag)sender.Tag == FixtureTag.IgnoreCollisions || (FixtureTag)other.Tag == FixtureTag.IgnoreCollisions)
            eventArgs.Result = false;
        
        if(eventArgs.Result)
            _contacts.Add(contact);

        return eventArgs.Result;
    }

    private void OnSeparation(Fixture sender, Fixture other, Contact contact)
    {
        var eventArgs = new PhysicsEventArgs
        {
            Sender = sender,
            Other = other,
            Contact = contact,
            Result = true
        };
        
        SeparationCallback?.Invoke(this, eventArgs);
        _contacts.Remove(contact);
    }

    /// <inheritdoc />
    public override string ToString() => $"PhysicsComponent(body={Body}, nbFixtures={_fixtures.Count})";
}