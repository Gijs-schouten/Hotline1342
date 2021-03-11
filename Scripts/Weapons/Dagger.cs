using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex.Scripts.Weapons
{
    class Dagger : Weapon
    {
        public Dagger()
        {
            WeaponDamage = 6;
            WeaponSpeed = 3;
            RotationSpeed = 0;
            Scale = 0.2f;
            Rotating = false;
            Offset = new Vector2(100, 150);
            isFlipped = true;
            SpriteLocation = "sprites/weapons/machete";
            //PickUp();
        }
    }
}
