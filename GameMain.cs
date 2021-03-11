using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace PadZex
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Scene testScene;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            testScene = new Scene(Content);
            testScene.SetAsMainScene();
            testScene.AddEntity(new Player());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Input.KeyPressed(Keys.A)) Console.WriteLine("Pressed A");
            
            Time time = new Time();
            time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            time.timeSinceStart = (float)gameTime.TotalGameTime.TotalSeconds;

            Scene.MainScene.Update(time);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Time time = new Time();
            time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            time.timeSinceStart = (float)gameTime.TotalGameTime.TotalSeconds;

            spriteBatch.Begin();
            Scene.MainScene.Draw(spriteBatch, time);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
