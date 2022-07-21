using Microsoft.Xna.Framework;
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
    private static GamePadState[] _oldGamePadStates = {
        GamePad.GetState(PlayerIndex.One),
        GamePad.GetState(PlayerIndex.Two),
        GamePad.GetState(PlayerIndex.Three),
        GamePad.GetState(PlayerIndex.Four)
    };

    public static void Update()
    {
        _oldMouseState = Mouse.GetState();
        _oldKeyboardState = Keyboard.GetState();
        _oldGamePadStates = new[]
        {
            GamePad.GetState(PlayerIndex.One),
            GamePad.GetState(PlayerIndex.Two),
            GamePad.GetState(PlayerIndex.Three),
            GamePad.GetState(PlayerIndex.Four)
        };
    }

    public static bool IsKeyDown(Key key) => Keyboard.GetState().IsKeyDown(GetKeys(key));
    public static bool IsKeyUp(Key key) => Keyboard.GetState().IsKeyUp(GetKeys(key));
    public static bool IsKeyPressed(Key key) => Keyboard.GetState().IsKeyDown(GetKeys(key)) && !_oldKeyboardState.IsKeyDown(GetKeys(key));
    public static bool IsKeyReleased(Key key) => Keyboard.GetState().IsKeyUp(GetKeys(key)) && !_oldKeyboardState.IsKeyUp(GetKeys(key));

    public static bool IsMouseButtonDown(MouseButton input, bool useOldState = false)
    {
        var state = useOldState ? _oldMouseState : Mouse.GetState();

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

    public static bool IsGamePadConnected(GamePadIndex index) => GamePad.GetState((int)index).IsConnected;
    
    public static bool IsGamePadButtonDown(GamePadIndex index, GamePadButton button, bool useOldState = false)
    {
        var state = useOldState ? _oldGamePadStates[(int)index] : GamePad.GetState((int)index);

        return button switch
        {
            GamePadButton.A => state.Buttons.A == ButtonState.Pressed,
            GamePadButton.B => state.Buttons.B == ButtonState.Pressed,
            GamePadButton.X => state.Buttons.X == ButtonState.Pressed,
            GamePadButton.Y => state.Buttons.Y == ButtonState.Pressed,
            GamePadButton.Back => state.Buttons.Back == ButtonState.Pressed,
            GamePadButton.Start => state.Buttons.Start == ButtonState.Pressed,
            GamePadButton.BigButton => state.Buttons.BigButton == ButtonState.Pressed,
            GamePadButton.LeftShoulder => state.Buttons.LeftShoulder == ButtonState.Pressed,
            GamePadButton.LeftStick => state.Buttons.LeftStick == ButtonState.Pressed,
            GamePadButton.RightShoulder => state.Buttons.RightShoulder == ButtonState.Pressed,
            GamePadButton.RightStick => state.Buttons.RightStick == ButtonState.Pressed,
            GamePadButton.DPadUp => state.DPad.Up == ButtonState.Pressed,
            GamePadButton.DPadDown => state.DPad.Down == ButtonState.Pressed,
            GamePadButton.DPadLeft => state.DPad.Left == ButtonState.Pressed,
            GamePadButton.DPadRight => state.DPad.Right == ButtonState.Pressed,
            _ => false
        };
    }

    public static bool IsGamePadButtonUp(GamePadIndex index, GamePadButton button, bool useOldState = false) =>
        !IsGamePadButtonDown(index, button, useOldState);
    public static bool IsGamePadButtonPressed(GamePadIndex index, GamePadButton button) =>
        IsGamePadButtonUp(index, button, true) && IsGamePadButtonDown(index, button);
    public static bool IsGamePadButtonReleased(GamePadIndex index, GamePadButton button) =>
        IsGamePadButtonUp(index, button) && IsGamePadButtonDown(index, button, true);

    public static float GetGamePadTrigger(GamePadIndex index, GamePadTrigger trigger)
    {
        var state = GamePad.GetState((int)index);
        return trigger switch
        {
            GamePadTrigger.Left => state.Triggers.Left,
            GamePadTrigger.Right => state.Triggers.Right,
            _ => 0f
        };
    }

    public static float GetGamePadJoyStickAxis(GamePadIndex index, GamePadJoyStickAxis axis)
    {
        var state = GamePad.GetState((int)index);
        return axis switch
        {
            GamePadJoyStickAxis.LeftX => state.ThumbSticks.Left.X,
            GamePadJoyStickAxis.LeftY => state.ThumbSticks.Left.Y,
            GamePadJoyStickAxis.RightX => state.ThumbSticks.Right.X,
            GamePadJoyStickAxis.RightY => state.ThumbSticks.Right.Y,
            _ => 0f
        };
    }

    private static Keys GetKeys(Key key) => (Keys)(int)key;
}
