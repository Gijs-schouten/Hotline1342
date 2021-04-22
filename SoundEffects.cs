using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PadZex.Core;
using System;

namespace PadZex
{
    public class SoundEffects : Entity
    {
        //Soundeffect uses position to determine what sound needs to play and if it needs to play (Y for what sound, X as a boolean)
        public List<SoundEffect> Sounds;
        public float ToPlay = 0;
        public int soundInt = 0;
        public SoundEffects() {}
        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            
        }

        public override void Initialize(ContentManager content)
        {
            AddTag("sound");
            Sounds = new List<SoundEffect>();
            Sounds.Add(content.Load<SoundEffect>("soundEffects/DoorBreak"));     //Sound 1
            Sounds.Add(content.Load<SoundEffect>("soundEffects/EnemyOuch"));     //Sound 2
            Sounds.Add(content.Load<SoundEffect>("soundEffects/MenuButton"));    //Sound 3
            Sounds.Add(content.Load<SoundEffect>("soundEffects/PlayerOuch"));    //Sound 4
            Sounds.Add(content.Load<SoundEffect>("soundEffects/PotionImpact"));  //Sound 5
            Sounds.Add(content.Load<SoundEffect>("soundEffects/Throw"));         //Sound 6
        }

        public override void Update(Time time)
        {
            ToPlay = Position.X;
            soundInt = (int)Position.Y;

            if (ToPlay > 0)
            {
                Sounds[soundInt].Play();
                Position.X = 0;
            }
        }
    }
}
