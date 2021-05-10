using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PadZex.Collision;
using PadZex.Scripts.Particle;
using PadZex.Core;
using PadZex.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace PadZex.Weapons
{
	class Potion : Weapon
	{
		private int particleAmount = 150;
		private bool exploded;
		public Potion()
		{
			WeaponDamage = 0;
			WeaponSpeed = 1200;
			RotationSpeed = 15f;
			Scale = 0.5f;
			Rotating = true;
			Offset = new Vector2(150, 100);
			AddTag("Potion");
			SpriteLocation = "sprites/weapons/potion";
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
			WeaponDamage = 6;

			(bool collided, IEnumerable<Shape> shapes) = Scene.MainScene.TestAllCollision(new Circle(this, Vector2.Zero, 2000));

			foreach (var shape in shapes)
			{
				if (collided && shape.Owner is IDamagable damagable)
				{
					damagable.Damage(shape.Owner, WeaponDamage);
				}

			}

			var particles = new PotionParticle[particleAmount];

			for (int i = 0; i < particleAmount; i++)
			{
				particles[i] = new PotionParticle(Position);
				Scene.MainScene.AddEntity(particles[i]);
			}

			exploded = true;
			Scene.MainScene.DeleteEntity(this);
		}
	}
}
