using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Utils;

namespace SE_Particles
{
    public class MyScene : Scene
    {
        public MyScene() : base()
        {
            Entity ent = new Entity();
            ent.AddComponent(new TransformComponent(new Vec2(420, 300)));
            ParticleUtils.ParticleEmitter particleEmitter = new ParticleUtils.ParticleEmitter(new Color[] { Color.BLUE, Color.RED, Color.YELLOW }, 
                minDirection:-120, maxDirection:-60, active: true);
            ent.AddComponent(new ParticleComponent()).AddEmitter(particleEmitter);
            AddEntity(ent);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.S))
                entities[0].GetComponent<ParticleComponent>().GetEmitters()[0].active = true;
            if (InputManager.IsKeyPressed(SharpEngine.Inputs.Key.D))
                entities[0].GetComponent<ParticleComponent>().GetEmitters()[0].active = false;
        }
    }
}