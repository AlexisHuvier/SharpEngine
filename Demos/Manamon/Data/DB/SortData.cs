namespace Manamon.Data.DB;

public struct SortData
{
    public uint Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Attack { get; }
    public int AttackMagic { get; }
    public int Heal { get; }
    public int MaxUse { get; }

    private SortData(uint id, string name, string description, int attack, int attackMagic, int heal, int maxUse)
    {
        Id = id;
        Name = name;
        Description = description;
        Attack = attack;
        AttackMagic = attackMagic;
        Heal = heal;
        MaxUse = maxUse;
    }
    
    private static readonly List<SortData> Types = new()
    {
        new SortData(1, "Guérison", "Permet de soigner les blessures", 0, 0, 40, 30),
        new SortData(2, "Perforation", "Perfore un organe", 0, 100, 0, 5),
        new SortData(3, "Bille", "Forme et fait exploser une bille", 20, 100, 0, 5),
        new SortData(4, "Chute de Pierre", "Forme et fait chuter des pierres", 60, 0, 0, 20),
        new SortData(5, "Lame de Vent", "Envoie une lame de vent tranchante", 60, 0, 0, 20),
        new SortData(6, "Bourrasque Explosive", "Provoque une compression d'air relaché d'un coup.", 20, 60, 0, 10)
    };

    public static SortData GetTypeById(uint id) => Types.FirstOrDefault(x => x.Id == id);
}