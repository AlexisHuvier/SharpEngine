using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion des textures
/// </summary>
public class TextureManager
{
    private readonly Window _window;
    private readonly Dictionary<string, Texture2D> _textures = new();
    private readonly Dictionary<string, string> _texturesWillBeCreated = new();

    internal TextureManager(Window window)
    {
        _window = window;
    }

    public void AddTexture(string name, Texture2D texture)
    {
        if (!_textures.ContainsKey(name))
            _textures.Add(name, texture);
    }

    public void AddTexture(string name, string file)
    {
        if (_textures.ContainsKey(name)) return;
        
        if (_window.InternalGame.GraphicsDevice != null)
            _textures.Add(name, Texture2D.FromFile(_window.InternalGame.GraphicsDevice, file));
        else
            _texturesWillBeCreated.Add(name, file);
    }

    internal void Load()
    {
        foreach (var texture in _texturesWillBeCreated)
            _textures.Add(texture.Key, Texture2D.FromFile(_window.InternalGame.GraphicsDevice, texture.Value));
    }

    public Texture2D GetTexture(string name)
    {
        if (_textures.ContainsKey(name))
            return _textures[name];
        throw new System.Exception($"Texture not founded : {name}");
    }

    internal void Unload(string name)
    {
        if (GetTexture(name) is { } texture)
            texture.Dispose();
    }

    internal void Unload()
    {
        foreach (var texture in _textures.Values)
            texture.Dispose();
        _textures.Clear();
    }
}
