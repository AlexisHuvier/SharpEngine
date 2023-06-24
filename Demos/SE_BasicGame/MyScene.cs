using SharpEngine;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using SharpEngine.Widget;

namespace SE_BasicWindow;

internal class MyScene: Scene
{
    private Checkbox _checkbox;
    
    public MyScene()
    {
        AddWidget(new ColorRect(new Vec2(50), new Vec2(25), Color.Aqua, 15));
        AddWidget(new Frame(new Vec2(200), new Vec2(50), new Vec2(5), Color.Red));
        _checkbox = AddWidget(new Checkbox(new Vec2(400)));

        AddWidget(new TextureButton(new Vec2(600, 400), "Heyo", "basic", "KnightM", new Vec2(200, 60), Color.Red)).Clicked += OnClicked;
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        if(sender != null)
            DebugManager.Log(LogLevel.LogDebug, $"BUTTON CLICK : {_checkbox.IsChecked}");
    }
}
