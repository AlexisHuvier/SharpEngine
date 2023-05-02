namespace Manamon.Data.DB;

public struct EnemyData
{
    public string Name { get; set; }
    public string Description { get; set; }

    private EnemyData(string name, string description)
    {
        Name = name;
        Description = description;
    }

    private static readonly List<EnemyData> Types = new()
    {
        new EnemyData("Liwä", "Une humaine qui semble avoir plus de la vingtaine.\nElle a des yeux bleus assez clairs, la peau blanche, et ses cheveux noirs\n avec quelques mèches rouges."),
        new EnemyData("Iolena", "Elle ressemble à un elfe mais avec des oreilles plus petite.\nElle est brune avec des yeux bleus ainsi qu'une tâche marron dans le gauche."),
        new EnemyData("Lilith", "Lilith semble être une femme oiseau.\nD'un taille d'un mètre 70, ses ailes lui donne une envergure de trois mètres.\nElle possède une peau blanche ainsi d'une chevelure brune.")
    };

    public static EnemyData GetTypeByName(string name) => Types.FirstOrDefault(x => x.Name == name);
}