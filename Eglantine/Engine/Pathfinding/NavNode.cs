using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine.Pathfinding;

namespace Eglantine.Engine.Pathfinding
{
	public class NavNode
	{
		// Core members
		public Point Position { get; private set; }
		public List<Polygon> ParentPolygon { get; private set; }
		public List<NavNode> Links { get; private set; }

		// Pathfinding members
		public float HScore = 0;
		public float GScore = 0;
		public float FScore
		{
			get { return HScore + GScore; }
		}
		public NavNode ParentNode;		// Used to trace the path using AStar

		public NavNode (Point point, Polygon parent = null)
		{
			Position = point;
			ParentPolygon = new List<Polygon>();

			if(parent != null)
				ParentPolygon.Add(parent);
		}

		public void AddPolygon(Polygon parent)
		{
			ParentPolygon.Add(parent);
		}

		public void AddLink(NavNode link)
		{
			if(!Links.Contains(link) && link != this)
				Links.Add(link);
		}

		public static void LinkNodes(NavNode n1, NavNode n2)
		{
			n1.AddLink(n2);
			n2.AddLink(n1);
		}

		// Returns the cost to move to this node from the given node
		public float MovementCostFrom (Point fromPosition)
		{
			return Vector2.Distance (fromPosition, Position);
		}
	}
}

