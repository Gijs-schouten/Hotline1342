using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
	public class QuadTree
	{
		private List<Cell> cells;

		private class Cell
		{
			private List<Cell> cells;
			private List<Shape> objects;
			public Vector2 Position { get; private set; }
			public Vector2 Size { get; private set; }
		}
	}

	public abstract class Shape
	{
		/*public static bool CircleWithRectangle(Circle circle, Rectangle aabb)
		{

		}

		public static bool RectangleWithRectangle(Rectangle rect1, Rectangle rect2)
		{

		}*/

		public static bool CircleWithCircle(Circle circle1, Circle circle2)
		{
			float x = circle2.Center.X - circle1.Center.X;
			float y = circle2.Center.Y - circle2.Center.Y;
			float r = circle2.Radius - circle1.Radius;

			return x * x + y * y <= r * r;
		}
	}

	public class Circle : Shape
	{
		public Vector2 Center;
		public float Radius;

		public Circle(Vector2 center, float radius)
		{
			Center = center;
			Radius = radius;
		}
	}

	public class Rectangle : Shape
	{
		public Vector2 Position;
		public Vector2 Size;

		public Rectangle(Vector2 position, Vector2 size)
		{
			Position = position;
			Size = size;
		}

		public Rectangle(float x, float y, float width, float height) 
			: this(new Vector2(x, y), new Vector2(width, height))
		{

		}
	}
}
