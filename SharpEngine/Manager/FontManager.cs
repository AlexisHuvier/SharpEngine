﻿using System;
using System.Collections.Generic;
using Raylib_cs;

namespace SharpEngine.Manager;

/// <summary>
/// Class which manage font
/// </summary>
public class FontManager
{
    private readonly Dictionary<string, Font> _fonts = new();

    /// <summary>
    /// Add font to manager
    /// </summary>
    /// <param name="name">Font Name</param>
    /// <param name="file">Font Path</param>
    /// <param name="fontSize">Font Size (25)</param>
    public void AddFont(string name, string file, int fontSize = 25)
    {
        _fonts.TryAdd(name, Raylib.LoadFontEx(file, fontSize, null, 0));
    }

    /// <summary>
    /// Get font from manager
    /// </summary>
    /// <param name="name">Font Name</param>
    /// <returns>Font</returns>
    /// <exception cref="Exception">Throws if font not found</exception>
    public Font GetFont(string name)
    {
        if (name == "RAYLIB_DEFAULT")
            return Raylib.GetFontDefault();
        
        if (_fonts.TryGetValue(name, out var font))
            return font;
        throw new Exception($"Font not found : {name}");
    }

    internal void Unload()
    {
        foreach (var font in _fonts.Values)
            Raylib.UnloadFont(font);
        _fonts.Clear();
    }
}