using System.Collections.Generic;
using System.IO;

namespace SharpEngine.Utils;

/// <summary>
/// Sauvegarde de données
/// </summary>
public class Save
{
    private readonly Dictionary<string, object> _data;

    public Save(Dictionary<string, object> @default)
    {
        _data = @default;
    }

    public object GetObject(string key) => _data.GetValueOrDefault(key, null);
    public T GetObjectAs<T>(string key) => _data.ContainsKey(key) ? (T) _data[key] : default;
    public void SetObject(string key, object value) => _data[key] = value;

    public void Write(string filename)
    {
        using var bW = new BinaryWriter(File.Open(filename, FileMode.Create));
        foreach (var dt in _data)
        {
            bW.Write(dt.Key);
            switch (dt.Value)
            {
                case bool v:
                    bW.Write("b");
                    bW.Write(v);
                    break;
                case int v1:
                    bW.Write("i");
                    bW.Write(v1);
                    break;
                case double v3:
                    bW.Write("d");
                    bW.Write(v3);
                    break;
                default:
                    bW.Write("s");
                    bW.Write(dt.Value.ToString() ?? string.Empty);
                    break;
            }
        }
    }

    public static Save Load(string fileName, Dictionary<string, object> @default)
    {
        var temp = new Save(@default);
        temp.Load(fileName);
        return temp;
    }

    public void Load(string fileName)
    {
        if (!File.Exists(fileName)) return;
        
        _data.Clear();
        using var bR = new BinaryReader(File.OpenRead(fileName));
        try
        {
            while (bR.ReadString() is { } key)
            {
                var type = bR.ReadString();
                object value = type switch
                {
                    "b" => bR.ReadBoolean(),
                    "i" => bR.ReadInt32(),
                    "d" => bR.ReadDouble(),
                    _ => bR.ReadString()
                };
                _data.Add(key, value);
            }
        }
        catch(EndOfStreamException)
        { }
    }
}
