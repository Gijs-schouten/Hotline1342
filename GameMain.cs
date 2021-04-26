﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using PadZex.Weapons;
using PadZex.Core;
using PadZex.Scenes;

namespace PadZex
{
    public class GameMain : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private PlayScene playScene;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Core.CoreUtils.GraphicsDevice = GraphicsDevice;
            LevelLoader.LevelLoader.LoadMapDefinitions();

			CoreUtils.Point = new Point(1080, 720);
            graphics.PreferredBackBufferWidth = CoreUtils.Point.X;
            graphics.PreferredBackBufferHeight = CoreUtils.Point.Y;

            IsFixedTimeStep = false;
            IsMouseVisible = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            playScene = new Scenes.PlayScene(Content);
            playScene.SetAsMainScene();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Collision.Shape.LoadTextures(Content);
            LevelLoader.LevelLoader.LoadAssets(Content);

            var level = LevelLoader.LevelLoader.LoadLevel(GraphicsDevice, "level1");
            playScene.LoadLevel(level);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateInput();
            var time = GetTime(gameTime);
            Scene.MainScene.Update(time);
            base.Update(gameTime);           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var time = GetTime(gameTime);
            
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              Scene.MainScene.Camera.Transform);
            
            Scene.MainScene.Draw(spriteBatch, time);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
        
        private static Time GetTime(GameTime gameTime)
        {
            var time = new Time
            {
                deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds,
                timeSinceStart = (float) gameTime.TotalGameTime.TotalSeconds
            };
            return time;
        }
    }
}
