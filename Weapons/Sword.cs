using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex.Weapons
{
    public class Sword : Weapon
    {
        public Sword() 
        {
            WeaponDamage = 10;
            WeaponSpeed = 3500;
            RotationSpeed = 35;
            Scale = 0.5f;
            Rotating = true;
            Offset = new Vector2(180,30);
            AddTag("Sword");
            SpriteLocation = "sprites/weapons/sword";
        }
    }
}
