using System.Collections.Generic;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Components;

public class ParticleComponent: Component
{
    private List<ParticleEmitter> _particleEmitters = new();

    public void AddEmitter(ParticleEmitter particleEmitter) => _particleEmitters.Add(particleEmitter);
    public List<ParticleEmitter> GetEmitters() => _particleEmitters;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Entity.GetComponent<TransformComponent>() is not { } tc) return;
        
        foreach (var particleEmitter in _particleEmitters)
            particleEmitter.Update(gameTime, tc.Position);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        foreach (var particleEmitter in _particleEmitters)
            particleEmitter.Draw(GetWindow());
    }
}
