using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PadZex.Collision
{
	public partial class QuadTree
	{
		private class Cell
		{
			public IReadOnlyList<Shape> Objects => objects;

			private List<Cell> cells;
			private List<Shape> objects;
			public Vector2 Position { get; private set; }
			public Vector2 Size { get; private set; }

			public Cell()
			{
				cells = new List<Cell>();
				objects = new List<Shape>();
			}

			public void AddShape(Shape shape)
			{
				objects.Add(shape);
			}
		}
	}
}
