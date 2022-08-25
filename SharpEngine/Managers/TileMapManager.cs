using System.Collections.Generic;
using SharpEngine.Utils.TileMap;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion des Tilemaps
/// </summary>
public class TilemapManager
{
    private readonly Window _window;
    private readonly Dictionary<string, TileMap> _tileMaps = new();

    internal TilemapManager(Window window)
    {
        _window = window;
    }
    
    public void AddMap(string name, string file)
    {
        if (!_tileMaps.ContainsKey(name))
        {
            var map = new TileMap(file, _window);
            _tileMaps.Add(name, map);
        }
    }

    public TileMap GetMap(string name)
    {
        if (_tileMaps.ContainsKey(name))
            return _tileMaps[name];
        throw new System.Exception($"TileMap not founded : {name}");
    }
}