using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PadZex
{
    public static class Input
    {
        private static KeyboardState currentState;
        private static KeyboardState oldState;

        public static Vector2 MousePosition => new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        public static bool MouseLeftPressed => Mouse.GetState().LeftButton == ButtonState.Pressed;
        public static bool MouseRightPressed => Mouse.GetState().RightButton == ButtonState.Pressed;

        public static void UpdateInput()
        {
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