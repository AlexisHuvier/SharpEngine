﻿using System;
using System.Collections.Generic;
using Raylib_cs;

namespace SharpEngine.Manager;

/// <summary>
/// Class which manage textures
/// </summary>
public class TextureManager
{
    private readonly Dictionary<string, Texture2D> _texture2Ds = new();

    /// <summary>
    /// Add texture to manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <param name="texture2D">Texture</param>
    public void AddTexture(string name, Texture2D texture2D)
    {
        _texture2Ds.TryAdd(name, texture2D);
    }

    /// <summary>
    /// Add texture to manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <param name="file">Texture File</param>
    public void AddTexture(string name, string file)
    {
        _texture2Ds.TryAdd(name, Raylib.LoadTexture(file));
    }

    /// <summary>
    /// Get Texture from Manager
    /// </summary>
    /// <param name="name">Texture Name</param>
    /// <returns>Texture</returns>
    /// <exception cref="Exception">Throws if texture not found</exception>
    public Texture2D GetTexture(string name)
    {
        if (_texture2Ds.TryGetValue(name, out var texture))
            return texture;
        throw new Exception($"Texture not found : {name}");
    }

    internal void Unload()
    {
        foreach (var texture in _texture2Ds.Values)
            Raylib.UnloadTexture(texture);
        _texture2Ds.Clear();
    }
}