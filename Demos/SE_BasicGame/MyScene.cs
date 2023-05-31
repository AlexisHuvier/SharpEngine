using SharpEngine;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        AddWidget(new Label(new Vec2(200, 200), "Ceci est un\nTEST", "basic")).ZLayer = 1;
        AddWidget(new Label(new Vec2(400, 200), "Ceci est un\nTEST", "basic", centerAllLines: true)).ZLayer = 2;
        AddWidget(new Label(new Vec2(200, 400), "Ceci est un\nTEST\nOMG", "basic", scale: new Vec2(2))).ZLayer = 1;
        AddWidget(new Label(new Vec2(400, 400), "Ceci est un\nTEST\nOMG", "basic", scale: new Vec2(2), centerAllLines: true)).ZLayer = 2;
    }
}
