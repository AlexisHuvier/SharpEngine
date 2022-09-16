using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

public class Tooltip: Widget
{
    public Tooltip(Vec2 position): base(position, 1000)
    {
        AddChild(new Frame(new Vec2(100, 25), new Vec2(200, 50), new Vec2(2), Color.Black, Color.DarkGray));
    }
}