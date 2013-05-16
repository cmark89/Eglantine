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

		public void AddVertex (Point p)
		{
			Vertices.Add(p);
		}
	}
}

