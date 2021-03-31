using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using PadZex.LevelLoader;
using PadZex.Core;

namespace PadZex.Scenes
{
    public class PlayScene : Scene
    {
        private Level loadedLevel;

        private List<Entity> spawnedTiles;

        public PlayScene(ContentManager contentManager) : base(contentManager)
        {
        }

        public void LoadLevel(Level level)
        {
            loadedLevel = level;
            spawnedTiles = new List<Entity>();

            foreach (var tile in level.Tiles)
            {
                var tileEntity = new Entities.Level.Tile(tile);
                AddEntity(tileEntity);
                spawnedTiles.Add(tileEntity);
            }
        }

        public void ReloadLevel() => LoadLevel(loadedLevel);

        public void UnloadLevel()
        {
            foreach(var entity in spawnedTiles)
            {
                DeleteEntity(entity);
            }

            spawnedTiles.Clear();
        }
    }
}
