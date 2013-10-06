using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine.Pathfinding;

namespace Eglantine.Engine.Pathfinding
{
	[Serializable]
	public class NavNode
	{
		// Core members
		public Vector2 Position { get; private set; }
		public List<Polygon> ParentPolygon { get; set; }
		public List<NavNode> Links { get; private set; }

		public bool InOpenList = false;
		public bool InClosedList = false;

		// Pathfinding members
		public float HScore = 0;
		public float GScore = 0;
		public float FScore
		{
			get { return HScore + GScore; }
		}
		public NavNode ParentNode;		// Used to trace the path using AStar

		public NavNode (Vector2 point, Polygon parent = null)
		{
			Position = point;
			ParentPolygon = new List<Polygon>();
			Links = new List<NavNode>();

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
		public float MovementCostFrom (Vector2 fromPosition)
		{
			return (float)Math.Pow(Math.Abs(fromPosition.X - Position.X) + Math.Abs(fromPosition.Y - Position.Y), 2f);
		}

		public void ResetPathfindingData()
		{
			GScore = 0;
			HScore = 0;
			ParentNode = null;

			InOpenList = false;
			InClosedList = false;
		}
	}
}

