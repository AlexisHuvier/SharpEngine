using System.Collections.Generic;
using SharpEngine.Utils;

namespace SharpEngine.Components
{
    public class ParticleComponent: Component
    {
        private List<ParticleUtils.ParticleEmitter> particleEmitters = new List<ParticleUtils.ParticleEmitter>();

        public ParticleComponent(): base()
        {}

        public void AddEmitter(ParticleUtils.ParticleEmitter particleEmitter) => particleEmitters.Add(particleEmitter);
        public List<ParticleUtils.ParticleEmitter> GetEmitters() => particleEmitters;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                foreach (ParticleUtils.ParticleEmitter particleEmitter in particleEmitters)
                    particleEmitter.Update(gameTime, tc.position);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (ParticleUtils.ParticleEmitter particleEmitter in particleEmitters)
                particleEmitter.Draw(GetWindow());
        }
    }
}
