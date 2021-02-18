using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PadZex
{
	public abstract partial class Entity
	{
		public Vector2 Position = Vector2.Zero;
		public Vector2 Origin = default;
		public float Angle = .0f;
		public float Scale = 1.0f;
		public int Depth = 0;

		public IEnumerable<string> Tags { get => tags; }
		private List<string> tags = new List<string>();

		public abstract void Initialize(ContentManager content);
		public abstract void Update(Time time);
		public abstract void Draw(SpriteBatch spriteBatch, Time time);

		public static Entity FindEntity(string tag) => Scene.MainScene?.FindEntity(tag);

		public Entity(params string[] tags)
		{
			this.tags.AddRange(tags);
		}

		public Entity() { }

		public void Draw(SpriteBatch spriteBatch, Texture2D texture)
		{
			spriteBatch.Draw( texture, Position, null,
				Color.White, Angle, Origin,
				Scale, SpriteEffects.None, Depth);
		}

		public void AddTag(string tag)
		{
			tags.Add(tag);
		}

		public void RemoveTag(string tag)
		{
			tags.Remove(tag);
		}
	}
}
