using System;
using System.Collections.Generic;
using Raylib_cs;

namespace SharpEngine.Manager;

public class TextureManager
{
    private readonly Dictionary<string, Texture2D> _texture2Ds = new();

    public void AddTexture(string name, Texture2D texture2D)
    {
        _texture2Ds.TryAdd(name, texture2D);
    }

    public void AddTexture(string name, string file)
    {
        _texture2Ds.TryAdd(name, Raylib.LoadTexture(file));
    }

    public Texture2D GetTexture(string name)
    {
        if (_texture2Ds.TryGetValue(name, out var texture))
            return texture;
        throw new Exception($"Texture not founded : {name}");
    }

    internal void Unload()
    {
        foreach (var texture in _texture2Ds.Values)
            Raylib.UnloadTexture(texture);
        _texture2Ds.Clear();
    }
}