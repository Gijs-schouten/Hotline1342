using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PadZex.Core;
using PadZex.Entities;
using PadZex.Entities.MainMenu;

namespace PadZex.Scenes
{
    public class MainMenuScene : Scene
    {
        private const float SONG_VOLUME = 0.4f;
        private const int REFERENCE_WIDTH = 1920;
        private const int REFERENCE_HEIGHT = 1080;
        
        private Song song;
        
        public MainMenuScene(ContentManager content) : base(content)
        {
            var viewport = CoreUtils.GraphicsDevice.Viewport;
            Camera = new Camera(CoreUtils.GraphicsDevice.Viewport)
            {
                Zoom = (float)CoreUtils.ScreenSize.X / REFERENCE_WIDTH,
                X = 0,
                Y= 0,
            };
            Camera.AddTag("Camera");
            Camera.Update(default);
            AddEntityImmediate(Camera);
            
            song = content.Load<Song>("backgroundMusic/mainmenu1");

            SpriteEntity background = new("sprites/mainmenu/background", content)
            {
                Depth = -1,
            };
            background.Scale = (float)REFERENCE_WIDTH / background.Texture.Width;
            AddEntityImmediate(background);

            Color playNormalColor = new Color(0.0f, 1f, 0.0f);
            Color playHoverColor = new Color(0.0f, 0.5f, 0.0f);
            Button playButton = new("sprites/mainmenu/play", content, playNormalColor, playHoverColor,
                () => SceneManager.ChangeScene(SceneName.Game));
            playButton.Center();
            playButton.Position = new Vector2((float) REFERENCE_WIDTH / 2, (float) REFERENCE_HEIGHT / 2);
            AddEntityImmediate(playButton);

            Color exitNormalColor = Color.Red;
            Color exitHoverColor = new Color(0.7f, .0f, .0f);
            Button exitbutton = new("sprites/mainmenu/exit", content, exitNormalColor, exitHoverColor,
                SceneManager.Quit);
            exitbutton.Origin = new Vector2(exitbutton.Texture.Width, exitbutton.Texture.Height);
            exitbutton.Position = new Vector2(REFERENCE_WIDTH - 10, REFERENCE_HEIGHT - 10);
            exitbutton.Scale = 0.4f;
            AddEntityImmediate(exitbutton);
            
            SpriteEntity logo = new("sprites/mainmenu/logo", content);
            logo.Center();
            logo.Scale = 0.7f / Camera.Zoom;
            logo.Position = new Vector2((float)REFERENCE_WIDTH / 2, (float)logo.Texture.Height / 2 + 2f);
            AddEntityImmediate(logo);
            AddEntityImmediate(new MouseEntity() { Scale = 2.0f });
        }

        public override void Initialize()
        {
            MediaPlayer.Volume = SONG_VOLUME;
            MediaPlayer.Play(song);
            base.Initialize();
        }

        public override void Update(Time time)
        {
            base.Update(time);
        }
    }
}