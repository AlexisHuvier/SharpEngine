using System.Collections.Generic;
using Raylib_cs;
using SharpEngine.Math;
using SharpEngine.Utils.Input;
using MouseButton = SharpEngine.Utils.Input.MouseButton;

namespace SharpEngine.Manager;

public static class InputManager
{
    public static Key[] GetPressedKeys()
    {
        var keys = new List<Key>();
        var key = Raylib.GetKeyPressed();
        while (key > 0)
        {
            System.Console.WriteLine(key);
            keys.Add((Key)key);
            key = Raylib.GetKeyPressed();
        }

        return keys.ToArray();
    }
    
    public static bool IsKeyDown(Key key) => Raylib.IsKeyDown(key.ToRayLib());
    public static bool IsKeyUp(Key key) => Raylib.IsKeyUp(key.ToRayLib());
    public static bool IsKeyPressed(Key key) => Raylib.IsKeyPressed(key.ToRayLib());
    public static bool IsKeyReleased(Key key) => Raylib.IsKeyReleased(key.ToRayLib());

    public static bool IsMouseButtonDown(MouseButton button) => Raylib.IsMouseButtonDown(button.ToRayLib());
    public static bool IsMouseButtonUp(MouseButton button) => Raylib.IsMouseButtonUp(button.ToRayLib());
    public static bool IsMouseButtonPressed(MouseButton button) => Raylib.IsMouseButtonPressed(button.ToRayLib());
    public static bool IsMouseButtonReleased(MouseButton button) => Raylib.IsMouseButtonReleased(button.ToRayLib());

    public static Vec2 GetMousePosition() => Raylib.GetMousePosition();
    public static void SetMousePosition(Vec2I position) => Raylib.SetMousePosition(position.X, position.Y);
    public static float GetMouseWheelMove() => Raylib.GetMouseWheelMove();

    public static bool IsGamePadConnected(int index) => Raylib.IsGamepadAvailable(index);
    public static bool IsGamePadButtonDown(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonDown(index, button.ToRayLib());
    public static bool IsGamePadButtonUp(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonUp(index, button.ToRayLib());
    public static bool IsGamePadButtonPressed(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonPressed(index, button.ToRayLib());
    public static bool IsGamePadButtonReleased(int index, GamePadButton button) =>
        Raylib.IsGamepadButtonReleased(index, button.ToRayLib());

    public static float GetGamePadAxis(int index, GamePadAxis axis) =>
        Raylib.GetGamepadAxisMovement(index, axis.ToRayLib());
}