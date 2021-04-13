using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PadZex
{
    public static class Input
    {
        private static KeyboardState currentState;
        private static KeyboardState oldState;
        private static MouseState mouseState;

        public static Point MousePosition => mouseState.Position;
        public static bool MouseLeftPressed => mouseState.LeftButton == ButtonState.Pressed;
        public static bool MouseRightPressed => mouseState.RightButton == ButtonState.Pressed;

        public static void UpdateInput()
        {
            mouseState = Mouse.GetState();
            currentState = Keyboard.GetState();
            oldState = currentState;
        }

        /// <summary>
        /// Returns if the specified keyboard key is pressed
        /// </summary>
        /// <param name="key">Key to check if it's pressed</param>
        /// <returns>Whether the key is pressed or not</returns>
        public static bool KeyPressed(Keys key) => oldState.IsKeyDown(key);
    }
}