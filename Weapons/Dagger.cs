using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex.Weapons
{
    class Dagger : Weapon
    {
        public Dagger()
        {
            WeaponDamage = 8;
            WeaponSpeed = 1000;
            RotationSpeed = 0;
            Scale = 0.2f;
            Rotating = false;
            Offset = new Vector2(100, 150);
            isFlipped = true;
			AddTag("Dagger");
            SpriteLocation = "sprites/weapons/machete";
        }
    }
}
