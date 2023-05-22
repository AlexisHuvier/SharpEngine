using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Utils;

public enum ParticleParametersFunction
{
    Decrease,
    Increase,
    Normal
}

public class ParticleEmitter
{
    public readonly List<Particle> Particles = new();
    public Color[] BeginColors;
    public Color[] EndColors;
    public Vec2 Offset;
    public float MinVelocity;
    public float MaxVelocity;
    public float MinAcceleration;
    public float MaxAcceleration;
    public float MinRotationSpeed;
    public float MaxRotationSpeed;
    public float MinRotation;
    public float MaxRotation;
    public float MinLifetime;
    public float MaxLifetime;
    public float MinDirection;
    public float MaxDirection;
    public float MinTimerBeforeSpawn;
    public float MaxTimerBeforeSpawn;
    public int MinNbParticlesPerSpawn;
    public int MaxNbParticlesPerSpawn;
    public float MinSize;
    public float MaxSize;
    public ParticleParametersFunction SizeFunction;
    public float SizeFunctionValue;
    public Vec2 SpawnSize;

    public int MaxParticles;
    public bool Active;
    public int ZLayer { 
        get => (int)(_internalLayerDepth * 4096);
        set => _internalLayerDepth = value / 4096f;
    }

    private List<Particle> _mustBeDeleted = new();
    private float _timerBeforeSpawn;
    private float _internalLayerDepth;

    public ParticleEmitter(Color[] beginColors, Color[] endColors = null, Vec2? spawnSize = null, Vec2? offset = null,
        float minVelocity = 20, float maxVelocity = 20,
        float minAcceleration = 0, float maxAcceleration = 0, float minRotationSpeed = 0, float maxRotationSpeed = 0,
        float minRotation = 0, float maxRotation = 0,
        float minLifetime = 2, float maxLifetime = 2, float minDirection = 0, float maxDirection = 0,
        float minTimerBeforeSpawn = 0.3f, float maxTimerBeforeSpawn = 0.3f,
        float minSize = 5, float maxSize = 5, int minNbParticlesPerSpawn = 4, int maxNbParticlesPerSpawn = 4,
        int maxParticles = -1, bool active = false, int zLayer = 4096,
        ParticleParametersFunction sizeFunction = ParticleParametersFunction.Normal, float sizeFunctionValue = 0)
    {
        BeginColors = beginColors;
        EndColors = endColors;
        Offset = offset ?? Vec2.Zero;
        MinVelocity = minVelocity;
        MaxVelocity = maxVelocity;
        MinAcceleration = minAcceleration;
        MaxAcceleration = maxAcceleration;
        MinRotationSpeed = minRotationSpeed;
        MaxRotationSpeed = maxRotationSpeed;
        MinRotation = minRotation;
        MaxRotation = maxRotation;
        MinLifetime = minLifetime;
        MaxLifetime = maxLifetime;
        MinDirection = minDirection;
        MaxDirection = maxDirection;
        Active = active;
        MinTimerBeforeSpawn = minTimerBeforeSpawn;
        MaxTimerBeforeSpawn = maxTimerBeforeSpawn;
        MinSize = minSize;
        MaxSize = maxSize;
        MinNbParticlesPerSpawn = minNbParticlesPerSpawn;
        MaxNbParticlesPerSpawn = maxNbParticlesPerSpawn;
        MaxParticles = maxParticles;
        ZLayer = zLayer;
        SizeFunction = sizeFunction;
        SizeFunctionValue = sizeFunctionValue;
        SpawnSize = spawnSize ?? Vec2.Zero;
    }

    public int GetParticlesCount() => Particles.Count;

    public void SpawnParticle(Vec2 objectPosition)
    {
        Vec2 position;
        if (SpawnSize == Vec2.Zero)
            position = new Vec2(Offset.X + objectPosition.X, Offset.Y + objectPosition.Y);
        else
            position = new Vec2(
                MathUtils.RandomBetween(-SpawnSize.X / 2, SpawnSize.X / 2) + Offset.X + objectPosition.X,
                MathUtils.RandomBetween(-SpawnSize.Y / 2, SpawnSize.Y / 2) + Offset.Y + objectPosition.Y);
        var angle = MathUtils.RandomBetween(MinDirection, MaxDirection);
        var velocity = new Vec2(MathF.Cos(MathUtils.ToRadians(angle)), MathF.Sin(MathUtils.ToRadians(angle))) *
                       MathUtils.RandomBetween(MinVelocity, MaxVelocity);
        var acceleration = new Vec2(MathF.Cos(MathUtils.ToRadians(angle)), MathF.Sin(MathUtils.ToRadians(angle))) *
                           MathUtils.RandomBetween(MinAcceleration, MaxAcceleration);
        var rotation = MathUtils.RandomBetween(MinRotation, MaxRotation);
        var rotationSpeed = MathUtils.RandomBetween(MinRotationSpeed, MaxRotationSpeed);
        var lifetime = MathUtils.RandomBetween(MinLifetime, MaxLifetime);
        var size = MathUtils.RandomBetween(MinSize, MaxSize);
        var beginColor = BeginColors[MathUtils.RandomBetween(0, BeginColors.Length - 1)];
        var endColor = beginColor;
        if (EndColors != null)
            endColor = EndColors[MathUtils.RandomBetween(0, EndColors.Length - 1)];

        var particle = new Particle(position, velocity, acceleration, lifetime, size, rotation,
            rotationSpeed, beginColor, endColor, _internalLayerDepth, SizeFunction, SizeFunctionValue);
        Particles.Add(particle);
    }

    public void Update(GameTime gameTime, Vec2 objectPosition)
    {
        _mustBeDeleted = new List<Particle>();
        foreach (var particle in Particles)
        {
            particle.Update(gameTime);
            if (particle.TimeSinceStart >= particle.Lifetime)
                _mustBeDeleted.Add(particle);
        }

        foreach (var particle in _mustBeDeleted)
            Particles.Remove(particle);

        _mustBeDeleted.Clear();

        if (!Active) return;

        if (_timerBeforeSpawn <= 0)
        {
            if (MaxParticles == -1 || MaxParticles > Particles.Count)
            {
                var nbParticles = MathUtils.RandomBetween(MinNbParticlesPerSpawn, MaxNbParticlesPerSpawn);
                for (var i = 0; i < nbParticles; i++)
                    SpawnParticle(objectPosition);
            }

            _timerBeforeSpawn = MathUtils.RandomBetween(MinTimerBeforeSpawn, MaxTimerBeforeSpawn);
        }

        _timerBeforeSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(Window window)
    {
        foreach (var particle in Particles)
            particle.Draw(window);
    }
}

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
                (int)(Position.Y - CameraManager.Position.Y - Size / 2), (int)Size, (int)Size), null,
            CurrentColor.ToMg(), MathUtils.ToRadians(Rotation), Vector2.Zero,
            Microsoft.Xna.Framework.Graphics.SpriteEffects.None, InternalLayerDepth);
    }
}
