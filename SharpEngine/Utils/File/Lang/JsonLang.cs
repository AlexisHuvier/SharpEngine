using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SharpEngine.Utils.File.Lang;

/// <summary>
/// Class which represents Lang File with Json
/// </summary>
public class JsonLang: ILang
{
    private Dictionary<string, string> _translations = new();

    /// <summary>
    /// Create JsonLang
    /// </summary>
    /// <param name="file">Json Path</param>
    public JsonLang(string file)
    {
        Reload(file);
    }

    /// <inheritdoc />
    public string GetTranslation(string key, string defaultTranslation) =>
        _translations.GetValueOrDefault(key, defaultTranslation);


    /// <inheritdoc />
    public void Reload(string file)
    {
        var obj = JObject.Parse(System.IO.File.ReadAllText(file));
        _translations = obj.ToObject<Dictionary<string, string>>() ?? new Dictionary<string, string>();
    }
}