﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using PadZex.Core;

namespace PadZex.Scripts.Particle
{
	class PotionParticle : Entity
	{
		private float particleSpeed;
		private float velocity;
		private Vector2 direction;
		Random r = new Random();
		private Texture2D particleSprite;
		public PotionParticle(Vector2 startPos)
		{
			Position = startPos;
		}
		public override void Draw(SpriteBatch spriteBatch, Time time)
		{
			Draw(spriteBatch, particleSprite);
		}

		public override void Initialize(ContentManager content)
		{
			particleSprite = content.Load<Texture2D>("sprites/weapons/potion_effect");
			velocity = (float)r.NextDouble() + 0.2f;
			Angle = r.Next(0, 1000);
			particleSpeed = r.Next(200, 600);
			Alpha = (float)r.NextDouble();
			direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
			Scale = 0.1f;
		}

		public override void Update(Time time)
		{
			if(velocity > 0) velocity -= time.deltaTime;
			Alpha -= time.deltaTime;
			Position += direction * particleSpeed * velocity * time.deltaTime;

			if (Alpha <= 0) {
				Scene.MainScene.DeleteEntity(this);
			}
		}
	}
}
