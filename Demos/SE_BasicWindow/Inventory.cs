using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

public class Inventory: Widget
{
    public Inventory(): base(new Vec2(450, 300))
    {
        AddChild(new Frame(Vec2.Zero, new Vec2(800, 500), new Vec2(3), Color.Black, Color.Gray));
        AddChild(new Label(new Vec2(0, -200), "Inventory", "basic"));
        for (var i = 0; i < 5; i++)
            AddChild(new InventorySlot(new Vec2(220 + (125 * (i % 3)) - 400, 150 + (125 * (i / 3)) - 250)));
    }

    public InventorySlot GetSlot(int nb) => GetChilds<InventorySlot>()[nb];
}