using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace Manamon.Widgets;

public class CombatFrame: Widget
{
    private readonly Label _message;
    
    public CombatFrame(Vec2? position = null): base(position)
    {
        AddChild(new Frame(Vec2.Zero, new Vec2(900, 200)));
        _message = AddChild(new Label(Vec2.Zero, "", "combat"));
    }

    public void SetMessage(string msg)
    {
        _message.Text = msg;
    }
}