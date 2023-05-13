using Manamon.Data;
using Manamon.Data.DB;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace Manamon.Widgets;

public class FightFrame: Widget
{
    private readonly Action<List<string>> _callback;
    private Selector _attackSelector;
    private Selector _manamonSelector;

    public FightFrame(Vec2? position, List<Monster> team, int currentIndex, Action<List<string>> callback) : base(position)
    {
        _callback = callback;
        AddChild(new Frame(Vec2.Zero, new Vec2(900, 200)));
        AddChild(new Label(new Vec2(-250, 0), "Que voulez vous faire ?", "combat"));
        _attackSelector = AddChild(new Selector(new Vec2(75, -50), "basic", 0, new List<string> {""}));
        _manamonSelector = AddChild(new Selector(new Vec2(325, -50), "basic", 0, new List<string> {""}));
        AddChild(new Button(new Vec2(75, -5), "Attaque", "basic"));
        AddChild(new Button(new Vec2(325, -5), "Changer", "basic"));
        AddChild(new Button(new Vec2(75, 50), "Fuir", "basic")).Command = Escape;
        AddChild(new Button(new Vec2(325, 50), "Capturer", "basic")).Command = Catch;
        
        Init(team, currentIndex);
    }

    public void Init(List<Monster> team, int currentIndex)
    {
        var current = team[currentIndex];
        var exist = team.Where(x => x.LifePoints > 0 && x != current).Select(x => x.Data.Name).ToList();
        var spells = current.Data.Spells.Select(SortData.GetTypeById).Select(x => x.Name).ToList();
        
        if(exist.Count == 0)
            exist.Add("Aucun");
        
        RemoveChild(_attackSelector);
        RemoveChild(_manamonSelector);
        _attackSelector = AddChild(new Selector(new Vec2(75, -50), "basic", 150, spells));
        _manamonSelector = AddChild(new Selector(new Vec2(325, -50), "basic", 150, exist));
    }

    private void Escape(Button button) => _callback.Invoke(new List<string> { "escape" });
    private void Catch(Button button) => _callback.Invoke(new List<string> { "catch" });
}