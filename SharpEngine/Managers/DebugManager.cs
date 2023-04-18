using System;
using ImGuiNET;
using SharpEngine.Utils.Math;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion des informations Debug
/// </summary>
public static class DebugManager
{
    public static int FrameRate { get; private set; }
    public static string MonoGameVersion => System.Diagnostics.FileVersionInfo
        .GetVersionInfo(typeof(Microsoft.Xna.Framework.Game).Assembly.Location).FileVersion;
    public static long GcMemory => GC.GetTotalMemory(false);
    public static string SharpEngineVersion => "0.17.0";
    
    
    private static TimeSpan _elapsedTime = TimeSpan.Zero;
    private static int _frameCounter;


    internal static void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime;

        if (_elapsedTime <= TimeSpan.FromSeconds(1)) return;
        
        _elapsedTime -= TimeSpan.FromSeconds(1);
        FrameRate = _frameCounter;
        _frameCounter = 0;
    }

    internal static void Draw() => _frameCounter++;
}
