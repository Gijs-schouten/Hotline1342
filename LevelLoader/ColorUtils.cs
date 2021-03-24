using Microsoft.Xna.Framework;
using System;

namespace PadZex.LevelLoader
{
    public static class ColorUtils
    {
        public static Color FromHex(string hex)
        {
            byte r = Byte.Parse(hex[0..1]);
            byte g = Byte.Parse(hex[2..3]);
            byte b = Byte.Parse(hex[4..5]);
            return new Color(r, g, b);
        }
    }
}
