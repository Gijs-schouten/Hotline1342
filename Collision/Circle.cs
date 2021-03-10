﻿using Microsoft.Xna.Framework;

namespace PadZex.Collision
{
	/// <summary>
	/// Circle shape for the <see cref="QuadTree"/>.
	/// </summary>
	public class Circle : Shape
	{
		public float X
		{
			get => Center.X;
			set => Center.X = value;
		}

		public float Y
		{
			get => Center.Y;
			set => Center.Y = value;
		}

		public float WorldX => X + Radius + Owner.Position.X; 
		public float WorldY => Y + Radius + Owner.Position.Y; 

		public Vector2 Center;
		public float Radius;

		public Circle(PadZex.Entity owner, Vector2 center, float radius) : base(owner)
		{
			Center = center;
			Radius = radius;
		}

		public override bool CollideWithRect(Rectangle rect) => CollisionUtils.CircleWithRectangle(this, rect);
		public override bool CollideWithCircle(Circle circle) => CollisionUtils.CircleWithCircle(this, circle);
	}
}
