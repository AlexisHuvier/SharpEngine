using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SharpEngine.Utils;

public class Lang
{
    public string Name;
    public Dictionary<string, string> Translations;

    public Lang(string name, Dictionary<string, string> translations)
    {
        Name = name;
        Translations = translations;
    }

    public string GetLangTranslation(string key, string defaultTranslation) => Translations.GetValueOrDefault(key, defaultTranslation);

    public static Lang FromFile(string file) => JsonSerializer.Deserialize<Lang>(File.ReadAllText(file));
}