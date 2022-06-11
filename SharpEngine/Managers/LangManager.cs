using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SharpEngine.Managers
{
    public class LangManager
    {
        private class Lang
        {
            public string name;
            public Dictionary<string, string> translations;

            public Lang(string name, Dictionary<string, string> translations)
            {
                this.name = name;
                this.translations = translations;
            }

            public string GetTranslation(string key, string defaultTransalation) => translations.GetValueOrDefault(key, defaultTransalation);

            public static Lang FromFile(string file) => JsonSerializer.Deserialize<Lang>(File.ReadAllText(file));
        }

        private static Dictionary<string, Lang> langs = new Dictionary<string, Lang>();
        private static string currentLang = "default";

        public static string GetCurrentLang() => currentLang;
        public static void SetCurrentLang(string lang) => currentLang = lang;

        public static List<string> GetLangs() => new List<string>(langs.Keys);

        public static void AddLang(string file)
        {
            Lang lang = Lang.FromFile(file);
            langs.Add(lang.name, lang);
        }

        public static string GetTranslation(string key, string defaultTranslation)
        {
            if(GetLangs().Contains(currentLang))
                return langs[currentLang].GetTranslation(key, defaultTranslation);
            return defaultTranslation;
        }
    }
}
