using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PadZex.Core
{
    public static class CoreUtils
    {
        public static System.Random Random { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }

		public static Point Point { get; set; }

        static CoreUtils()
        {
            Random = new System.Random();
        }
    }
}
