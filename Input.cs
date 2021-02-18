using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    public class Input
    {
        public Vector2 mousePosition { get; private set; }

        public Vector2 getMousePosition()
        {
            //Returns the position of the mouse as a Vector2
            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            return mousePosition;
        }

        public bool getKeyState(Keys key)
        {
            //Returns if the specified keyboard key is pressed
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(key)) return true;
            else return false;
        }

        public bool getMouseButtonLeft()
        {
            //Returns if the left mousebutton is pressed
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) return true;
            else return false;
        }

        public bool getMouseButtonRight()
        {
            //Returns if the right mousebutton is pressed
            if (Mouse.GetState().RightButton == ButtonState.Pressed) return true;
            else return false;
        }
    }
}