using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PadZex.LevelLoader
{
    /// <summary>
    /// A tile is a single piece in the level with a position, a texture and an optional shape.
    /// </summary>
    public struct Tile
    {
        public Point GridPosition { get; private set; }
        public Texture2D Texture { get; private set; }
        public Collision.Shape Shape { get; private set; }

        public Tile(Point point, Texture2D texture)
        {
            GridPosition = point;
            Texture = texture;
            Shape = null;
        }

        public Tile(Point point, Texture2D texture, Collision.Shape shape) : this(point, texture)
        {
            Shape = shape;
        }
    }
}
