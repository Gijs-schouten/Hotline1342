using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Collision;
using PadZex.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex
{
	/// <summary>
	///  Temporary test class for the player. Remove when an actual good player is implemented.
	///  WASD : Move
	///  E : Delete self
	/// </summary>
	public class Player : Entity
	{
		private Texture2D playerSprite;
		private Color color = Color.White;

		public override void Initialize(ContentManager content)
		{
			playerSprite = content.Load<Texture2D>("sprites/player");
			Depth = 1;
		}

		public override void Draw(SpriteBatch spriteBatch, Time time)
		{
			spriteBatch.Draw(playerSprite, Position, null, color, Angle, Origin, Scale, SpriteEffects.None, Depth);
		}

		public override void Update(Time time)
		{
			Debug.Log(Position.X);
			KeyboardState keyBoardState = Keyboard.GetState();

			float speed = 300.0f;

			if(keyBoardState.IsKeyDown(Keys.A))
			{
				Position.X -= speed * time.deltaTime;
			}
			else if(keyBoardState.IsKeyDown(Keys.D))
			{
				Position.X += speed * time.deltaTime;
			}

			if(keyBoardState.IsKeyDown(Keys.W))
			{
				Position.Y -= speed * time.deltaTime;
			}
			else if(keyBoardState.IsKeyDown(Keys.S))
			{
				Position.Y += speed * time.deltaTime;
			}

			if(keyBoardState.IsKeyDown(Keys.E))
			{
				Entity.DeleteEntity(this);
			}
		}

		public override Shape InitializeShape()
		{
			var shape = new Collision.Rectangle(this, Vector2.Zero, new Vector2(playerSprite.Width, playerSprite.Height));
			shape.ShapeEnteredEvent += OnShapeEnteredEvent;
			shape.ShapeExitedEvent += OnShapeExitedEvent;
			return shape;
		}

		private void OnShapeExitedEvent(Entity shape)
		{
			color = Color.White;
			Debug.Log("exited");
		}

		private void OnShapeEnteredEvent(Entity shape)
		{
			color = Color.Red;
			Debug.Log("entered");
		}
	}
}
