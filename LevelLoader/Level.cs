using System.Collections.Generic;

namespace PadZex.LevelLoader
{
    /// <summary>
    /// Contains all tiles in the level
    /// </summary>
    public struct Level
    {
        public IEnumerable<Tile> Tiles { get; private set; }
        public IEnumerable<Entity> Entities { get; private set; } 

        public Level(IEnumerable<Tile> tiles, IEnumerable<Entity> entities)
        {
            this.Tiles = tiles;
            this.Entities = entities;
        }
    }
}
