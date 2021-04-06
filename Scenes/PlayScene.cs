using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using PadZex.LevelLoader;
using PadZex.Core;
using System.Linq;

namespace PadZex.Scenes
{
    public class PlayScene : Scene
    {
        private Level loadedLevel;

        private List<Entity> spawnedEntities;

        private int currentLevel = 1;
        public PlayScene(ContentManager contentManager) : base(contentManager)
        {
        }

        public void LoadLevel(Level level)
        {
            loadedLevel = level;
            spawnedEntities = new List<Entity>();
            int width = level.Tiles.First().Texture.Width;

            foreach (var tile in level.Tiles)
            {
                var tileEntity = new Entities.Level.Tile(tile);
                AddEntity(tileEntity);
                spawnedEntities.Add(tileEntity);
            }

            foreach(var entityType in level.Entities)
            {
                var entityTypeInstance = Activator.CreateInstance(entityType.EntityType);
                if (entityTypeInstance == null) continue;
                Entity entity = (Entity)entityTypeInstance;
                entity.Position = new Microsoft.Xna.Framework.Vector2(
                    entityType.GridPosition.X * width, 
                    entityType.GridPosition.Y * width);

                spawnedEntities.Add(entity);
                AddEntity(entity);
            }
        }

        public void ReloadLevel() => LoadLevel(loadedLevel);

        public void UnloadLevel()
        {
            foreach(var entity in spawnedEntities)
            {
                DeleteEntity(entity);
            }

            spawnedEntities.Clear();
        }

        public void LoadNextLevel()
        {
            foreach (Entity entity in entities)
            {
                if (!(entity.Tags.Contains("Player") || entity.Tags.Contains("camera"))) DeleteEntity(entity);
            }
            currentLevel++;

            var level = LevelLoader.LevelLoader.LoadLevel(Core.CoreUtils.GraphicsDevice, "level" + currentLevel);
            LoadLevel(level);
        }
    }
}
