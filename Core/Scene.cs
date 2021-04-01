using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PadZex.Collision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PadZex.Core
{
    /// <summary>
    /// This contains the base functionality a scene needs to function.
    /// Entities can be added here and then 
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// The scene that is currently set as "active" and updated and drawn
        /// </summary>
        public static Scene MainScene { get; private set; }

        protected List<Entity> entities;
        protected List<Entity> entityGulag;
        private ContentManager contentManager;
        private CollisionField quadTree;

		public Camera Camera { get; set; }
        public Scene(ContentManager contentManager)
        {
            entities = new List<Entity>();
            entityGulag = new List<Entity>();
            quadTree = new CollisionField();
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Add an entity to the scene and initialize it
        /// </summary>
        public Entity AddEntity(Entity entity)
        {
            entities.Add(entity);
            entity.Initialize(contentManager);
            Shape shape = entity.InitializeShape();
            if (shape != null)
            {
                quadTree.AddShape(shape);
            }
            return entity;
        }

        /// <summary>
        /// Initialize is called when the scene activates.
        /// </summary>
        public virtual void Initialize()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Time time)
        {
            // TODO: check whats on screen first before drawing it.

            foreach (var entity in entities)
            {
                entity.Draw(spriteBatch, time);
            }
        }

        public virtual void Update(Time time)
        {
            // delete all entities in the dirty list first.
            foreach (var entity in entityGulag)
            {
                if (!entities.Contains(entity)) continue;

                entity.OnDestroy();
                entities.Remove(entity);
            }

            foreach (var entity in entities)
            {
                entity.Update(time);
            }

            quadTree.UpdateCollision();
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
        /// Find an Entity of a specific type in the main scene and return it
        /// </summary>
        /// <typeparam name="T">Must inherit <see cref="Entity"/>.</typeparam>
        /// <param name="tag">Tag the entity needs</param>
        /// <returns>An entity of Type <typeparamref name="T"/>, or null if not found.</returns>
        public T FindEntity<T>(string tag) where T : Entity => entities.FirstOrDefault(x => x.Tags.Contains(tag) && x is T) as T;

        /// <summary>
        /// Mark an entity in the MainScene for death and delete it the next frame.
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntity(Entity entity) => entityGulag.Add(entity);
    }
}
