using System;
using ImGuiNET;
using Raylib_cs;

namespace SharpEngine.Manager;

public static class DebugManager
{
    public static int FrameRate => Raylib.GetFPS();
    public static long GcMemory => GC.GetTotalMemory(false);
    public const string SeVersion = "0.20.0";

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
}