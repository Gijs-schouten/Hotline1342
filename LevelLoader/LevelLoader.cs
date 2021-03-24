using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PadZex.LevelLoader
{
    public class LevelLoader
    {
        public const string LEVEL_PATH = "./levels/";

        public static Level LoadLevel(string fileName)
        {
            throw new NotImplementedException();
        }

        public static IReadOnlyList<string> GetLevelNames()
        {
            string[] files = Directory.GetFiles(LEVEL_PATH);
            IEnumerable<string> linqFiles = files.Select(x => x.Substring(x.LastIndexOf("/") + 1, x.LastIndexOf(".") - x.LastIndexOf("/") - 1))
                                                 .Distinct();

            return linqFiles.ToList();
        }
    }

    internal class MapDefinitions
    {
        private const string DEFINITION_FILE = "mapdefinitions";

        private static Dictionary<Color, TileDefinition<FloorType>> floorTileDefinitions;
        private static Dictionary<Color, TileDefinition<WallType>> wallTileDefinitions;

        public static void LoadTiles()
        {
            floorTileDefinitions = new Dictionary<Color, TileDefinition<FloorType>>();
            wallTileDefinitions = new Dictionary<Color, TileDefinition<WallType>>();

            string[] definitions = File.ReadAllLines(Path.Combine(LevelLoader.LEVEL_PATH, DEFINITION_FILE));

            foreach(string definition in definitions)
            {
                LoadDefinition(definition);
            }
        }

        private static void LoadDefinition(string definition)
        {
            string[] split = definition.Split(" ");
            Enum t = GetDefinitionType(split[0]);
            Color color = ColorUtils.FromHex(split[1]);
        }

        private static Enum GetDefinitionType(string enumType)
        {
            string[] split = enumType.Split('.');

        }
    }
}
