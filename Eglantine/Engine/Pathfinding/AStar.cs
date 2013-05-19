using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine.Pathfinding
{
	public class AStar
	{
		private Navmesh Navmesh;
		private List<NavNode> OpenList;
		private List<NavNode> ClosedList;

		private Point EndPoint;

		public AStar (Navmesh mesh)
		{
			Navmesh = mesh;
		}

		public List<NavNode> GetPath(NavNode startNode, NavNode endNode)
		{
			// First, make sure the OpenList and ClosedList are empty
			OpenList.Clear();
			ClosedList.Clear();

			// Since the path is starting now, dump all previously calculated 
			// pathfinding values from the nodes
			Navmesh.ResetPathfinding();

			// Save the given endNode for the heuristic
			EndPoint = endNode;

			NavNode currentNode = startNode;

			// Now add the startNode to the open list.
			OpenList.Add(startNode);
			CalculateValue(startNode);
		}

		public void CalculateValue (NavNode node)
		{
			// Set the G value to the G of the parent plus the distance between parent and child node
			float tempG = 0;
			if (node.ParentNode != null)
			{
				tempG += node.ParentNode.GScore;
				tempG += node.MovementCostFrom(node.ParentNode);
			}
			node.GScore = tempG;

			node.HScore = Heuristic(node.Position, EndPoint);
		}

		public float Heuristic(Point start, Point end)
		{
			return Math.Abs(end.X - start.X) + Math.Abs(end.Y - start.Y);
		}
	}
}

