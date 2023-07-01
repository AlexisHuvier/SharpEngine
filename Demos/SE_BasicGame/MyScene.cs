using SharpEngine;
using SharpEngine.Math;
using SharpEngine.Widget;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        AddWidget(new MultiLineInput(new Vec2(300), "", "basic"));
    }
}
