﻿namespace SharpEngine.Data.Lang;

/// <summary>
/// Interface which represents Lang File
/// </summary>
public interface ILang
{
    /// <summary>
    /// Get Translation for Key
    /// </summary>
    /// <param name="key">Translation Key</param>
    /// <param name="defaultTranslation">Default Translation</param>
    /// <returns>Translation or Default Translation if key not found</returns>
    public string GetTranslation(string key, string defaultTranslation);

    /// <summary>
    /// Reload Translations from File
    /// </summary>
    /// <param name="file">File Path</param>
    public void Reload(string file);
}