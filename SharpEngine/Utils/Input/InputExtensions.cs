using Raylib_cs;

namespace SharpEngine.Utils.Input;

internal static class InputExtensions
{ 
    public static Raylib_cs.MouseButton ToRayLib(this MouseButton button) => (Raylib_cs.MouseButton)button;
    public static KeyboardKey ToRayLib(this Key key) => (KeyboardKey)key;
    public static GamepadButton ToRayLib(this GamePadButton button) => (GamepadButton)button;
    public static GamepadAxis ToRayLib(this GamePadAxis axis) => (GamepadAxis)axis;
    
    public static MouseButton ToSe(this Raylib_cs.MouseButton button) => (MouseButton)button;
    public static Key ToSe(this KeyboardKey key) => (Key)key;
    public static GamePadButton ToSe(this GamepadButton button) => (GamePadButton)button;
    public static GamePadAxis ToSe(this GamepadAxis axis) => (GamePadAxis)axis;
}