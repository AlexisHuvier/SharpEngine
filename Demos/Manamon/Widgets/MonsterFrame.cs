using Manamon.Data;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace Manamon.Widgets;

public class MonsterFrame: Widget
{
    private readonly Monster _monster;
    private readonly Label _title;
    private readonly ProgressBar _lifeBar;
    
    public MonsterFrame(Vec2? position, Monster monster): base(position)
    {
        _monster = monster;
        AddChild(new Frame(Vec2.Zero, new Vec2(300, 50)));
        _title = AddChild(new Label(Vec2.Zero, monster.Data.Name, "basic"));
        _lifeBar = AddChild(new ProgressBar(new Vec2(40, 0), Color.Green, new Vec2(180, 35)));
    }

    public override void Initialize()
    {
        base.Initialize();
        _title.Position = new Vec2(GetWindow().FontManager.GetFont("basic").MeasureString(_monster.Data.Name).X / 2 - 125, 0);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _lifeBar.Value = _monster.LifePoints * 100 / _monster.MaxLifePoints;
    }
}