﻿using Microsoft.Xna.Framework.Content;
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
		protected List<Entity> entityGulag;
		private ContentManager contentManager;

		public Scene(ContentManager contentManager)
		{
			entities = new List<Entity>();
			entityGulag = new List<Entity>();
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
			// delete all entities in the dirty list first.
			foreach (var entity in entityGulag)
			{
				entities.Remove(entity);
			}

			foreach (var entity in entities)
			{
				entity.Update(time);
			}
		}

		/// <summary>
		/// Set the current scene as the main scene
		/// </summary>
		public void SetAsMainScene() => MainScene = this;

		/// <summary>
		/// Find an entity in the Main Scene and return it
		/// </summary>
		/// <param name="tag">Tag the entity needs</param>
		/// <returns>An entity, null if not found</returns>
		public Entity FindEntity(string tag) => entities.FirstOrDefault(x => x.Tags.Contains(tag));

		/// <summary>
		/// Mark an entity in the MainScene for death and delete it the next frame.
		/// </summary>
		/// <param name="entity"></param>
		public void DeleteEntity(Entity entity) => entityGulag.Add(entity);
	}
	public struct Time
	{
		public float deltaTime;
		public float timeSinceStart;
	}
}
