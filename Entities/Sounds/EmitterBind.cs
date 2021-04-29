using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PadZex.Core;

namespace PadZex.Entities.Sounds
{
    public class EmitterBind : Core.Entity
    {
        public bool Playing { get; set; }

        private Core.Entity target;
        private SoundEffectInstance soundEffect;
        private AudioEmitter audioEmitter;
        private AudioListener audioListener;

        public EmitterBind(Core.Entity target, SoundEffect soundEffect, AudioListener audioListener)
        {
            this.target = target;
            this.audioListener = audioListener;
            audioEmitter = new AudioEmitter();
            Position = target.Position;
            audioEmitter.Position = new Vector3(Position, 0);
            
            this.soundEffect = soundEffect.CreateInstance();
        }

        public void Play()
        {
            //this.soundEffect.Apply3D(audioListener, audioEmitter);
            this.soundEffect.Play();
        }

        public override void Initialize(ContentManager content)
        {
            Play();
        }

        public override void Update(Time time)
        {
            Vector2 velocity = target.Position - Position;
            velocity.Normalize();
            Position = target.Position;
            audioEmitter.Velocity = new Vector3(velocity, 0);
            audioEmitter.Position = new Vector3(Position, 0);
            //soundEffect.Apply3D(audioListener, audioEmitter);
        }

        public override void Draw(SpriteBatch spriteBatch, Time time) { }
    }
}