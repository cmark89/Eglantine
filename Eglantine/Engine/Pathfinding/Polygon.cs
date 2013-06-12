using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine.Pathfinding
{
	public class Polygon
	{
		// The list of vertices --must-- wind consecutively in one direction
		public List<Vector2> Vertices { get; private set; }
		public Polygon ()
		{
			Vertices = new List<Vector2>();
		}

		public Polygon (List<Vector2> vertices)
		{
			Vertices = vertices;
		}

		/// <summary>
		/// Adds a vertex to the polyon.
		/// </summary>
		public void AddVertex (Vector2 p)

		{
			Vertices.Add(p);
		}

		/// <summary>
		/// Gets the center point of the polygon.
		/// </summary>
		public Vector2 GetCenter ()
		{
			float x = 0;
			float y = 0;

			foreach (Vector2 p in Vertices) 
			{
				x += p.X;
				y += p.Y;
			}

			x /= Vertices.Count;
			y /= Vertices.Count;

			return new Vector2(x, y);
		}

		public bool ContainsPoint (Vector2 startPoint)
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

