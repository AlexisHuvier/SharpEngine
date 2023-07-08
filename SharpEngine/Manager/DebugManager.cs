using System;
using ImGuiNET;
using Raylib_cs;
using SharpEngine.Utils;

namespace SharpEngine.Manager;

/// <summary>
/// Static class which manage debug information
/// </summary>
public static class DebugManager
{
    /// <summary>
    /// Number of frame per seconds
    /// </summary>
    public static int FrameRate => Raylib.GetFPS();
    
    
    /// <summary>
    /// Number of bytes in GC
    /// </summary>
    public static long GcMemory => GC.GetTotalMemory(false);
    
    
    /// <summary>
    /// SharpEngine Version
    /// </summary>
    public const string SeVersion = "1.4.0";

    /// <summary>
    /// Create ImGui Window for SharpEngine
    /// </summary>
    public static void CreateSeImGuiWindow()
    {
        ImGui.Begin("SharpEngine Debug");
        ImGui.Text($"SharpEngine Version : {SeVersion}");
        ImGui.Separator();
        ImGui.Text($"FPS from ImGui : {1000.0/ImGui.GetIO().Framerate:.000}ms/frame ({ImGui.GetIO().Framerate} FPS)");
        ImGui.Text($"FPS from SE : {1000.0/FrameRate:.000}ms/frame ({FrameRate} FPS)");
        ImGui.Text($"GC Memory : {GcMemory/1000000.0:.000} mo");
        ImGui.End();
    }

    /// <summary>
    /// Log Message
    /// </summary>
    /// <param name="level">Level of Log</param>
    /// <param name="message">Message</param>
    public static void Log(LogLevel level, string message) => Raylib.TraceLog(level.ToRayLib(), message);

    /// <summary>
    /// Set Log Level
    /// </summary>
    /// <param name="level">Level of Log</param>
    public static void SetLogLevel(LogLevel level) => Raylib.SetTraceLogLevel(level.ToRayLib());
}