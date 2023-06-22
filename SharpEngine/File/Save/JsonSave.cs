using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SharpEngine.File.Save;

/// <summary>
/// Class which represents Save with json data
/// </summary>
public class JsonSave: ISave
{
    private Dictionary<string, object> _data = new();

    /// <inheritdoc />
    public void Read(string file)
    {
        var obj = JObject.Parse(System.IO.File.ReadAllText(file));
        _data = obj.ToObject<Dictionary<string, object>>() ?? new Dictionary<string, object>();
    }

    /// <inheritdoc />
    public void Write(string file) => System.IO.File.WriteAllText(file, JObject.FromObject(_data).ToString());

    /// <inheritdoc />
    public object GetObject(string key, object defaultValue) => _data.GetValueOrDefault(key, defaultValue);

    /// <inheritdoc />
    public T GetObjectAs<T>(string key, T defaultValue) =>
        _data.TryGetValue(key, out var value) ? (T)value : defaultValue;

    /// <inheritdoc />
    public void SetObject(string key, object value) => _data[key] = value;
}