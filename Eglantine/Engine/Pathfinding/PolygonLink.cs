using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine.Pathfinding
{
	public class PolygonLink
	{
		public List<Polygon> LinkedPolygons { get; private set; }
		public List<Point> Points { get; private set; }

		public PolygonLink ()
		{
			LinkedPolygons = new List<Polygon>();
			Points = new List<Point>();
		}

		public void AddPolygon(Polygon p)
		{
			LinkedPolygons.Add(p);
		}

		public void AddPoint(Point p)
		{
			Points.Add(p);
		}
	}
}

