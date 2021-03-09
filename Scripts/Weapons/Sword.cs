using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex.Scripts.Weapons
{
    public class Sword : Weapon
    {
        public Sword() 
        {
            WeaponDamage = 10;
            WeaponSpeed = 3;
            SpriteLocation = "sprites/weapons/sword";
        }
    }
}
