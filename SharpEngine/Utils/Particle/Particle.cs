using System;
using Microsoft.Xna.Framework;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using GameTime = SharpEngine.Utils.Math.GameTime;

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
    public float InternalLayerDepth;

    public Particle(Vec2 position, Vec2 velocity, Vec2 acceleration, float lifetime, float size, float rotation,
        float rotationSpeed, Color beginColor, Color endColor, float internalLayerDepth,
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
        InternalLayerDepth = internalLayerDepth;
        SizeFunction = sizeFunction;
        SizeFunctionValue = sizeFunctionValue;
    }

    public void Update(GameTime gameTime)
    {
        Velocity = new Vec2(
            Velocity.X + Acceleration.X * (float)gameTime.ElapsedGameTime.TotalSeconds,
            Velocity.Y + Acceleration.Y * (float) gameTime.ElapsedGameTime.TotalSeconds
            );
        Position = new Vec2(
            Position.X + Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds,
            Position.Y + Velocity.Y * (float) gameTime.ElapsedGameTime.TotalSeconds
        );
        Rotation += RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            CurrentColor = Color.GetColorBetween(BeginColor, EndColor, TimeSinceStart, Lifetime);

        TimeSinceStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(Window window)
    {
        if (Size == 0) return;

        var texture = window.TextureManager.GetTexture("blank");
        window.InternalGame.SpriteBatch.Draw(texture,
            new Rectangle((int)(Position.X - CameraManager.Position.X - Size / 2),
                (int)(Position.Y - CameraManager.Position.Y - Size / 2), (int)Size, (int)Size), 
            null, CurrentColor, MathUtils.ToRadians(Rotation), Vector2.Zero,
            Microsoft.Xna.Framework.Graphics.SpriteEffects.None, InternalLayerDepth);
    }
}