using Microsoft.Xna.Framework;
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
        Camera camera;
        BackgroundMusic music;
        private int volumeChangeCooldown;
        //  private List<Sprite> _sprites;

        private PlayScene playScene;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            volumeChangeCooldown = 0;
            Core.CoreUtils.GraphicsDevice = GraphicsDevice;
            LevelLoader.LevelLoader.LoadMapDefinitions();

            camera = new Camera(GraphicsDevice.Viewport);
            music = new BackgroundMusic();
            // TODO: Add your initialization logic here
            playScene = new Scenes.PlayScene(Content);
            playScene.SetAsMainScene(camera);
            playScene.AddEntityImmediate(camera);
            playScene.AddEntityImmediate(new Player());
            playScene.AddEntity(new Sword());
            playScene.AddEntity(camera);
            playScene.AddEntity(music);
            camera.SelectTarget("Player");
			CoreUtils.Point = new Point(1080, 720);
            graphics.PreferredBackBufferWidth = CoreUtils.Point.X;
            graphics.PreferredBackBufferHeight = CoreUtils.Point.Y;

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            playScene.AddEntity(new Camera(GraphicsDevice.Viewport));

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
            music.SongNumber = playScene.CurrentLevel;

            if (volumeChangeCooldown <= 0) //This exists so that holding the volume change for less than a second doesn't make it go from no to max volume
            {
                if (Input.KeyPressed(Keys.Up))
                { 
                    music.ChangeVolume(true);
                    volumeChangeCooldown = 60;
                }
                if (Input.KeyPressed(Keys.Down))
                { 
                    music.ChangeVolume(false);
                    volumeChangeCooldown = 60;
                }
            }
            volumeChangeCooldown--;

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
            music.Update(time);
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
