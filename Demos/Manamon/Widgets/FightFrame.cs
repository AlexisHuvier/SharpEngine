using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace Manamon.Widgets;

public class FightFrame: Widget
{
    private readonly Action<List<string>> _callback;

    public FightFrame(Vec2? position, Action<List<string>> callback) : base(position)
    {
        _callback = callback;
        AddChild(new Frame(Vec2.Zero, new Vec2(900, 200)));
        AddChild(new Label(new Vec2(-250, 0), "Que voulez vous faire ?", "combat"));
        AddChild(new Button(new Vec2(75, -35), "Attaque", "basic"));
        AddChild(new Button(new Vec2(325, -35), "Changer", "basic"));
        AddChild(new Button(new Vec2(75, 35), "Fuir", "basic")).Command = Escape;
        AddChild(new Button(new Vec2(325, 35), "Capturer", "basic")).Command = Catch;
    }

    private void Escape(Button button) => _callback.Invoke(new List<string> { "escape" });
    private void Catch(Button button) => _callback.Invoke(new List<string> { "catch" });
}