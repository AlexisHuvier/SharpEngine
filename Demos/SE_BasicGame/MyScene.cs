using SharpEngine;
using SharpEngine.Math;
using SharpEngine.Utils.Widget;
using SharpEngine.Widget;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        AddWidget(new Label(new Vec2(100), "Test Multi\nLine", "basic", centerAllLines: false));
        AddWidget(new Label(new Vec2(300, 100), "Test Multi\nLine", "basic", centerAllLines: true));
        AddWidget(new Label(new Vec2(100, 500), "Test Multi\nLine", "basic", style: LabelStyle.Strike | LabelStyle.Underline, centerAllLines: false));
        AddWidget(new Label(new Vec2(300, 500), "Test Multi\nLine", "basic", style: LabelStyle.Strike | LabelStyle.Underline, centerAllLines: true));
    }
}
