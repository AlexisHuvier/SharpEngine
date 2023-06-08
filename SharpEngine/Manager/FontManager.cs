using System;
using System.Collections.Generic;
using Raylib_cs;

namespace SharpEngine.Manager;

public class FontManager
{
    private readonly Dictionary<string, Font> _fonts = new();

    public void AddFont(string name, string file, int fontSize = 25)
    {
        _fonts.TryAdd(name, Raylib.LoadFontEx(file, fontSize, null, 0));
    }

    public Font GetFont(string name)
    {
        if (name == "RAYLIB_DEFAULT")
            return Raylib.GetFontDefault();
        
        if (_fonts.TryGetValue(name, out var font))
            return font;
        throw new Exception($"Font not founded : {name}");
    }

    internal void Unload()
    {
        foreach (var font in _fonts.Values)
            Raylib.UnloadFont(font);
        _fonts.Clear();
    }
}