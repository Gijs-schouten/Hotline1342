using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PadZex
{
	/// <summary>
	/// This contains the base functionality a scene needs to function.
	/// Entities can be added here and then 
	/// </summary>
	public class Scene
	{
		public static Scene MainScene { get; private set; }

		protected List<Entity> entities;
		private ContentManager contentManager;

		public Scene(ContentManager contentManager)
		{
			entities = new List<Entity>();
			this.contentManager = contentManager;
		}

		public void AddEntity(Entity entity)
		{
			entities.Add(entity);
			entity.Initialize(contentManager);
		}

		public virtual void Draw(SpriteBatch spriteBatch, Time time)
		{
			// TODO: check whats on screen first before drawing it.

			foreach(var entity in entities)
			{
				entity.Draw(spriteBatch, time);
			}
		}

		public virtual void Update(Time time)
		{
			foreach (var entity in entities)
			{
				entity.Update(time);
			}
		}

		public void SetAsMainScene() => MainScene = this;

		public Entity FindEntity(string tag) => entities.FirstOrDefault(x => x.Tags.Contains(tag));
	}
}
