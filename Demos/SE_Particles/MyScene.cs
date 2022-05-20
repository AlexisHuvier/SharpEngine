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
            ParticleUtils.ParticleEmitter firstEmitter = new ParticleUtils.ParticleEmitter(new Color[] { Color.BLUE, Color.RED, Color.YELLOW }, 
                minDirection:-120, maxDirection:-60, active: true, offset: new Vec2(-30, 0));
            ParticleUtils.ParticleEmitter secondEmitter = new ParticleUtils.ParticleEmitter(new Color[] { Color.DARK_VIOLET }, minDirection: 0, maxDirection: 360,
                minSize: 20, maxSize: 20, sizeFunction: ParticleUtils.ParticleParametersFunction.DECREASE, active: true, offset: new Vec2(20, 0)); 
            ParticleUtils.ParticleEmitter thirdEmitter = new ParticleUtils.ParticleEmitter(new Color[] { Color.GREEN }, minDirection: 0, maxDirection: 360,
                minSize: 20, maxSize: 20, sizeFunction: ParticleUtils.ParticleParametersFunction.INCREASE, active: true, offset: new Vec2(70, 0));
            ParticleComponent cmp = ent.AddComponent(new ParticleComponent());
            cmp.AddEmitter(firstEmitter);
            cmp.AddEmitter(secondEmitter);
            cmp.AddEmitter(thirdEmitter);
            AddEntity(ent);
        }
    }
}