using Microsoft.Xna.Framework.Input;
using SharpEngine.Utils;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion des entrées
/// </summary>
public static class InputManager
{
    private static MouseState _oldMouseState = Mouse.GetState();
    private static KeyboardState _oldKeyboardState = Keyboard.GetState();

    public static void Update()
    {
        _oldMouseState = Mouse.GetState();
        _oldKeyboardState = Keyboard.GetState();
    }

    public static bool IsKeyDown(Key key) => Keyboard.GetState().IsKeyDown(GetKeys(key));
    public static bool IsKeyUp(Key key) => Keyboard.GetState().IsKeyUp(GetKeys(key));
    public static bool IsKeyPressed(Key key) => Keyboard.GetState().IsKeyDown(GetKeys(key)) && !_oldKeyboardState.IsKeyDown(GetKeys(key));
    public static bool IsKeyReleased(Key key) => Keyboard.GetState().IsKeyUp(GetKeys(key)) && !_oldKeyboardState.IsKeyUp(GetKeys(key));

    public static bool IsMouseButtonDown(MouseButton input, bool useOldState = false)
    {
        MouseState state;
        state = useOldState ? _oldMouseState : Mouse.GetState();

        return input switch
        {
            MouseButton.Left => state.LeftButton == ButtonState.Pressed,
            MouseButton.Middle => state.MiddleButton == ButtonState.Pressed,
            MouseButton.Right => state.RightButton == ButtonState.Pressed,
            _ => false
        };
    }

    public static bool IsMouseButtonUp(MouseButton input, bool useOldState = false) => !IsMouseButtonDown(input, useOldState);
    public static bool IsMouseButtonPressed(MouseButton input) => IsMouseButtonUp(input, true) && IsMouseButtonDown(input);
    public static bool IsMouseButtonReleased(MouseButton input) => IsMouseButtonUp(input) && IsMouseButtonDown(input, true);

    public static bool MouseInRectangle(Rect rec)
    {
        var state = Mouse.GetState();
        return rec.ToMg().Contains(state.X, state.Y);
    }

    public static bool MouseInRectangle(Vec2 position, Vec2 size) => MouseInRectangle(new Rect(position, size));
    public static int GetMouseWheelValue() => Mouse.GetState().ScrollWheelValue - _oldMouseState.ScrollWheelValue;
    public static Vec2 GetMousePosition() => Mouse.GetState().Position.ToVector2();

    private static Keys GetKeys(Key key) => (Keys)(int)key;
}
