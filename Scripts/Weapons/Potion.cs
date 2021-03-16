﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex.Scripts.Weapons
{
	class Potion : Weapon
	{
		public Potion()
		{
			WeaponDamage = 5;
			WeaponSpeed = 2.5f;
			RotationSpeed = 3;
			Scale = 0.5f;
			Rotating = true;
			Offset = new Vector2(180, 30);
			AddTag("Potion");
			SpriteLocation = "sprites/weapons/potion";
			PickUp();
		}

		public override void Update(Time time)
		{
			base.Update(time);

			if (throwing && velocity > 0.5f)
			{
				Scale += time.deltaTime;
			}
			else if (throwing && velocity < 0.5f && velocity > 0)
			{
				Scale -= time.deltaTime;
			}
		}
	}
}
