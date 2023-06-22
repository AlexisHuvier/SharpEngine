using SharpEngine;
using SharpEngine.Math;
using SharpEngine.Utils;
using SharpEngine.Widget;

namespace SE_BasicWindow;

internal class MyScene: Scene
{
    public MyScene()
    {
        AddWidget(new ColorRect(new Vec2(50), new Vec2(25), Color.Aqua, 15));
        AddWidget(new Label(new Vec2(100), "Test Label", "basic", Color.Red, 10));
        AddWidget(new Label(new Vec2(200), "Test Label Multiline\nTest !", "basic", Color.Red));
        AddWidget(new Label(new Vec2(300), "Test Label Multiline\nTest !", "basic", Color.Red, 0, true));
    }
}
