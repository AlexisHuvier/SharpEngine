using SharpEngine;
using SharpEngine.Math;
using SharpEngine.Widget;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        var scrollFrame = AddWidget(new ScrollFrame(new Vec2(300), new Vec2(200)));

        for (int i = 0; i < 500; i += 50)
            scrollFrame.AddChild(new Label(new Vec2(0, i), $"Test {i}", "basic"));
    }
}
