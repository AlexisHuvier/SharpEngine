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

    public bool Equals(Monster other) => Data.Equals(other.Data) && MaxLifePoints == other.MaxLifePoints &&
                                         LifePoints == other.LifePoints && Level == other.Level && Exp == other.Exp;
    public override bool Equals(object? obj) => obj is Monster other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Data, MaxLifePoints, LifePoints, Level, Exp);

    public static bool operator ==(Monster lhs, Monster rhs) =>
        lhs.Data == rhs.Data && lhs.MaxLifePoints == rhs.MaxLifePoints &&
        lhs.LifePoints == rhs.LifePoints && lhs.Level == rhs.Level &&
        lhs.Exp == rhs.Exp;
    public static bool operator !=(Monster lhs, Monster rhs) => !(lhs == rhs);
}