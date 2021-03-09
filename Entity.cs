using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PadZex
{
    /// <summary>
    /// Base functionality for any entity that is drawn on screen.
    /// </summary>
    public abstract class Entity
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Origin = default;
        public float Angle = .0f;
        public float Scale = 1.0f;
        public int Depth = 0;

        public IEnumerable<string> Tags => tags;
        private List<string> tags = new List<string>();
        private SpriteEffects effect = SpriteEffects.None;

        public abstract void Initialize(ContentManager content);
        public abstract void Update(Time time);
        public abstract void Draw(SpriteBatch spriteBatch, Time time);

        /// <summary>
        /// Look for an entity in the Active scene and return it
        /// </summary>
        /// <param name="tag">Tag to look for</param>
        /// <returns>an Entity in the scene. Null if not found.</returns>
        public static Entity FindEntity(string tag) => Scene.MainScene?.FindEntity(tag);

        /// <summary>
        /// Deletes an entity the next frame in the active scene.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        public static void DeleteEntity(Entity entity) => Scene.MainScene?.DeleteEntity(entity);

        /// <summary>
        /// Create an entity and apply tags on it.
        /// </summary>
        /// <param name="tags">Tags to apply on the entity.</param>
        public Entity(params string[] tags)
        {
            this.tags.AddRange(tags);
        }

        public Entity() { }

        /// <summary>
        /// Draw the texture to the screen with the entities attributes.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Position, null,
                Color.White, Angle, Origin,
                Scale, effect, Depth);
        }

        public void AddTag(string tag)
        {
            tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            tags.Remove(tag);
        }

        public void FlipSprite()
        {
            effect = SpriteEffects.FlipHorizontally;
        }
    }
}
