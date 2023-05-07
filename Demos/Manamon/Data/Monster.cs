using Manamon.Data.DB;

namespace Manamon.Data;

public struct Monster
{
    public MonsterData Data { get; }
    public int MaxLifePoints { get; set; }
    public int LifePoints { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }

    public Monster(string data)
    {
        Data = MonsterData.GetTypeByName(data);
        MaxLifePoints = 20;
        LifePoints = 20;
        Level = 0;
        Exp = 0;
    }
}