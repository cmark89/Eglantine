using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine.Pathfinding
{
	public class AStar
	{
		private Navmesh Navmesh;
		private List<NavNode> OpenList = new List<NavNode>();
		private List<NavNode> ClosedList = new List<NavNode>();

		private NavNode EndPoint;

		public AStar (Navmesh mesh)
		{
			Navmesh = mesh;
		}

		public List<NavNode> GetPath (NavNode startNode, NavNode endNode)
		{
			Console.WriteLine ("Starting AStar.GetPath()");
			Console.WriteLine ("Navmesh contains " + Navmesh.Nodes.Count + " nodes");
			// First, make sure the OpenList and ClosedList are empty
			OpenList.Clear ();
			ClosedList.Clear ();

			// Since the path is starting now, dump all previously calculated 
			// pathfinding values from the nodes
			Navmesh.ResetPathfindingData ();

			// Save the given endNode for the heuristic
			EndPoint = endNode;

			NavNode currentNode = startNode;

			// Now add the startNode to the open list.
			Console.WriteLine ("Start point initialized.");
			OpenList.Add (currentNode);
			currentNode.InOpenList = true;
			CalculateValue (currentNode);

			foreach (NavNode l in endNode.Links)
			{
				Console.WriteLine("END NODE LINKS TO " + l.Position.X + ":" + l.Position.Y);
				if(l.Links.Contains(endNode))
					Console.WriteLine(l.Position.X + ":" + l.Position.Y + " links back to endpoint.");
				else
					Console.WriteLine(l.Position.X + ":" + l.Position.Y + " DOES NOT LINK TO ENDPOINT.");
			}

			int considered = 0;
			// Iterate over nodes until the path has been found
			while (OpenList.Count > 0)
			{
				considered++;
				Console.WriteLine ("Iterating over openlist.");
				// Pick the node in the OpenList with the lowest F Score
				currentNode = FindBestNode ();

				// Now add all of its links to the OpenList
				foreach (NavNode link in currentNode.Links)
				{
					// First, check if the node is the target node.  If so, break immediately.
					if (link == EndPoint)
					{
						Console.WriteLine ("End point found.");
						EndPoint.ParentNode = currentNode;
						OpenList.Clear ();

						// Trace the path.
						List<NavNode> path = new List<NavNode> ();
						currentNode = endNode;
						path.Add (endNode);
						while (currentNode.ParentNode != startNode)
						{
							path.Add (currentNode.ParentNode);
							currentNode = currentNode.ParentNode;
						}

						return path;
					}

					if (link.InClosedList)
					{
						continue;
					}

					if (!link.InOpenList)
					{
						OpenList.Add (link);
						link.InOpenList = true;
						link.ParentNode = currentNode;
						CalculateValue (link);
					} else
					{
						// Check if the new G value is less than the node's current F value
						if (link.GScore > link.ParentNode.GScore + link.MovementCostFrom (link.ParentNode.Position))
						{
							link.ParentNode = currentNode;
							CalculateValue (link);
						}
					}
				}
				// Now that we're done with this node, move it to the closed list
				OpenList.Remove (currentNode);
				ClosedList.Add (currentNode);
				currentNode.InOpenList = false;
				currentNode.InClosedList = true;
			}

			// Return null, for no path could be found
			Console.WriteLine ("No path found.");
			if (endNode == null)
			{
				Console.WriteLine("WARNING! END POINT IS NULL!");
			}
			else
				Console.WriteLine("End point exists but was never found.");

			return null;
		}

		public void CalculateValue (NavNode node)
		{
			// Set the G value to the G of the parent plus the distance between parent and child node
			float tempG = 0;
			if (node.ParentNode != null)
			{
				tempG += node.ParentNode.GScore;
				tempG += node.MovementCostFrom(node.ParentNode.Position);
			}
			node.GScore = tempG;

			node.HScore = Heuristic(node.Position, EndPoint.Position);
		}


		public float Heuristic(Vector2 start, Vector2 end)
		{
			return Math.Abs(end.X - start.X) + Math.Abs(end.Y - start.Y);
		}

		public NavNode FindBestNode ()
		{
			NavNode currentNode = null;
			foreach (NavNode n in OpenList)
			{
				if(currentNode == null || n.FScore < currentNode.FScore)
					currentNode = n;
			}

			return currentNode;
		}
	}
}

