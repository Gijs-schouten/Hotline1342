using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PadZex.Collision;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex
{
	public class SquareEntity : Entity
	{
		Texture2D texture;

		public override void Draw(SpriteBatch spriteBatch, Time time)
		{
			Draw(spriteBatch, texture);
		}

		public override void Initialize(ContentManager content)
		{
			texture = content.Load<Texture2D>("sprites/player");
			Position = new Microsoft.Xna.Framework.Vector2(500, 500);
		}

		public override void Update(Time time)
		{

		}

		public override Shape InitializeShape()
		{
			var rectangle = new Circle(this, Vector2.Zero, texture.Width / 2);
			return rectangle;
		}
	}
}
