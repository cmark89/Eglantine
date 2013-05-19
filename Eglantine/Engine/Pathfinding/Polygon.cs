using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine.Pathfinding
{
	public class Polygon
	{
		public List<Point> Vertices { get; private set; }
		public Polygon ()
		{
			Vertices = new List<Point>();
		}

		public Polygon (List<Point> vertices)
		{
			Vertices = vertices;
		}

		/// <summary>
		/// Adds a vertex to the polyon.
		/// </summary>
		public void AddVertex (Point p)

		{
			Vertices.Add(p);
		}

		/// <summary>
		/// Gets the center point of the polygon.
		/// </summary>
		public Point GetCenter ()
		{
			int x = 0;
			int y = 0;

			foreach (Point p in Vertices) 
			{
				x += p.X;
				y += p.Y;
			}

			x /= Vertices.Count;
			y /= Vertices.Count;

			return new Point(x, y);
		}

		public bool ContainsPoint (Point startPoint)
		{
			bool inside = false;

			for (int i = 0, j = Vertices.Count - 1; i < Vertices.Count; j = i++)
			{
				if((Vertices[i].Y > startPoint.Y) != (Vertices[j].Y > startPoint.Y) && 
					startPoint.X < (Vertices[i].X - Vertices[j].X) * (startPoint.Y - Vertices[i].Y) / (Vertices[j].Y - Vertices[i].Y) + Vertices[i].X)
				{
					inside = !inside;
				}
			}

			return inside;
		}
	}
}

