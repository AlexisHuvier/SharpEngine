using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

public class InventorySlot: Widget
{
    private Tooltip _tooltip;
    private Label _number;
    private Image _texture;
    private Frame _frame;
    
    public InventorySlot(Vec2 position) : base(position)
    {
        _frame = AddChild(new Frame( Vec2.Zero, new Vec2(100), Vec2.One, Color.Black, Color.DarkGray));
        _texture = AddChild(new Image(Vec2.Zero, ""));
        _number = AddChild(new Label(new Vec2(40), "", "basic"));
        _tooltip = AddChild(new Tooltip(Vec2.Zero));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.MouseInRectangle(new Rect(GetRealPosition().X - 50, GetRealPosition().Y - 50, 100, 100)))
        {
            _frame.BorderColor = Color.White;
            _tooltip.Displayed = true;
            _tooltip.Position = InputManager.GetMousePosition() - Position;
        }
        else
        {
            _frame.BorderColor = Color.Black;
            //_tooltip.Displayed = false;
        }
    }
}