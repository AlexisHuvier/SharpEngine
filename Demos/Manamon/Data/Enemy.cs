using Manamon.Data.DB;

namespace Manamon.Data;

public class Enemy
{
    public readonly List<Monster> Team = new() { new Monster("Liwä") };
    public bool IsDefeat = false;
    public EnemyData Data;

    public Enemy(string data)
    {
        Data = EnemyData.GetTypeByName(data);
    }
}