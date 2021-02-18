using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex.Scripts.Weapons
{
    public class Weapon : Entity
    {
        public float WeaponDamage { get; set; }
        public float WeaponSpeed { get; set; }
        public string SpriteLocation { get; set; }

        private Texture2D weaponSprite;
        public event Action<float> HitsObject;

        public override void Initialize(ContentManager content) {
            weaponSprite = content.Load<Texture2D>(SpriteLocation);
            Position = new Vector2(50, 50);
        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            Draw(spriteBatch, weaponSprite);
        }

        public override void Update(Time time)
        {
            
        }

        private void Collide() 
        {
            HitsObject(WeaponDamage);
        }
       
    }
}
