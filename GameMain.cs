using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Collections.Generic;
using PadZex.Scripts.Weapons;

namespace PadZex
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Player player;
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
            // TODO: Add your initialization logic here
            testScene = new Scene(Content);
            testScene.SetAsMainScene();
            testScene.AddEntity(new Player());
            testScene.AddEntity(new Sword());
            testScene.Camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateInput();
            if (Input.KeyPressed(Keys.W))
                Scene.MainScene.Camera.Zoom += 0.01f;
            else if (Input.KeyPressed(Keys.S))
                Scene.MainScene.Camera.Zoom -= 0.01f;
            if (Input.KeyPressed(Keys.A))
                Scene.MainScene.Camera.Rotation += 0.01f;
            else if (Input.KeyPressed(Keys.D))
                Scene.MainScene.Camera.Rotation -= 0.01f;
            Scene.MainScene.Camera.Update(player.Position);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here

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
            
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              Scene.MainScene.Camera.Transform);
            Scene.MainScene.Draw(spriteBatch, time);
            spriteBatch.End();
            base.Draw(gameTime);
        }   
    }
}
