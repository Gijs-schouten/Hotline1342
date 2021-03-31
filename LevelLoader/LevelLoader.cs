using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PadZex.Collision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PadZex.LevelLoader
{
    public static class LevelLoader
    {
        public const string LEVEL_PATH = "./levels/";
        public const string PNG_POSTFIX = ".png";
        public const string ENTTITIES_POSTFIX = "_entities";

        private static Dictionary<string, Texture2D> tileAssets; 

        public static Level LoadLevel(GraphicsDevice graphicsDevice, string fileName)
        {
            string path = Path.Combine(LEVEL_PATH, fileName);

            using StreamReader streamReader = new StreamReader(path);
            Texture2D levelTexture = Texture2D.FromStream(graphicsDevice, streamReader.BaseStream);
            var tiles = ReadLevelTexture(levelTexture);
            return new Level(tiles, null);
        }

        private static IEnumerable<Tile> ReadLevelTexture(Texture2D levelTexture)
        {
            List<Tile> tiles = new List<Tile>();
            Color[] raw = new Color[levelTexture.Width * levelTexture.Height];
            levelTexture.GetData(raw);

            for (int i = 0; i < raw.Length; i++)
            {
                int x = i % levelTexture.Width;
                int y = i / levelTexture.Width;

                TileDefinition tileDefinition = MapDefinitions.GetDefinition(raw[i]);

                if (tileDefinition == null) continue;

                Shape shape = null;
                bool addShape = tileDefinition is TileDefinition<WallType>;

                // get a random texture from the tile definition
                int random = Core.CoreUtils.Random.Next(tileDefinition.Tiles.Count());
                string texturePath = tileDefinition.Tiles.ElementAt(random);
                Texture2D texture = tileAssets[texturePath];

                if(addShape)
                {
                    // only initalize the width and height right now.
                    // Owner and position will come later
                    // We select Width for the height because they have to be square
                    // (Texturs can have some extending height for added depth)
                    shape = new Collision.Rectangle(null, new Vector2(0, texture.Height - texture.Width), new Vector2(texture.Width, texture.Width));
                }

                Tile tile = new Tile(new Point(x, y), texture, shape);
                tiles.Add(tile);
            }

            return tiles;
        }

        /// <summary>
        /// Load all assets defined in the floor and wall type definition
        /// </summary>
        /// <param name="contentManager"></param>
        public static void LoadAssets(ContentManager contentManager)
        {
            tileAssets = new Dictionary<string, Texture2D>();

            void LoadDefinitionAsset<T>(IReadOnlyDictionary<Color, TileDefinition<T>> definitionAsset) where T : Enum
            {
                foreach (var definition in definitionAsset.Values)
                {
                    foreach (string file in definition.Tiles)
                    {
                        string path = Path.Combine(definition.Path, file);
                        Texture2D texture = contentManager.Load<Texture2D>(path);

                        if(!tileAssets.ContainsKey(file)) 
                            tileAssets.Add(file, texture);
                    }
                }
            }

            LoadDefinitionAsset(MapDefinitions.FloorTileDefinition);
            LoadDefinitionAsset(MapDefinitions.WallTileDefinition);
        }

        /// <summary>
        /// Load the map definitions. Do this before loading assets or the level.
        /// </summary>
        public static void LoadMapDefinitions() => MapDefinitions.LoadTiles();

        /// <summary>
        /// Retrieve a list of all the level names.
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<string> GetLevelNames()
        {
            string[] files = Directory.GetFiles(LEVEL_PATH);
            IEnumerable<string> linqFiles = files.Select(x => x.Substring(x.LastIndexOf("/") + 1, x.LastIndexOf(".") - x.LastIndexOf("/") - 1))
                                                 .Distinct();

            return linqFiles.ToList();
        }
    }
}
