using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SE_Particles;

public sealed class MyScene : Scene
{
    private Entity _ent;
    
    public MyScene()
    {
        _ent = new Entity();
        _ent.AddComponent(new TransformComponent(new Vec2(420, 300)));
        ParticleEmitter[] emitters =
        {
            new(new[] { Color.Blue, Color.Red, Color.Yellow }, minDirection: -120, maxDirection: -60, active: true,
                offset: new Vec2(-30, 0)),
            new(new[] { Color.DarkViolet }, minDirection: 0, maxDirection: 360, minSize: 20, maxSize: 20,
                sizeFunction: ParticleParametersFunction.Decrease, active: true, offset: new Vec2(20, 0)),
            new(new[] { Color.Green }, minDirection: 0, maxDirection: 360, minSize: 20, maxSize: 20,
                sizeFunction: ParticleParametersFunction.Increase, active: true, offset: new Vec2(70, 0)),
            new(new[] { Color.Red }, minDirection: -140, maxDirection: -40, endColors: new[] { Color.Yellow },
                minSize: 10, maxSize: 10, sizeFunction: ParticleParametersFunction.Decrease, active: true,
                offset: new Vec2(-80, 0)),
            new(new[] { Color.Blue }, minDirection: -90, maxDirection: -90, active: true, offset: new Vec2(120, 0),
                spawnSize: new Vec2(40))
        };
        var cmp = _ent.AddComponent(new ParticleComponent());
        foreach (var emitter in emitters)
            cmp.AddEmitter(emitter);
        AddEntity(_ent);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
            _ent.GetComponent<TransformComponent>().Position = InputManager.GetMousePosition();

    }
}