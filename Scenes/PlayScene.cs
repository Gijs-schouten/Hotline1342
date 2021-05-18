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
        private const int REFERENCE_WIDTH = 1920;
        
        public int EnemyCount { get; set; } = 0;
        
        private Level loadedLevel;
        private List<Entity> spawnedEntities;
        private List<Entity> protectedEntities = new();
        private readonly BackgroundMusic backgroundMusic;

        private int currentLevel = 1;
        private bool LevelLoaded { get; set; }

        public PlayScene(ContentManager content) : base(content)
        {
            Player player = new();
            AddProtectedEntityImmediate(player);
            AddProtectedEntityImmediate((backgroundMusic = new BackgroundMusic()));
            AddProtectedEntityImmediate((Camera = new Camera(CoreUtils.GraphicsDevice.Viewport)));
            AddProtectedEntityImmediate(new MouseEntity());
            
            Camera.SelectTarget("Player", this, -player.SpriteSize * player.Scale / 4);
            Camera.Zoom *= CoreUtils.ScreenSize.X / (float)REFERENCE_WIDTH;
            
            var level = LevelLoader.LevelLoader.LoadLevel(CoreUtils.GraphicsDevice, "level1");
            LoadLevel(level);
        }

        public void AddProtectedEntityImmediate(Entity entity)
        {
            protectedEntities.Add(entity);
            AddEntityImmediate(entity);
        }

        public override void Initialize()
        {
            backgroundMusic.Start();
            base.Initialize();
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
            foreach(var entity in entities)
            {
                if (protectedEntities.Contains(entity)) continue;
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
            
            currentLevel++;
            var level = LevelLoader.LevelLoader.LoadLevel(Core.CoreUtils.GraphicsDevice, "level" + currentLevel);
            LoadLevel(level);
        }
    }
}
