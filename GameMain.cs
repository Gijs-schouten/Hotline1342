﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Weapons;
using System.Collections.Generic;

namespace PadZex
{
    public class GameMain : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Player player;
        Camera camera;
        //  private List<Sprite> _sprites; 

        private Scene testScene;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            // TODO: Add your initialization logic here
            testScene = new Scene(Content);
            testScene.SetAsMainScene();
            testScene.AddEntity(new Player(Content.Load<Texture2D>("Sprites/Player"))); 
            testScene.AddEntity(new Sword());
            testScene.AddEntity(new Dagger());
            testScene.AddEntity(new Potion());
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            testScene.AddEntity(new Camera(GraphicsDevice.Viewport));

            camera.SelectTarget("Player");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateInput(); 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var time = new Time
            {
                deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds,
                timeSinceStart = (float)gameTime.TotalGameTime.TotalSeconds
            };

            Scene.MainScene.Update(time);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

			var time = new Time
            {
				deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds,
                timeSinceStart = (float)gameTime.TotalGameTime.TotalSeconds
            };

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            Scene.MainScene.Draw(spriteBatch, time);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
