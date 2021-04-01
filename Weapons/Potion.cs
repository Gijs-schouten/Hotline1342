using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PadZex.Collision;
using PadZex.Scripts.Particle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PadZex.Weapons
{
	class Potion : Weapon
	{
		private int particleAmount = 200;
		private bool exploded;
		public Potion()
		{
			WeaponDamage = 5;
			WeaponSpeed = 2.5f;
			RotationSpeed = 0f;
			Scale = 0.5f;
			Rotating = false;
			//Offset = new Vector2(180, 30);
			AddTag("Potion");
			SpriteLocation = "sprites/weapons/potion";
			PickUp();
		}

		public override void Update(Time time)
		{
			base.Update(time);
			KeyboardState state = Keyboard.GetState();

			if (throwing && velocity > 0.5f)
			{
				Scale += time.deltaTime;
			}
			else if (throwing && velocity < 0.5f && velocity > 0)
			{
				Scale -= time.deltaTime;
			}

			if (throwing && velocity <= 0 && !exploded)
			{
				Explode();
			}
		}

		private void Explode()
		{
			Alpha = 0;

			Scene.MainScene.TestCollision(new Circle(this, Vector2.Zero, 200));

			var particles = new PotionParticle[particleAmount];

			for (int i = 0; i < particleAmount; i++)
			{
				particles[i] = new PotionParticle(Position);
				Scene.MainScene.AddEntity(particles[i]);
			}

			exploded = true;
		}
	}
}
