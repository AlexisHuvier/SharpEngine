using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SharpEngine.Utils;

namespace SharpEngine.Managers;

public static class LangManager
{
    public static string CurrentLang { get; set; } = "default";

    private static readonly Dictionary<string, Lang> Langs = new();
    public static List<string> GetLangs() => new(Langs.Keys);

    public static void AddLang(string file)
    {
        var lang = Lang.FromFile(file);
        Langs.Add(lang.Name, lang);
    }

    public static string GetTranslation(string key, string defaultTranslation)
    {
        return GetLangs().Contains(CurrentLang) ? Langs[CurrentLang].GetLangTranslation(key, defaultTranslation) : defaultTranslation;
    }
}
