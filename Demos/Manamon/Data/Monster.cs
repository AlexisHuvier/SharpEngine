namespace Manamon.Data;

public struct Monster
{
    public string Name { get; }
    public int Level { get; set; }
    public int Exp { get; set; }

    public Monster(string name)
    {
        Name = name;
        Level = 0;
        Exp = 0;
    }
}