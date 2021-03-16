using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Collections.Generic;
using PadZex.Weapons;

namespace PadZex
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Player player;
        Camera camera;
        private Scene testScene;
        
        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            player = new Player();
            camera = new Camera(GraphicsDevice.Viewport);
            // TODO: Add your initialization logic here
            testScene = new Scene(Content);
            testScene.SetAsMainScene();
            testScene.AddEntity(new Player());
            testScene.AddEntity(new Sword());
            //testScene.AddEntity(new Dagger());
            //testScene.AddEntity(new Potion());

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
            PadZex.Collision.Shape.LoadTextures(Content);
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
            camera.Update(time);
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.Transform);
            Scene.MainScene.Draw(spriteBatch, time);
            spriteBatch.End();
            base.Draw(gameTime);
        }   
    }
}
