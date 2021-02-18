using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PadZex
{
    public static class Input
    {
        public static Vector2 mousePosition { get; private set; }

        public static Vector2 GetMousePosition()
        {
            //Returns the position of the mouse as a Vector2
            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            return mousePosition;
        }

        public static bool KeyPressed(Keys key)
        {
            //Returns if the specified keyboard key is pressed
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(key)) return true;
            else return false;
        }

        public static bool MouseLeftPressed()
        {
            //Returns if the left mousebutton is pressed
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) return true;
            else return false;
        }

        public static bool MouseRightPressed()
        {
            //Returns if the right mousebutton is pressed
            if (Mouse.GetState().RightButton == ButtonState.Pressed) return true;
            else return false;
        }
    }
}