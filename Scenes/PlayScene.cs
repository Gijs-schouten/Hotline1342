using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using PadZex.LevelLoader;
using PadZex.Core;
using System.Linq;
using PadZex.Entities;

namespace PadZex.Scenes
{
    public class PlayScene : Scene
    {
        private Level loadedLevel;
        private List<Entity> spawnedEntities;

        public int EnemyCount { get; set; } = 0;
        public int CurrentLevel = 1;
        public bool LevelLoaded { get; private set; }

        public PlayScene(ContentManager content) : base(content)
        {
            Player player = new Player();
            AddEntityImmediate(player);
            AddEntityImmediate(new BackgroundMusic());
            AddEntityImmediate((Camera = new Camera(CoreUtils.GraphicsDevice.Viewport)));
            AddEntityImmediate(new MouseEntity());
            
            Camera.SelectTarget("Player", this, -player.SpriteSize * player.Scale / 4);
            
            var level = LevelLoader.LevelLoader.LoadLevel(CoreUtils.GraphicsDevice, "level1");
            LoadLevel(level);
        }

        public void LoadLevel(Level level)
        {
            if(LevelLoaded)
            {
                UnloadLevel();
            }

            EnemyCount = 0;
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
                var entityTypeInstance = Activator.CreateInstance(entityType.EntityType, level, entityType.GridPosition);
                if (entityTypeInstance == null) continue;
                Entity entity = (Entity)entityTypeInstance;
                entity.Position = new Microsoft.Xna.Framework.Vector2(
                    entityType.GridPosition.X * width, 
                    entityType.GridPosition.Y * width);

                spawnedEntities.Add(entity);
                AddEntity(entity);
            }

            LevelLoaded = true;
        }

        public void ReloadLevel() => LoadLevel(loadedLevel);

        public void UnloadLevel()
        {
            foreach(var entity in spawnedEntities)
            {
                DeleteEntity(entity);
            }

            spawnedEntities.Clear();
            LevelLoaded = false;
        }

        public override void Update(Time time)
        {
            if(!HitStun.UpdateStun(time.deltaTime)) base.Update(time);
        }

        public void LoadNextLevel()
        {
            if (LevelLoaded)
            {
                UnloadLevel();
            }
            
            CurrentLevel++;
            var level = LevelLoader.LevelLoader.LoadLevel(Core.CoreUtils.GraphicsDevice, "level" + CurrentLevel);
            LoadLevel(level);
        }
    }
}
