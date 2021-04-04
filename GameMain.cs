using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Weapons;

using System.Collections.Generic;
using System;
using PadZex.Core;


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
            Core.CoreUtils.GraphicsDevice = GraphicsDevice;
            LevelLoader.LevelLoader.LoadMapDefinitions();

            camera = new Camera(GraphicsDevice.Viewport);
            // TODO: Add your initialization logic here
            playScene = new Scenes.PlayScene(Content);
            playScene.SetAsMainScene();
            playScene.AddEntityImmediate(new Player());
            playScene.AddEntity(new Sword());
            playScene.AddEntity(camera);
            camera.SelectTarget("Player");


            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            testScene.AddEntity(new Camera(GraphicsDevice.Viewport));

            camera.SelectTarget("Player");
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
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.Transform);
            Scene.MainScene.Draw(spriteBatch, time);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
