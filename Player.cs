using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		public override void Initialize(ContentManager content)
		{
			playerSprite = content.Load<Texture2D>("sprites/player");
		}

		public override void Draw(SpriteBatch spriteBatch, Time time)
		{
			Draw(spriteBatch, playerSprite);
		}

		public override void Update(Time time)
		{
			Debug.WriteLine(Position.X);
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
	}
}
