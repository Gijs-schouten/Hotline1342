using System;
using System.Collections.Generic;

namespace PadZex.Collision
{
	/// <summary>
	/// <para>	/// Base shape class to be used with the QuadTree.
	/// Extend this class to make a new shape type.
	/// </para>
	///
	/// See: <see cref="Rectangle"/> & <see cref="Circle"/>
	/// </summary>
	public abstract class Shape
	{
		public delegate void ShapeCollisionDelegate(PadZex.Entity shape);

		/// <summary>
		/// Event fired when a shape enters this shape.
		/// </summary>
		public event ShapeCollisionDelegate ShapeEnteredEvent;

		/// <summary>
		/// Event fired when a shape exits this shape and was previously in it.
		/// </summary>
		public event ShapeCollisionDelegate ShapeExitedEvent;

		/// <summary>
		/// The entity that owns this shape.
		/// </summary>
		public PadZex.Entity Owner { get; }

		/// <summary>
		/// The shapes that this shape is currently collided with.
		/// </summary>
		public IReadOnlyList<Shape> CollidedShapes => collidedShapes;

		private List<Shape> collidedShapes;

		protected Shape(PadZex.Entity owner)
		{
			this.Owner = owner;
			collidedShapes = new List<Shape>();
		}

		public abstract bool CollideWithRect(Rectangle rect);
		public abstract bool CollideWithCircle(Circle circle);

		private void InvokeEnterCollision(PadZex.Entity entity) => ShapeEnteredEvent?.Invoke(entity);
		private void InvokeExitCollision(PadZex.Entity entity) => ShapeExitedEvent?.Invoke(entity);

		private void ShapeCollided(Shape shape)
		{
			collidedShapes.Add(shape);
			InvokeEnterCollision(shape.Owner);
		}

		private void ShapeExited(Shape shape)
		{
			collidedShapes.Remove(shape);
			InvokeExitCollision(shape.Owner);
		}

		private bool Collided(Shape shape) => shape switch
		{
			Rectangle rect => CollideWithRect(rect),
			Circle circle => CollideWithCircle(circle),
			_ => throw new NotImplementedException()
		};

		public void CheckCollision(Shape shape)
		{
			bool collided = Collided(shape);

			if(collided)
			{
				ShapeCollided(shape);
			}
			else if(collidedShapes.Contains(shape))
			{
				ShapeExited(shape);
			}
		}

		
	}
}
