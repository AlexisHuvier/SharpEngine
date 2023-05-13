using Manamon.Data;
using Manamon.Entity;
using Manamon.Widgets;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace Manamon.Scene;

public class Combat: SharpEngine.Scene
{
    private int _indexMonsterEnemy = 0;
    private int _indexMonsterPlayer = 0;
    private int _currentMessage;
    private double _timerBeforeInput;
    private Enemy _enemy = null!;
    
    public CombatFrame CombatFrame = null!;
    public FightFrame FightFrame = null!;

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        
        RemoveAllEntities();
        RemoveAllWidgets();

        AddEntity(new MonsterEntity(new Vec2(700, 200), enemy.Team[_indexMonsterEnemy])).Initialize();
        AddEntity(new MonsterEntity(new Vec2(500, 500), Manamon.Player.Team[_indexMonsterPlayer])).Initialize();

        AddWidget(new MonsterFrame(new Vec2(900, 150), enemy.Team[_indexMonsterEnemy])).Initialize();
        AddWidget(new MonsterFrame(new Vec2(300, 550), Manamon.Player.Team[_indexMonsterPlayer])).Initialize();
        
        CombatFrame = AddWidget(new CombatFrame(new Vec2(600, 750)));
        CombatFrame.Initialize();
        CombatFrame.SetMessage($"{enemy.Data.Name} lance son premier Manamon !\nIl s'agit de {enemy.Team[_indexMonsterEnemy].Data.Name} !");

        FightFrame = AddWidget(new FightFrame(new Vec2(600, 750), Manamon.Player.Team, _indexMonsterPlayer, Callback));
        FightFrame.Initialize();
        FightFrame.Displayed = false;
        
        _currentMessage = 0;
    }

    private void EnemyPlay()
    {
        
    }

    private void Callback(List<string> action)
    {
        if(action.Count == 0)
            return;
        
        CombatFrame.Displayed = true;
        FightFrame.Displayed = false;

        if (action[0] == "escape")
        {
            if (Rand.GetRandF(100) < 40)
            {
                CombatFrame.SetMessage("Vous fuiez !");
                _currentMessage = 2;
            }
            else
            {
                CombatFrame.SetMessage("L'adversaire vous retient !");
                _currentMessage = 3;
            }
        }
        else if (action[0] == "catch")
        {
            if (Rand.GetRandF(100) < 100 - _enemy.Team[_indexMonsterEnemy].LifePoints * (float)100 / _enemy.Team[_indexMonsterEnemy].MaxLifePoints)
            {
                CombatFrame.SetMessage("Vous attrapez l'enemi !");
                _currentMessage = 4;
            }
            else
            {
                CombatFrame.SetMessage("L'adversaire ne se laisse pas faire !");
                _currentMessage = 5;
            }
        }

        _timerBeforeInput = .5;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_timerBeforeInput > 0)
            _timerBeforeInput -= gameTime.ElapsedGameTime.TotalSeconds;

        if (_timerBeforeInput <= 0 && InputManager.IsMouseButtonPressed(MouseButton.Left))
        {
            if (CombatFrame.Displayed)
            {
                switch (_currentMessage)
                {
                    case 0:
                        CombatFrame.SetMessage(
                            $"Vous rétorquez alors avec votre premier Manamon...\n{Manamon.Player.Team[_indexMonsterPlayer].Data.Name} !");
                        _currentMessage = 1;
                        break;
                    case 1:
                        CombatFrame.Displayed = false;
                        FightFrame.Displayed = true;
                        break;
                    case 2:
                        GetWindow().IndexCurrentScene = 1;
                        break;
                    case 3 or 4 or 5:
                        EnemyPlay();
                        CombatFrame.Displayed = false;
                        FightFrame.Displayed = true;
                        break;
                }
            }
            
        }
    }
}