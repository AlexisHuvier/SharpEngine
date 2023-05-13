namespace Manamon.Data.DB;

public struct MonsterData
{

    public uint Id { get; }
    public string Name { get; }
    public string Description { get; }
    public string Race { get; }
    
    public List<uint> Spells { get; }

    private MonsterData(uint id, string name, string description, string race, List<uint> spells)
    {
        Id = id;
        Name = name;
        Description = description;
        Race = race;
        Spells = spells;
    }
    
    public static bool operator ==(MonsterData lhs, MonsterData rhs) => lhs.Id == rhs.Id;
    public static bool operator !=(MonsterData lhs, MonsterData rhs) => !(lhs == rhs);
    public bool Equals(MonsterData other) => Id == other.Id;
    public override bool Equals(object? obj) => obj is MonsterData other && Equals(other);
    public override int GetHashCode() => (int)Id;

    private static readonly List<MonsterData> Types = new()
    {
        new MonsterData(1, "Liwä", "Une humaine qui semble avoir plus de la vingtaine.\nElle a des yeux bleus assez clairs, la peau blanche, et ses cheveux noirs\n avec quelques mèches rouges.", "Humaine",
            new List<uint> { 1, 2 }),
        new MonsterData(2, "Iolena", "Elle ressemble à un elfe mais avec des oreilles plus petite.\nElle est brune avec des yeux bleus ainsi qu'une tâche marron dans le gauche.", "Eternelle",
            new List<uint> { 3, 4 }),
        new MonsterData(3, "Lilith", "Lilith semble être une femme oiseau.\nD'un taille d'un mètre 70, ses ailes lui donne une envergure de trois mètres.\nElle possède une peau blanche ainsi d'une chevelure brune.", "Femme Bête Oiseau", 
            new List<uint> { 5, 6 })
    };

    public static MonsterData GetTypeByName(string name) => Types.FirstOrDefault(x => x.Name == name);
}