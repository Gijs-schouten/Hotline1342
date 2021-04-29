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

        public EmitterBind(Core.Entity target, SoundEffect soundEffect, AudioListener audioListener)
        {
            this.target = target;
            audioEmitter = new AudioEmitter();
            
            this.soundEffect = soundEffect.CreateInstance();
            this.soundEffect.Apply3D(audioListener, audioEmitter);
        }

        public void Play()
        {
            this.soundEffect.Play();
        }

        public override void Initialize(ContentManager content)
        {
            Play();
        }

        public override void Update(Time time)
        {
            Vector2 velocity = target.Position - Position;
            Position = target.Position;
            audioEmitter.Velocity = new Vector3(velocity, 0);
            audioEmitter.Position = new Vector3(Position, 0);
        }

        public override void Draw(SpriteBatch spriteBatch, Time time) { }
    }
}