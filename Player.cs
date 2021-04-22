﻿using PadZex.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Collision;
using PadZex.Core;
using System;
using System.Collections.Generic;
using System.Text;
using PadZex.LevelLoader;
using System.Linq;

namespace PadZex
{
	/// <summary>
	///  Player with static sprite and hp bar.
	///  WASD : Move
	/// </summary>
	public class Player : Entity ,IDamagable
	{
		private Texture2D playerSprite;
		private Color color = Color.White;
		private float speed;
		private Entity sound;

		public Player()
		{

		}

		public override void Initialize(ContentManager content)
		{
			playerSprite = content.Load<Texture2D>("sprites/player");
			//health = new Health(100, 100);
			speed = 0;
			AddTag("Player");
			Depth = 5;
			Scale = 1f / (float)playerSprite.Width * 200f;
		}

		public override void Draw(SpriteBatch spriteBatch, Time time)
		{
			Draw(spriteBatch, playerSprite);
		}

		public override void Update(Time time)
		{
			float horizontal = -Convert.ToSingle(Input.KeyPressed(Keys.A)) + Convert.ToSingle(Input.KeyPressed(Keys.D));
			float vertical = -Convert.ToSingle(Input.KeyPressed(Keys.W)) + Convert.ToSingle(Input.KeyPressed(Keys.S));

			Vector2 move = new Vector2(horizontal, vertical);

			if (move != new Vector2(0, 0))
			{
				speed += .4f;
				move = Vector2.Normalize(move);
			}
			else
			{
				speed = 0f;
			}

			speed = Math.Clamp(speed, 0, 10);
			float xVelocity = move.X * speed;
			float yVelocity = move.Y * speed;

			Position.X += xVelocity;
			CheckHorizontalCollision(xVelocity);

			Position.Y += yVelocity;
			CheckVerticalCollision(yVelocity);
		}

		private void CheckHorizontalCollision(float velocity)
        {
            (bool collided, IEnumerable<Shape> shapes) = Scene.MainScene.TestAllCollision(Shape);
			if(collided)
            {
				var walls = shapes.Where(x  =>  x.Owner.Tags.Contains("wall")).Cast<Collision.Rectangle>();
				var wall = walls.FirstOrDefault();
				if  (wall == null) return;

				if (velocity < 0) Position.X = wall.WorldX + wall.WorldWidth;
				else Position.X = wall.WorldX - ((Collision.Rectangle)Shape).WorldWidth;

            }
        }
	
		private void CheckVerticalCollision(float velocity)
        {
            (bool collided, IEnumerable<Shape> shapes) = Scene.MainScene.TestAllCollision(Shape);
			if(collided)
            {
				var walls = shapes.Where(x  =>  x.Owner.Tags.Contains("wall")).Cast<Collision.Rectangle>();
				var wall = walls.FirstOrDefault();
				if  (wall == null) return;

				if (velocity < 0) Position.Y = wall.WorldY + wall.WorldHeight;
				else Position.Y = wall.WorldY - ((Collision.Rectangle)Shape).WorldHeight;
            }
        }

		public override Shape CreateShape()
		{
			var shape = new Collision.Rectangle(this, Vector2.Zero, new Vector2(playerSprite.Width, playerSprite.Height));
			return shape;
		}

		public void Damage(Entity entity, float damage = 0)
        {
			sound = FindEntity("sound");
			sound.Position = new Vector2(1, 3);
			//  Entity.DeleteEntity(this);
		}
    }
}
		