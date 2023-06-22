using System;
using System.Numerics;
using Raylib_cs;
using SharpEngine.Math;

namespace SharpEngine.Utils.Particle;

/// <summary>
/// CLass which represents Particle
/// </summary>
public class Particle
{
    /// <summary>
    /// Position of Particle
    /// </summary>
    public Vec2 Position;
    
    /// <summary>
    /// Velocity of Particle
    /// </summary>
    public Vec2 Velocity;

    /// <summary>
    /// Acceleration of Particle
    /// </summary>
    public Vec2 Acceleration;

    /// <summary>
    /// Lifetime of Particle
    /// </summary>
    public float Lifetime;

    /// <summary>
    /// Time Since Start of Particle
    /// </summary>
    public float TimeSinceStart;

    /// <summary>
    /// Size of Particle
    /// </summary>
    public float Size;

    /// <summary>
    /// Max Size of Particle
    /// </summary>
    public float MaxSize;

    /// <summary>
    /// Rotation of Particle
    /// </summary>
    public float Rotation;

    /// <summary>
    /// Rotation Speed of Particle
    /// </summary>
    public float RotationSpeed;

    /// <summary>
    /// Begin Color of Particle
    /// </summary>
    public Color BeginColor;

    /// <summary>
    /// Current Color of Particle
    /// </summary>
    public Color CurrentColor;

    /// <summary>
    /// End Color of Particle
    /// </summary>
    public Color EndColor;

    /// <summary>
    /// Size Function of Particle
    /// </summary>
    public ParticleParametersFunction SizeFunction;

    /// <summary>
    /// Size Function Value of Particle
    /// </summary>
    public float SizeFunctionValue;

    /// <summary>
    /// Create Particle
    /// </summary>
    /// <param name="position">Particle Position</param>
    /// <param name="velocity">Particle Velocity</param>
    /// <param name="acceleration">Particle Acceleration</param>
    /// <param name="lifetime">Particle Lifetime</param>
    /// <param name="size">Particle Size</param>
    /// <param name="rotation">Particle Rotation</param>
    /// <param name="rotationSpeed">Particle Rotation Speed</param>
    /// <param name="beginColor">Particle Begin Color</param>
    /// <param name="endColor">Particle End Color</param>
    /// <param name="sizeFunction">Particle Size Function</param>
    /// <param name="sizeFunctionValue">Particle Size Function Value</param>
    public Particle(Vec2 position, Vec2 velocity, Vec2 acceleration, float lifetime, float size, float rotation,
        float rotationSpeed, Color beginColor, Color endColor,
        ParticleParametersFunction sizeFunction = ParticleParametersFunction.Normal, float sizeFunctionValue = 0)
    {
        Position = position;
        Velocity = velocity;
        Acceleration = acceleration;
        Lifetime = lifetime;
        TimeSinceStart = 0;
        MaxSize = size;
        if (sizeFunction == ParticleParametersFunction.Increase)
            Size = 0;
        else
            Size = size;
        Rotation = rotation;
        RotationSpeed = rotationSpeed;
        BeginColor = beginColor;
        CurrentColor = beginColor;
        EndColor = endColor;
        SizeFunction = sizeFunction;
        SizeFunctionValue = sizeFunctionValue;
    }

    /// <summary>
    /// Update Particle
    /// </summary>
    /// <param name="delta">Frame Time</param>
    /// <exception cref="ArgumentOutOfRangeException">throws if SizeFunctions is out of range</exception>
    public void Update(float delta)
    {
        Velocity = new Vec2(
            Velocity.X + Acceleration.X * delta,
            Velocity.Y + Acceleration.Y * delta);
        Position = new Vec2(
            Position.X + Velocity.X * delta,
            Position.Y + Velocity.Y * delta);
        Rotation += RotationSpeed * delta;

        switch (SizeFunction)
        {
            case ParticleParametersFunction.Increase when SizeFunctionValue == 0:
                Size = MaxSize * TimeSinceStart / Lifetime;
                break;
            case ParticleParametersFunction.Increase:
                Size += SizeFunctionValue;
                break;
            case ParticleParametersFunction.Decrease when SizeFunctionValue == 0:
                Size = MaxSize * (Lifetime - TimeSinceStart) / Lifetime;
                break;
            case ParticleParametersFunction.Decrease:
                Size -= SizeFunctionValue;
                break;
            case ParticleParametersFunction.Normal:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(SizeFunction), SizeFunction, null);
        }

        if (EndColor != BeginColor)
            CurrentColor = BeginColor.TranslateTo(EndColor, TimeSinceStart, Lifetime);
        TimeSinceStart += delta;
    }

    /// <summary>
    /// Draw Particle
    /// </summary>
    public void Draw()
    {
        if (Size == 0) return;

        Raylib.DrawRectanglePro(new Rectangle(Position.X, Position.Y, Size, Size), new Vector2(Size / 2, Size / 2),
            Rotation, CurrentColor);
    }
}