using System;
using System.Collections.Generic;
using SharpEngine.Managers;

namespace SharpEngine.Utils;

public enum ParticleParametersFunction
{
    Decrease,
    Increase,
    Normal
}

public class ParticleEmitter
{
    public List<Particle> Particles = new();
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

    private List<Particle> _mustBeDeleted = new();
    private float _timerBeforeSpawn;

    public ParticleEmitter(Color[] beginColors, Color[] endColors = null, Vec2 spawnSize = null, Vec2 offset = null,
        float minVelocity = 20, float maxVelocity = 20,
        float minAcceleration = 0, float maxAcceleration = 0, float minRotationSpeed = 0, float maxRotationSpeed = 0,
        float minRotation = 0, float maxRotation = 0,
        float minLifetime = 2, float maxLifetime = 2, float minDirection = 0, float maxDirection = 0,
        float minTimerBeforeSpawn = 0.3f, float maxTimerBeforeSpawn = 0.3f,
        float minSize = 5, float maxSize = 5, int minNbParticlesPerSpawn = 4, int maxNbParticlesPerSpawn = 4,
        int maxParticles = -1, bool active = false,
        ParticleParametersFunction sizeFunction = ParticleParametersFunction.Normal, float sizeFunctionValue = 0)
    {
        BeginColors = beginColors;
        EndColors = endColors;
        Offset = offset ?? new Vec2(0);
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
        SizeFunction = sizeFunction;
        SizeFunctionValue = sizeFunctionValue;
        SpawnSize = spawnSize;
    }

    public int GetParticlesCount() => Particles.Count;

    public void SpawnParticle(Vec2 objectPosition)
    {
        Vec2 position;
        if (SpawnSize == null || SpawnSize == new Vec2(0))
            position = Offset;
        else
            position = Offset + new Vec2(Math.RandomBetween(-SpawnSize.X / 2, SpawnSize.X / 2),
                Math.RandomBetween(-SpawnSize.Y / 2, SpawnSize.Y / 2));
        var angle = Math.RandomBetween(MinDirection, MaxDirection);
        var velocity = new Vec2(MathF.Cos(Math.ToRadians(angle)), MathF.Sin(Math.ToRadians(angle))) *
                       Math.RandomBetween(MinVelocity, MaxVelocity);
        var acceleration = new Vec2(MathF.Cos(Math.ToRadians(angle)), MathF.Sin(Math.ToRadians(angle))) *
                           Math.RandomBetween(MinAcceleration, MaxAcceleration);
        var rotation = Math.RandomBetween(MinRotation, MaxRotation);
        var rotationSpeed = Math.RandomBetween(MinRotationSpeed, MaxRotationSpeed);
        var lifetime = Math.RandomBetween(MinLifetime, MaxLifetime);
        var size = Math.RandomBetween(MinSize, MaxSize);
        var beginColor = BeginColors[Math.RandomBetween(0, BeginColors.Length - 1)];
        Color endColor = null;
        if (EndColors != null)
            endColor = EndColors[Math.RandomBetween(0, EndColors.Length - 1)];

        var particle = new Particle(objectPosition + position, velocity, acceleration, lifetime, size, rotation,
            rotationSpeed, beginColor, endColor, SizeFunction, SizeFunctionValue);
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
                var nbParticles = Math.RandomBetween(MinNbParticlesPerSpawn, MaxNbParticlesPerSpawn);
                for (var i = 0; i < nbParticles; i++)
                    SpawnParticle(objectPosition);
            }

            _timerBeforeSpawn = Math.RandomBetween(MinTimerBeforeSpawn, MaxTimerBeforeSpawn);
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

    public void Update(GameTime gameTime)
    {
        Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
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

        if (EndColor != null && EndColor != BeginColor)
            CurrentColor = Color.GetColorBetween(BeginColor, EndColor, TimeSinceStart, Lifetime);

        TimeSinceStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(Window window)
    {
        if (Size == 0) return;

        var texture = window.TextureManager.GetTexture("blank");
        var size = new Vec2(Size);
        window.InternalGame.SpriteBatch.Draw(texture,
            new Rect((Position - CameraManager.Position - size / 2), size).ToMg(), null, CurrentColor.ToMg(),
            Math.ToRadians(Rotation), new Microsoft.Xna.Framework.Vector2(0),
            Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
    }
}
