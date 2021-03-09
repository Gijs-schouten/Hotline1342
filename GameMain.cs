using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Scripts.Weapons;

namespace PadZex
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Scene testScene;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            testScene = new Scene(Content);
            testScene.SetAsMainScene();
            testScene.AddEntity(new Player());
            testScene.AddEntity(new Sword());
            testScene.AddEntity(new Dagger());

            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
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

    public struct Time
	{
        public float deltaTime;
        public float timeSinceStart;
	}
}
