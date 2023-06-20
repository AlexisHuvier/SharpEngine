using System;
using System.Numerics;
using Raylib_cs;
using SharpEngine.Math;

namespace SharpEngine.Utils.Particle;

public class Particle
{
    public Vec2 Position;
    public Vec2 Velocity;
    public Vec2 Acceleration;
    public float Lifetime;
    public float TimeSinceStart;
    public float Size;
    public float MaxSize;
    public float Rotation;
    public float RotationSpeed;
    public Color BeginColor;
    public Color CurrentColor;
    public Color EndColor;
    public ParticleParametersFunction SizeFunction;
    public float SizeFunctionValue;

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

    public void Draw()
    {
        if (Size == 0) return;

        Raylib.DrawRectanglePro(new Rectangle(Position.X, Position.Y, Size, Size), new Vector2(Size / 2, Size / 2),
            Rotation, CurrentColor);
    }
}