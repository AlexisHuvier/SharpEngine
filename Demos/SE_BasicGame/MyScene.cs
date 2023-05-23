using SE_BasicWindow.Classes;
using SharpEngine;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public readonly Player Player;
    
    public MyScene()
    {
        Player = AddEntity(new Player(new Vec2(50), 50));
        Player.PauseState = PauseState.WhenPaused;
        AddEntity(new Player(new Vec2(75), 1));
        AddWidget(new Image(new Vec2(400, 200), "KnightM", scale: new Vec2(3)));
        AddWidget(new Image(new Vec2(405, 205), "KnightM", scale: new Vec2(3)));

        AddWidget(new Image(new Vec2(400, 300), "KnightM", scale: new Vec2(3))).ZLayer = 10;
        AddWidget(new Image(new Vec2(405, 305), "KnightM", scale: new Vec2(3))).ZLayer = 0;

        AddWidget(new TestWidget(new Vec2(400, 300), 1000));
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsMouseButtonPressed(MouseButton.Left))
            Paused = !Paused;
    }
}
