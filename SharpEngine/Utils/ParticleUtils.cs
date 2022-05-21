using System;
using System.Collections.Generic;

namespace SharpEngine.Utils
{
    public class ParticleUtils
    {
        public enum ParticleParametersFunction
        {
            DECREASE,
            INCREASE,
            NORMAL
        }

        public class ParticleEmitter
        {
            public List<Particle> particles = new List<Particle>();
            public Color[] beginColors;
            public Color[] endColors;
            public Vec2 offset = new Vec2(0);
            public float minVelocity = 20;
            public float maxVelocity = 20;
            public float minAcceleration = 0;
            public float maxAcceleration = 0;
            public float minRotationSpeed = 0;
            public float maxRotationSpeed = 0;
            public float minRotation = 0;
            public float maxRotation = 0;
            public float minLifetime = 2;
            public float maxLifetime = 2;
            public float minDirection = 0;
            public float maxDirection = 0;
            public float minTimerBeforeSpawn = 0.3f;
            public float maxTimerBeforeSpawn = 0.3f;
            public int minNbParticlesPerSpawn = 4;
            public int maxNbParticlesPerSpawn = 4;
            public float minSize = 5;
            public float maxSize = 5;
            public ParticleParametersFunction sizeFunction = ParticleParametersFunction.NORMAL;
            public float sizeFunctionValue = 0;
            public Vec2 spawnSize = null;

            public int maxParticles = -1;
            public bool active;

            private List<Particle> mustBeDeleted = new List<Particle>();
            private float timerBeforeSpawn = 0;

            public ParticleEmitter(Color[] beginColors, Color[] endColors = null, Vec2 spawnSize = null, Vec2 offset = null, float minVelocity = 20, float maxVelocity = 20, 
                float minAcceleration = 0, float maxAcceleration = 0, float minRotationSpeed = 0, float maxRotationSpeed = 0, float minRotation = 0, float maxRotation = 0, 
                float minLifetime = 2, float maxLifetime = 2, float minDirection = 0, float maxDirection = 0, float minTimerBeforeSpawn = 0.3f, float maxTimerBeforeSpawn = 0.3f, 
                float minSize = 5, float maxSize = 5, int minNbParticlesPerSpawn = 4, int maxNbParticlesPerSpawn = 4, int maxParticles = -1, bool active = false,
                ParticleParametersFunction sizeFunction = ParticleParametersFunction.NORMAL, float sizeFunctionValue = 0)
            {
                this.beginColors = beginColors;
                this.endColors = endColors;
                this.offset = offset ?? new Vec2(0);
                this.minVelocity = minVelocity;
                this.maxVelocity = maxVelocity;
                this.minAcceleration = minAcceleration;
                this.maxAcceleration = maxAcceleration;
                this.minRotationSpeed = minRotationSpeed;
                this.maxRotationSpeed = maxRotationSpeed;
                this.minRotation = minRotation;
                this.maxRotation = maxRotation;
                this.minLifetime = minLifetime;
                this.maxLifetime = maxLifetime;
                this.minDirection = minDirection;
                this.maxDirection = maxDirection;
                this.active = active;
                this.minTimerBeforeSpawn = minTimerBeforeSpawn;
                this.maxTimerBeforeSpawn = maxTimerBeforeSpawn;
                this.minSize = minSize;
                this.maxSize = maxSize;
                this.minNbParticlesPerSpawn = minNbParticlesPerSpawn;
                this.maxNbParticlesPerSpawn = maxNbParticlesPerSpawn;
                this.maxParticles = maxParticles;
                this.sizeFunction = sizeFunction;
                this.sizeFunctionValue = sizeFunctionValue;
                this.spawnSize = spawnSize;
            }

            public int GetParticlesCount() => particles.Count;

            public void SpawnParticle(Vec2 objectPosition)
            {
                Vec2 position;
                if (spawnSize == null || spawnSize == new Vec2(0))
                    position = offset;
                else
                    position = offset + new Vec2(Math.RandomBetween(-spawnSize.x / 2, spawnSize.x / 2), Math.RandomBetween(-spawnSize.y / 2, spawnSize.y / 2));
                float angle = Math.RandomBetween(minDirection, maxDirection);
                Vec2 velocity = new Vec2(MathF.Cos(Math.ToRadians(angle)), MathF.Sin(Math.ToRadians(angle))) * Math.RandomBetween(minVelocity, maxVelocity);
                Vec2 acceleration = new Vec2(MathF.Cos(Math.ToRadians(angle)), MathF.Sin(Math.ToRadians(angle))) * Math.RandomBetween(minAcceleration, maxAcceleration);
                float rotation = Math.RandomBetween(minRotation, maxRotation);
                float rotationSpeed = Math.RandomBetween(minRotationSpeed, maxRotationSpeed);
                float lifetime = Math.RandomBetween(minLifetime, maxLifetime);
                float size = Math.RandomBetween(minSize, maxSize);
                Color beginColor = beginColors[Math.RandomBetween(0, beginColors.Length - 1)];
                Color endColor = null;
                if(endColors != null)
                    endColor = endColors[Math.RandomBetween(0, endColors.Length - 1)];

                Particle particle = new Particle(objectPosition + position, velocity, acceleration, lifetime, size, rotation, rotationSpeed, beginColor, endColor, sizeFunction, sizeFunctionValue);
                particles.Add(particle);
            }

