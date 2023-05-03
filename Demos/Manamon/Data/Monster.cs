namespace Manamon.Data;

public struct Monster
{
    public string Name { get; }
    public int MaxLifePoints { get; set; }
    public int LifePoints { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }

    public Monster(string name)
    {
        Name = name;
        MaxLifePoints = 20;
        LifePoints = 20;
        Level = 0;
        Exp = 0;
    }
}