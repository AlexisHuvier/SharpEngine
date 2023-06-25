using SharpEngine;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using SharpEngine.Widget;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        AddWidget(new ColorRect(new Vec2(50), new Vec2(25), Color.Aqua, 15));
        AddWidget(new Frame(new Vec2(200), new Vec2(50), new Vec2(5), Color.Red));
        var checkbox = AddWidget(new Checkbox(new Vec2(400)));
        AddWidget(new LineInput(new Vec2(200, 400), "BASE", "basic")).ValueChanged += (_, args) =>
            DebugManager.Log(LogLevel.LogDebug, $"Old : {args.OldValue}, New : {args.NewValue}");

        AddWidget(new TextureButton(new Vec2(600, 400), "Heyo", "basic", "KnightM", new Vec2(200, 60), Color.Red))
            .Clicked += (_, _) => DebugManager.Log(LogLevel.LogDebug, checkbox.IsChecked.ToString());

    }
}
