using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow.Classes;

public class TestWidget: SharpEngine.Widgets.Widget
{
    public TestWidget(Vec2 position, int zLayer): base(position)
    {
        AddChild(new Frame(Vec2.Zero, new Vec2(200, 200), new Vec2(2), Color.Black)).ZLayer = zLayer;
    }
}