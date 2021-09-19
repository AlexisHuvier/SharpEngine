﻿using Microsoft.Xna.Framework.Input;

namespace SharpEngine
{
    public class InputManager
    {
        private static MouseState oldMouseState = Mouse.GetState();
        private static KeyboardState oldKeyboardState = Keyboard.GetState();

        public static void Update()
        {
            oldMouseState = Mouse.GetState();
            oldKeyboardState = Keyboard.GetState();
        }

        public static bool IsKeyDown(Inputs.Key key)
        {
            return Keyboard.GetState().IsKeyDown(GetKeys(key));
        }

        public static bool IsKeyUp(Inputs.Key key)
        {
            return Keyboard.GetState().IsKeyUp(GetKeys(key));
        }

        public static bool IsKeyPressed(Inputs.Key key)
        {
            return Keyboard.GetState().IsKeyDown(GetKeys(key)) && !oldKeyboardState.IsKeyDown(GetKeys(key));
        }

        public static bool IsKeyReleased(Inputs.Key key)
        {
            return Keyboard.GetState().IsKeyUp(GetKeys(key)) && !oldKeyboardState.IsKeyUp(GetKeys(key));
        }

        public static bool IsMouseButtonDown(Inputs.MouseButton input, bool useOldState = false)
        {
            MouseState state;
            if (useOldState)
                state = oldMouseState;
            else
                state = Mouse.GetState();

            switch (input)
            {
                case Inputs.MouseButton.LEFT:
                    return state.LeftButton == ButtonState.Pressed;
                case Inputs.MouseButton.MIDDLE:
                    return state.MiddleButton == ButtonState.Pressed;
                case Inputs.MouseButton.RIGHT:
                    return state.RightButton == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        public static bool IsMouseButtonUp(Inputs.MouseButton input, bool useOldState = false)
        {
            return !IsMouseButtonDown(input, useOldState);
        }

        public static bool IsMouseButtonPressed(Inputs.MouseButton input)
        {
            return IsMouseButtonUp(input, true) && IsMouseButtonDown(input, false);
        }

        public static bool IsMouseButtonReleased(Inputs.MouseButton input)
        {
            return IsMouseButtonUp(input, false) && IsMouseButtonDown(input, true);
        }

        public static bool MouseInRectangle(Rect rec)
        {
            MouseState state = Mouse.GetState();
            return rec.ToMonoGameRectangle().Contains(state.X, state.Y);
        }

        public static bool MouseInRectangle(Vec2 position, Vec2 size)
        {
            return MouseInRectangle(new Rect(position, size));
        }

        public static int GetMouseWheelValue()
        {
            return Mouse.GetState().ScrollWheelValue - oldMouseState.ScrollWheelValue;
        }

        public static Vec2 GetMousePosition()
        {
            return Mouse.GetState().Position.ToVector2();
        }

        internal static Keys GetKeys(Inputs.Key key)
        {
            return (Keys)(int)key;
        }
    }
}
