using System.Collections.Generic;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Particle;

namespace SharpEngine.Components;

public class ParticleComponent: Component
{
    private readonly List<ParticleEmitter> _particleEmitters = new();
    private TransformComponent _transformComponent;

    public void AddEmitter(ParticleEmitter particleEmitter) => _particleEmitters.Add(particleEmitter);
    public List<ParticleEmitter> GetEmitters() => _particleEmitters;

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = Entity.GetComponent<TransformComponent>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_transformComponent == null) return;
        
        foreach (var particleEmitter in _particleEmitters)
            particleEmitter.Update(gameTime, _transformComponent.Position);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (_transformComponent == null) return;

        foreach (var particleEmitter in _particleEmitters)
            particleEmitter.Draw(GetWindow());
    }
}
