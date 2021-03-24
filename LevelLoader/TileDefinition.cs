using System;
using System.Collections.Generic;

namespace PadZex.LevelLoader
{
    /// <summary>
    /// Generic data for tiles 
    /// </summary>
    internal struct TileDefinition<T> where T : Enum 
    {
        public string Path { get; }
        public IEnumerable<string> Tiles { get; }
        public T TileType { get; }

        public TileDefinition(string path, IEnumerable<string> tiles, T floorType)
        {
            Path = path;
            Tiles = tiles;
            TileType = floorType;
        }
    }
}
