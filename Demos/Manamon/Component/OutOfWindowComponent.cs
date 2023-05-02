using Manamon.Entity;
using SharpEngine.Components;
using SharpEngine.Utils.Math;

namespace Manamon.Component;

public class OutOfWindowComponent : SharpEngine.Components.Component
{
    private readonly Func<int, bool> _callback;

    public OutOfWindowComponent(Func<int, bool> callback)
    {
        _callback = callback;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        var player = (PlayerEntity)GetEntity();

        var phys = player.GetComponent<PhysicsComponent>();
        var position = phys.GetPosition();
        if (position.X < player.PlayerSize.X / 2)
            position = _callback(0)
                ? new Vec2(1200 - player.PlayerSize.X / 2, position.Y)
                : new Vec2(player.PlayerSize.X / 2, position.Y);
        else if (position.X > 1200 - player.PlayerSize.X / 2)
            position = _callback(1)
                ? new Vec2(player.PlayerSize.X / 2, position.Y)
                : new Vec2(1200 - player.PlayerSize.X / 2, position.Y);
        if (position.Y < player.PlayerSize.Y / 2)
            position = _callback(2)
                ? new Vec2(position.X, 900 - player.PlayerSize.Y / 2)
                : new Vec2(position.X, player.PlayerSize.Y / 2);
        else if (position.Y > 900 - player.PlayerSize.Y / 2)
            position = _callback(3)
                ? new Vec2(position.X, player.PlayerSize.Y / 2)
                : new Vec2(position.X, 900 - player.PlayerSize.Y / 2);
        
        if(phys.GetPosition() != position)
            phys.SetPosition(position);
    }
}