using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Scripts.Weapons
{
    class Weapon
    {
        public float WeaponDamage { get; set; }
        public float WeaponSpeed { get; set; }
        public SpriteBatch sprite { get; set; }

        public event Action<float> HitsObject;

        private void Collide() 
        {
            HitsObject(WeaponDamage);
        }
       
    }
}
