﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadZex.Weapons
{
	class Brick : Weapon
	{
		Random r = new Random();
		public Brick()
		{
			WeaponDamage = 15;
			WeaponSpeed = 2300;
			RotationSpeed = r.Next(-5, 5); ;
			Scale = 0.3f;
			Rotating = true;
			Offset = new Vector2(150, 150);
			AddTag("Brick");
			SpriteLocation = "sprites/weapons/brick";
		}
	}
}
