using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SE_BasicWindow.Classes;

public class Player: SharpEngine.Entities.Entity
{
    public Player(Vec2 position, int layer)
    {
        AddComponent(new TransformComponent(position, new Vec2(3), zLayer: layer));
        AddComponent(new AnimSpriteSheetComponent("KnightM", new Vec2(16, 28), new List<Animation>
        {
            new("die", new List<uint> { 0 }, 100f),
            new("idle", new List<uint> { 1, 2 }, 250f),
            new("walk", new List<uint> { 3, 4, 5, 6, 7, 8 }, 100f)
        }, "idle"));
        AddComponent(new ControlComponent(ControlType.FourDirection, useGamePad: false));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        var control = GetComponent<ControlComponent>();
        var anim = GetComponent<AnimSpriteSheetComponent>();

        anim.FlipX = control.Direction.X switch
        {
            < 0 => true,
            > 0 => false,
            _ => anim.FlipX
        };

        if (control.IsMoving && anim.Anim == "idle")
            anim.Anim = "walk";
        else if (!control.IsMoving && anim.Anim == "walk")
            anim.Anim = "idle";
    }
}