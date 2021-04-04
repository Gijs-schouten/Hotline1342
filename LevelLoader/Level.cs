using System;
using System.Collections.Generic;

namespace PadZex.LevelLoader
{
    public record Level(IEnumerable<Tile> Tiles, IEnumerable<LevelEntity> Entities);
}