            public void Update(GameTime gameTime, Vec2 objectPosition)
            {
                mustBeDeleted = new List<Particle>();
                foreach (Particle particle in particles)
                {
                    particle.Update(gameTime);
                    if(particle.timeSinceStart >= particle.lifetime)
                        mustBeDeleted.Add(particle);
                }

                foreach (Particle particle in mustBeDeleted)
                    particles.Remove(particle);

                mustBeDeleted.Clear();

                if (active)
                {
                    if (timerBeforeSpawn <= 0)
                    {
                        if (maxParticles == -1 || maxParticles > particles.Count)
                        {
                            int nbParticles = Math.RandomBetween(minNbParticlesPerSpawn, maxNbParticlesPerSpawn);
                            for (int i = 0; i < nbParticles; i++)
                                SpawnParticle(objectPosition);
                        }
                        timerBeforeSpawn = Math.RandomBetween(minTimerBeforeSpawn, maxTimerBeforeSpawn);
                    }

                    timerBeforeSpawn -= (float)gameTime.elapsedGameTime.TotalSeconds;
                }
            }

            public void Draw(Window window)
            {
                foreach (Particle particle in particles)
                    particle.Draw(window);
            }
        }

        public class Particle
        {
            public Vec2 position;
            public Vec2 velocity;
            public Vec2 acceleration;
            public float lifetime;
            public float timeSinceStart;
            public float size;
            public float maxSize;
            public float rotation;
            public float rotationSpeed;
            public Color beginColor;
            public Color currentColor;
            public Color endColor;
            public ParticleParametersFunction sizeFunction = ParticleParametersFunction.NORMAL;
            public float sizeFunctionValue = 0;

            public Particle(Vec2 position, Vec2 velocity, Vec2 acceleration, float lifetime, float size, float rotation, float rotationSpeed, Color beginColor, Color endColor,
                ParticleParametersFunction sizeFunction = ParticleParametersFunction.NORMAL, float sizeFunctionValue = 0)
            {
                this.position = position;
                this.velocity = velocity;
                this.acceleration = acceleration;
                this.lifetime = lifetime;
                timeSinceStart = 0;
                this.maxSize = size;
                if (sizeFunction == ParticleParametersFunction.INCREASE)
                    this.size = 0;
                else
                    this.size = size;
                this.rotation = rotation;
                this.rotationSpeed = rotationSpeed;
                this.beginColor = beginColor;
                currentColor = beginColor;
                this.endColor = endColor;
                this.sizeFunction = sizeFunction;
                this.sizeFunctionValue = sizeFunctionValue;
            }

            public void Update(GameTime gameTime)
            {
                velocity += acceleration * (float)gameTime.elapsedGameTime.TotalSeconds;
                position += velocity * (float)gameTime.elapsedGameTime.TotalSeconds;
                rotation += rotationSpeed * (float)gameTime.elapsedGameTime.TotalSeconds;

                if(sizeFunction == ParticleParametersFunction.INCREASE)
                {
                    if (sizeFunctionValue == 0)
                        size = maxSize * timeSinceStart / lifetime;
                    else
                        size += sizeFunctionValue;
                }
                else if(sizeFunction == ParticleParametersFunction.DECREASE)
                {
                    if (sizeFunctionValue == 0)
                        size = maxSize * (lifetime - timeSinceStart) / lifetime;
                    else
                        size -= sizeFunctionValue;
                }

                if (endColor != null && endColor != beginColor)
                    currentColor = Color.GetColorBetween(beginColor, endColor, timeSinceStart, lifetime);

                timeSinceStart += (float)gameTime.elapsedGameTime.TotalSeconds;
            }

            public void Draw(Window window)
            {
                if (this.size != 0)
                {
                    var texture = window.textureManager.GetTexture("blank");
                    var size = new Vec2(this.size);
                    window.internalGame.spriteBatch.Draw(texture, new Rect((position - CameraManager.position - size / 2), size).ToMG(), null, currentColor.ToMG(), Math.ToRadians(rotation), new Microsoft.Xna.Framework.Vector2(0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
                }
            }
        }
    }
}
