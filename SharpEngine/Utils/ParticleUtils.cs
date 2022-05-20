using System;
using System.Collections.Generic;

namespace SharpEngine.Utils
{
    public class ParticleUtils
    {
        public class ParticleEmitter
        {
            public List<Particle> particles = new List<Particle>();
            public Color[] colors;
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

            public int maxParticles = -1;
            public bool active;

            private List<Particle> mustBeDeleted = new List<Particle>();
            private float timerBeforeSpawn = 0;

            public ParticleEmitter(Color[] colors, Vec2 offset = null, float minVelocity = 20, float maxVelocity = 20, float minAcceleration = 0, float maxAcceleration = 0,
                float minRotationSpeed = 0, float maxRotationSpeed = 0, float minRotation = 0, float maxRotation = 0, float minLifetime = 2, float maxLifetime = 2,
                int minNbParticlesPerSpawn = 4, int maxNbParticlesPerSpawn = 4, int maxParticles = -1, bool active = false)
                float minDirection = 0, float maxDirection = 0, float minTimerBeforeSpawn = 0.3f, float maxTimerBeforeSpawn = 0.3f, float minSize = 5, float maxSize = 5,
            {
                this.colors = colors;
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
            }

            public int GetParticlesCount() => particles.Count;

            public void SpawnParticle()
            {
                Vec2 position = offset;
                float angle = Math.RandomBetween(minDirection, maxDirection);
                Vec2 velocity = new Vec2(MathF.Cos(Math.ToRadians(angle)), MathF.Sin(Math.ToRadians(angle))) * Math.RandomBetween(minVelocity, maxVelocity);
                Vec2 acceleration = new Vec2(MathF.Cos(Math.ToRadians(angle)), MathF.Sin(Math.ToRadians(angle))) * Math.RandomBetween(minAcceleration, maxAcceleration);
                float rotation = Math.RandomBetween(minRotation, maxRotation);
                float rotationSpeed = Math.RandomBetween(minRotationSpeed, maxRotationSpeed);
                float lifetime = Math.RandomBetween(minLifetime, maxLifetime);
                float size = Math.RandomBetween(minSize, maxSize);
                Color color = colors[Math.RandomBetween(0, colors.Length - 1)];

                Particle particle = new Particle(position, velocity, acceleration, lifetime, size, rotation, rotationSpeed, color);
                particles.Add(particle);
            }

            public void Update(GameTime gameTime)
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
                                SpawnParticle();
                        }
                        timerBeforeSpawn = Math.RandomBetween(minTimerBeforeSpawn, maxTimerBeforeSpawn);
                    }

                    timerBeforeSpawn -= (float)gameTime.elapsedGameTime.TotalSeconds;
                }
            }

            public void Draw(Window window, Vec2 objectPosition)
            {
                foreach (Particle particle in particles)
                    particle.Draw(window, objectPosition + offset);
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
            public float rotation;
            public float rotationSpeed;
            public Color color;

            public Particle(Vec2 position, Vec2 velocity, Vec2 acceleration, float lifetime, float size, float rotation, float rotationSpeed, Color color,
            {
                this.position = position;
                this.velocity = velocity;
                this.acceleration = acceleration;
                this.lifetime = lifetime;
                timeSinceStart = 0;
                this.size = size;
                this.rotation = rotation;
                this.rotationSpeed = rotationSpeed;
                this.color = color;
            }

            public void Update(GameTime gameTime)
            {
                velocity += acceleration * (float)gameTime.elapsedGameTime.TotalSeconds;
                position += velocity * (float)gameTime.elapsedGameTime.TotalSeconds;
                rotation += rotationSpeed * (float)gameTime.elapsedGameTime.TotalSeconds;

                timeSinceStart += (float)gameTime.elapsedGameTime.TotalSeconds;
            }

            public void Draw(Window window, Vec2 particleEmitterPosition)
            {
                var texture = window.textureManager.GetTexture("blank");
                var size = new Vec2(this.size);
                window.internalGame.spriteBatch.Draw(texture, new Rect((particleEmitterPosition + position - CameraManager.position), size).ToMG(), null, color.ToMG(), Math.ToRadians(rotation), (size / 2).ToMG(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None,  1);

            }
        }
    }
}
