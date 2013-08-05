using System;
using System.Collections.Generic;
using LuaInterface;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	[Serializable]
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

		// Constructs a polygon from a lua table containing a list of vertices
		public Polygon (LuaTable table)
		{
			Vertices = new List<Vector2>();

			LuaTable currentVertex;
			float x, y;

			for (int point = 1; point < table.Keys.Count + 1; point++) 
			{
				currentVertex = (LuaTable)table[point];
				x = (float)((double)(currentVertex["X"]));
				y = (float)((double)(currentVertex["Y"]));
				AddVertex (new Vector2 (x, y));
			}
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

		public bool ContainsPoint (Vector2 point)
		{
			bool inside = false;

			for(int i = 0, j = Vertices.Count - 1; i < Vertices.Count; j = i++)
			{
				if((Vertices[i].Y > point.Y) != (Vertices[j].Y > point.Y) && 
					point.X < (Vertices[j].X - Vertices[i].X) * (point.Y - Vertices[i].Y) / (Vertices[j].Y - Vertices[i].Y) + Vertices[i].X)
				{
					inside = !inside;
				}
			}

			return inside;
		}
	}
}

