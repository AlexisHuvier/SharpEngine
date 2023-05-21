using System;
using System.Collections.Generic;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;

namespace SharpEngine.Utils;

public enum ParticleParametersFunction
{
    Decrease,
    Increase,
    Normal
}

public class ParticleEmitter
{
    public List<Particle> Particles { get; }= new();
    public Color[] BeginColors { get; set; }
    public Color[] EndColors { get; set; }
    public Vec2 Offset { get; set; }
    public float MinVelocity { get; set; }
    public float MaxVelocity { get; set; }
    public float MinAcceleration { get; set; }
    public float MaxAcceleration { get; set; }
    public float MinRotationSpeed { get; set; }
    public float MaxRotationSpeed { get; set; }
    public float MinRotation { get; set; }
    public float MaxRotation { get; set; }
    public float MinLifetime { get; set; }
    public float MaxLifetime { get; set; }
    public float MinDirection { get; set; }
    public float MaxDirection { get; set; }
    public float MinTimerBeforeSpawn { get; set; }
    public float MaxTimerBeforeSpawn { get; set; }
    public int MinNbParticlesPerSpawn { get; set; }
    public int MaxNbParticlesPerSpawn { get; set; }
    public float MinSize { get; set; }
    public float MaxSize { get; set; }
    public ParticleParametersFunction SizeFunction { get; set; }
    public float SizeFunctionValue { get; set; }
    public Vec2 SpawnSize { get; set; }

    public int MaxParticles { get; set; }
    public bool Active { get; set; }
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
            position = Offset;
        else
            position = Offset + new Vec2(MathUtils.RandomBetween(-SpawnSize.X / 2, SpawnSize.X / 2),
                MathUtils.RandomBetween(-SpawnSize.Y / 2, SpawnSize.Y / 2));
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
        Color endColor = null;
        if (EndColors != null)
            endColor = EndColors[MathUtils.RandomBetween(0, EndColors.Length - 1)];

        var particle = new Particle(objectPosition + position, velocity, acceleration, lifetime, size, rotation,
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
    public Vec2 Position { get; set; }
    public Vec2 Velocity { get; set; }
    public Vec2 Acceleration { get; set; }
    public float Lifetime { get; set; }
    public float TimeSinceStart { get; set; }
    public float Size { get; set; }
    public float MaxSize { get; set; }
    public float Rotation { get; set; }
    public float RotationSpeed { get; set; }
    public Color BeginColor { get; set; }
    public Color CurrentColor { get; set; }
    public Color EndColor { get; set; }
    public ParticleParametersFunction SizeFunction { get; set; }
    public float SizeFunctionValue { get; set; }
    public float InternalLayerDepth { get; set; }

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
        window.InternalGame.SpriteBatch.Draw(texture,
            new Rect(
                new Vec2(Position.X - CameraManager.Position.X - Size / 2,
                    Position.Y - CameraManager.Position.Y - Size / 2), Size, Size).ToMg(), null,
            CurrentColor.ToMg(), MathUtils.ToRadians(Rotation), new Microsoft.Xna.Framework.Vector2(0),
            Microsoft.Xna.Framework.Graphics.SpriteEffects.None, InternalLayerDepth);
    }
}
