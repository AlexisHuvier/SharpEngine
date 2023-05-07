﻿namespace Manamon.Data.DB;

public struct MonsterData
{
    public string Name { get; }
    public string Description { get; }
    public string Race { get; }

    private MonsterData(string name, string description, string race)
    {
        Name = name;
        Description = description;
        Race = race;
    }

    private static readonly List<MonsterData> Types = new()
    {
        new MonsterData("Liwä", "Une humaine qui semble avoir plus de la vingtaine.\nElle a des yeux bleus assez clairs, la peau blanche, et ses cheveux noirs\n avec quelques mèches rouges.", "Humaine"),
        new MonsterData("Iolena", "Elle ressemble à un elfe mais avec des oreilles plus petite.\nElle est brune avec des yeux bleus ainsi qu'une tâche marron dans le gauche.", "Eternelle"),
        new MonsterData("Lilith", "Lilith semble être une femme oiseau.\nD'un taille d'un mètre 70, ses ailes lui donne une envergure de trois mètres.\nElle possède une peau blanche ainsi d'une chevelure brune.", "Femme Bête Oiseau")
    };

    public static MonsterData GetTypeByName(string name) => Types.FirstOrDefault(x => x.Name == name);
}