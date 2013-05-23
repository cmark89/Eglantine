using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine.Pathfinding
{
	public class PolygonLink
	{
		public List<Polygon> LinkedPolygons { get; private set; }
		public List<Vector2> Points { get; private set; }

		public PolygonLink ()
		{
			LinkedPolygons = new List<Polygon>();
			Points = new List<Vector2>();
		}

		public void AddPolygon(Polygon p)
		{
			LinkedPolygons.Add(p);
		}

		public void AddPoint(Vector2 p)
		{
			Points.Add(p);
		}
	}
}

