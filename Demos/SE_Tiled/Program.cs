﻿using SharpEngine;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SE_Tiled;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(900, 600), Color.CornflowerBlue, debug: true);

        win.TextureManager.AddTexture("sprite0", "Resources/sprite0.png");
        win.TileMapManager.AddMap("map", "Resources/map.tmx");
        win.TileMapManager.AddMap("map_tileset", "Resources/map_tileset.tmx");
        win.TileMapManager.AddMap("map_infinite", "Resources/map_infinite.tmx");
        
        win.AddScene(new MyScene());
        win.Run();
    }
}
