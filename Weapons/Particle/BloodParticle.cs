using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using PadZex.Core;

namespace PadZex.Scripts.Particle
{
	class BloodParticle : Entity
	{
		private float particleSpeed;
		private float velocity;
		private Vector2 direction;
		private bool isTrail;
		Random r = new Random();
		private Texture2D particleSprite;
		public BloodParticle(Vector2 startPos, bool trail)
		{
			Position = startPos;
			isTrail = trail;
		}
		
		public override void Draw(SpriteBatch spriteBatch, Time time)
		{
			if (velocity > 0)
			{
				velocity -= time.deltaTime * 4;
				Position += direction * particleSpeed * velocity * time.deltaTime;

				if (!isTrail)
				{
					BloodParticle trail = new BloodParticle(Position, true);
					Scene.MainScene.AddEntity(trail);
				}

			}
			
			if (Scene.MainScene.Camera.IsInScreen(this)) {
				Draw(spriteBatch, particleSprite);
			}
			
		}

		public override void Initialize(ContentManager content)
		{
			Scale = 0.15f;
			particleSprite = content.Load<Texture2D>("sprites/weapons/blood_particle");
			Angle = r.Next(0, 1000);
			Alpha = (float)r.NextDouble();
			direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));

			if (!isTrail)
			{
				velocity = (float)r.NextDouble() + 0.2f;
				particleSpeed = r.Next(0, 800);			
				
			} else
			{
				velocity = 0.3f;
				particleSpeed = 50;
			}
		}

		public override void Update(Time time) { }
	}
}
