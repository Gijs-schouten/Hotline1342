using System;
using System.Collections.Generic;

namespace PadZex.LevelLoader
{
    /// <summary>
    /// Generic data for tiles 
    /// </summary>
    internal class TileDefinition<T> : TileDefinition where T : Enum 
    {
        public T TileType { get; }

        public TileDefinition(string path, IEnumerable<string> tiles, T floorType) : base(path, tiles)
        {
            TileType = floorType;
        }
    }

    internal class TileDefinition
    {
        /// <summary>
        /// Base path where the sprites of the tile definition is.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Files that the tile definition contains
        /// </summary>
        public IEnumerable<string> Tiles { get; }
        
        public TileDefinition(string path, IEnumerable<string> tiles)
        {
            Path = path;
            Tiles = tiles;
        }
    }
}
