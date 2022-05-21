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
            ParticleUtils.ParticleEmitter[] emitters = new ParticleUtils.ParticleEmitter[] {
                new ParticleUtils.ParticleEmitter(new Color[] { Color.BLUE, Color.RED, Color.YELLOW }, minDirection:-120, maxDirection:-60, active: true,
                    offset: new Vec2(-30, 0)),
                new ParticleUtils.ParticleEmitter(new Color[] { Color.DARK_VIOLET }, minDirection: 0, maxDirection: 360, minSize: 20, maxSize: 20,
                    sizeFunction: ParticleUtils.ParticleParametersFunction.DECREASE, active: true, offset: new Vec2(20, 0)),
                new ParticleUtils.ParticleEmitter(new Color[] { Color.GREEN }, minDirection: 0, maxDirection: 360, minSize: 20, maxSize: 20,
                    sizeFunction: ParticleUtils.ParticleParametersFunction.INCREASE, active: true, offset: new Vec2(70, 0)),
                new ParticleUtils.ParticleEmitter(new Color[] { Color.RED }, minDirection: -140, maxDirection: -40, endColors: new Color[] { Color.YELLOW }, minSize: 10,
                    maxSize: 10, sizeFunction: ParticleUtils.ParticleParametersFunction.DECREASE, active: true, offset: new Vec2(-80, 0)),
                new ParticleUtils.ParticleEmitter(new Color[] { Color.BLUE }, minDirection: -90, maxDirection: -90, active: true, offset: new Vec2(120, 0), 
                    spawnSize: new Vec2(40))
            };
            ParticleComponent cmp = ent.AddComponent(new ParticleComponent());
            foreach (ParticleUtils.ParticleEmitter emitter in emitters)
                cmp.AddEmitter(emitter);
            ent.AddComponent(new ControlComponent());
            AddEntity(ent);
        }
    }
}