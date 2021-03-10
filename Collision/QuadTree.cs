using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PadZex.Collision
{
	public partial class QuadTree
	{
		private List<Cell> cells;

		public QuadTree()
		{
			cells = new List<Cell>();
			cells.Add(new Cell());
		}

		public void UpdateCollision()
		{
			var cell = cells[0];
			foreach(var shape1 in cell.Objects)
			{
				foreach(var shape2 in cell.Objects)
				{
					if (shape1 == shape2)
						continue;

					shape1.CheckCollision(shape2);
				}
			}
		}

		public void AddShape(Shape shape)
		{
			// TODO : Add the shape to the correct cell. Currently adds it to the main cell.
			Cell cell = GetCell(shape);
			cell.AddShape(shape);
		}

		private Cell GetCell(Shape shape)
		{
			return cells[0];
		}
	}
}
