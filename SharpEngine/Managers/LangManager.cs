using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SharpEngine.Utils;

namespace SharpEngine.Managers;

public static class LangManager
{
    private static readonly Dictionary<string, Lang> Langs = new();
    private static string _currentLang = "default";

    public static string GetCurrentLang() => _currentLang;
    public static void SetCurrentLang(string lang) => _currentLang = lang;
    public static List<string> GetLangs() => new(Langs.Keys);

    public static void AddLang(string file)
    {
        var lang = Lang.FromFile(file);
        Langs.Add(lang.Name, lang);
    }

    public static string GetTranslation(string key, string defaultTranslation)
    {
        return GetLangs().Contains(_currentLang) ? Langs[_currentLang].GetLangTranslation(key, defaultTranslation) : defaultTranslation;
    }
}
