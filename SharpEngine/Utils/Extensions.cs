﻿using Raylib_cs;
using SharpEngine.Utils.Input;
using MouseButton = SharpEngine.Utils.Input.MouseButton;

namespace SharpEngine.Utils;

internal static class Extensions
{ 
    public static Raylib_cs.MouseButton ToRayLib(this MouseButton button) => (Raylib_cs.MouseButton)button;
    public static KeyboardKey ToRayLib(this Key key) => (KeyboardKey)key;
    public static GamepadButton ToRayLib(this GamePadButton button) => (GamepadButton)button;
    public static GamepadAxis ToRayLib(this GamePadAxis axis) => (GamepadAxis)axis;
    public static TraceLogLevel ToRayLib(this LogLevel logLevel) => (TraceLogLevel)logLevel;
    
    public static MouseButton ToSe(this Raylib_cs.MouseButton button) => (MouseButton)button;
    public static Key ToSe(this KeyboardKey key) => (Key)key;
    public static GamePadButton ToSe(this GamepadButton button) => (GamePadButton)button;
    public static GamePadAxis ToSe(this GamepadAxis axis) => (GamePadAxis)axis;
    public static LogLevel ToSe(this TraceLogLevel logLevel) => (LogLevel)logLevel;
}