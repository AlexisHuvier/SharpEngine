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
    public static long GcMemory => GC.GetTotalMemory(false);
    public const string SharpEngineVersion = "0.19.6";
    
    private static double _elapsedTime;
    private static int _frameCounter;

    public static void CreateSharpEngineImGuiWindow()
    {
        ImGui.Begin("SharpEngine Debug");
        ImGui.Text($"SharpEngine Version : {SharpEngineVersion}");
        ImGui.Separator();
        ImGui.Text($"FPS from ImGui : {1000.0/ImGui.GetIO().Framerate:.000}ms/frame ({ImGui.GetIO().Framerate} FPS)");
        ImGui.Text($"FPS from SE : {1000.0/FrameRate:.000}ms/frame ({FrameRate} FPS)");
        ImGui.Text($"GC Memory : {GcMemory/1000000.0:.000} mo");
        ImGui.End();
    }

    internal static void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime <= 1) return;
        
        _elapsedTime -= 1;
        FrameRate = _frameCounter;
        _frameCounter = 0;
    }

    internal static void Draw() => _frameCounter++;
}
