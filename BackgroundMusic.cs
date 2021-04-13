using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using PadZex.Core;
using Microsoft.Xna.Framework.Graphics;

namespace PadZex
{
    public class BackgroundMusic : Entity
    {
        private Song[] songs = new Song[3];
        public int SongNumber = 0;
        private int currentSong;
        public BackgroundMusic()
        {
            AddTag("backgroundMusic");
        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            
        }

        public override void Initialize(ContentManager content)
        {
            currentSong = SongNumber;
            this.songs[0] = content.Load<Song>("backgroundMusic/DoomsGate");
            this.songs[1] = content.Load<Song>("backgroundMusic/TheyFear");
            this.songs[2] = content.Load<Song>("backgroundMusic/BFGDivision");
            MediaPlayer.Play(songs[SongNumber]);
            MediaPlayer.IsRepeating = true;
        }

        public override void Update(Time time)
        {
            
            if (currentSong != SongNumber)
            {
                if (SongNumber < songs.Length - 1)
                {
                    currentSong = SongNumber;
                    MediaPlayer.Play(songs[SongNumber]);
                    MediaPlayer.IsRepeating = true;
                }
                else
                {
                    Console.WriteLine("ERROR 404: song " + SongNumber + " not found");
                }
            }          
        }
    }
}
