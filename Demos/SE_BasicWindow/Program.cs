using ImGuiNET;
using SharpEngine;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace SE_BasicWindow;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(900, 600), Color.CornflowerBlue, debug: true)
        {
            RenderImGui = (window) =>
            {
                {
                    ImGui.Text($"SE Version : {DebugManager.GetSharpEngineVersion()}");
                    ImGui.Text($"Monogame Version : {DebugManager.GetMonogameVersion()}");
                    ImGui.Text($"FPS : {DebugManager.GetFps()}");
                    ImGui.Text($"GC Memory : {DebugManager.GetGcMemory() / 1024} ko");
                    ImGui.Separator();
                    ImGui.Text($"Connected GamePad : {InputManager.IsGamePadConnected(GamePadIndex.One)}");
                    ImGui.Text($"1A Pressed : {InputManager.IsGamePadButtonDown(GamePadIndex.One, GamePadButton.A)}");
                    ImGui.Text($"2A Pressed : {InputManager.IsGamePadButtonDown(GamePadIndex.Two, GamePadButton.A)}");
                    ImGui.Text($"Left Trigger Value : {InputManager.GetGamePadTrigger(GamePadIndex.One, GamePadTrigger.Left)}");
                    ImGui.Text($"Left X Axis Value : {InputManager.GetGamePadJoyStickAxis(GamePadIndex.One, GamePadJoyStickAxis.LeftX)}");
                    ImGui.Separator();
                    ImGui.Text($"Slider Value : {((Slider)window.GetCurrentScene().GetWidgets()[0]).Value}");
                }
            }
        };
        
        win.FontManager.AddFont("basic", "Resources/basic.ttf");

        win.AddScene(new MyScene());
        win.Run();
    }
}