﻿using System.Collections.Generic;
using SharpEngine.Manager;
using SharpEngine.Utils;

namespace SharpEngine.Data.Lang;

/// <summary>
/// Class which represents Lang File with Minecraft Format (allow start line comments)
/// </summary>
public class McLang: ILang
{
    private readonly Dictionary<string, string> _translations = new();
    
    /// <summary>
    /// Create MC Lang
    /// </summary>
    /// <param name="file">MC Lang Path</param>
    public McLang(string file)
    {
        Reload(file);
    }

    /// <inheritdoc />
    public string GetTranslation(string key, string defaultTranslation) =>
        _translations.GetValueOrDefault(key, defaultTranslation);

    /// <inheritdoc />
    public void Reload(string file)
    {
        _translations.Clear();
        foreach (var line in System.IO.File.ReadAllLines(file))
        {
            var finalLine = line.Trim();
            
            if(finalLine.StartsWith("#") || finalLine.Length == 0) 
                continue;

            if (!finalLine.Contains('='))
            {
                DebugManager.Log(LogLevel.LogWarning, $"SE_MCLANG: Cannot found '=' between key and value : {line} (Skip line)");
                continue;
            }
            
            _translations.Add(finalLine.Split("=")[0], finalLine.Split("=")[1]);
        }
    }
}