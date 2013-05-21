using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LuaInterface;

namespace Eglantine.Engine.Pathfinding
{
	public class Navmesh
	{
		List<Polygon> Polygons = new List<Polygon>();
		List<PolygonLink> Links = new List<PolygonLink>();

		public List<NavNode> Nodes = new List<NavNode>();
		private List<NavNode> tempNodes = new List<NavNode>();
		AStar aStar;



		// Create a navmesh from a luatable
		public Navmesh (LuaTable nav)
		{
			// Parse the table into nodes
			ParseLuaTable(nav);
			BuildMesh();
			// Now create an instance of AStar to link to the mesh
			aStar = new AStar(this);

			Console.WriteLine("Navmesh constructed.");
		}


		/// <summary>
		/// Parses a lua table and extracts a list of polygons and connection points.
		/// </summary>
		public void ParseLuaTable(LuaTable nav)
		{
			LuaTable currentTable = (LuaTable)nav["Polygons"];
			LuaTable currentPolygon;
			Polygon tempPolygon;
			int tempX, tempY;

			// Iterate through each polygon in the table
			for (int i = 1; i < currentTable.Keys.Count + 1; i++) {

				currentPolygon = (LuaTable)currentTable [i];

				// Add each point in the polygon to the temporary object
				tempPolygon = new Polygon ();
				LuaTable nestedTable;
				for (int point = 1; point < currentPolygon.Keys.Count + 1; point++) 
				{
					nestedTable = (LuaTable)currentPolygon[point];
					tempX = (int)((double)(nestedTable["X"]));
					tempY = (int)((double)(nestedTable["Y"]));
					tempPolygon.AddVertex (new Point (tempX, tempY));
				}

				// Now that all the points have been added to the polygon, add it to
				// the polygon list.
				Polygons.Add (tempPolygon);
				Console.WriteLine("Added polygon.");
			}

			Console.WriteLine(Polygons.Count + " added!");

			// All the polygons should now have been added to the table.  
			// Now add all the links
			currentTable = (LuaTable)nav ["Connections"];

			LuaTable currentLink;
			PolygonLink tempLink;
			for (int i = 1; i < currentTable.Keys.Count + 1; i++) {
				// Add the polygons that this link connects
				tempLink = new PolygonLink ();
				currentLink = (LuaTable)currentTable[i];
				currentLink = (LuaTable)currentLink["Connects"];
				tempLink.AddPolygon(Polygons[(int)(double)(currentLink[1]) - 1]);
				tempLink.AddPolygon(Polygons[(int)(double)(currentLink[2]) - 1]);

				// Add the points at which those polygons are connected
				currentLink = (LuaTable)currentTable[i];
				currentLink = (LuaTable)currentLink["Points"];
				LuaTable nestedTable;
				for (int j = 1; j < currentLink.Keys.Count + 1; j++) 
				{
					nestedTable = (LuaTable)currentLink[j];
					tempX = (int)(double)nestedTable["X"];
					tempY = (int)(double)nestedTable["Y"];
					tempLink.AddPoint (new Point(tempX, tempY));
				}

				// Finally add the link
				Links.Add(tempLink);
			}

			// The points from the navmesh should now have been loaded. 
			return;
		}

		/// <summary>
		/// Constructs the navmesh using the polygons and connections loaded from a LuaTable
		/// </summary>
		public void BuildMesh ()
		{
			Console.WriteLine("Begin constructing navmesh.");
			Nodes = new List<NavNode> ();

			// Add each point from each polygon into the list of nodes.
			foreach (Polygon polygon in Polygons)
			{
				Console.WriteLine("Construct polygon...");
				foreach (Point point in polygon.Vertices)
				{
					Console.WriteLine("Add vertex.");
					Nodes.Add (new NavNode (point, polygon));
				}

				Nodes.Add(new NavNode(polygon.GetCenter(), polygon));
			}

			// Add each linked point to the list of nodes
			foreach (PolygonLink link in Links)
			{
				Console.WriteLine("Link polygons...");
				foreach (Point point in link.Points)
				{
					Console.WriteLine("Create link.");
					NavNode linkedNode = new NavNode(point);
					Nodes.Add (linkedNode);
					foreach (Polygon polygon in link.LinkedPolygons)
						linkedNode.AddPolygon (polygon);
				}
			}

			// Link connected nodes together
			foreach (NavNode node in Nodes)
			{
				Console.WriteLine("Linking nodes together.");
				// Get a list of all nodes that share a polygon
				List<NavNode> connectedNodes = new List<NavNode>();
				foreach(Polygon parent in node.ParentPolygon)
				{
					connectedNodes.AddRange(Nodes.FindAll(x => x.ParentPolygon.Contains(parent)));
				}

				// Link all connected nodes together
				foreach(NavNode otherNode in connectedNodes)
					NavNode.LinkNodes(node, otherNode);
			}

			Console.WriteLine("Total nodes: " + Nodes.Count);
		}


		public Polygon ContainingPolygon (Point p)
		{
			foreach (Polygon polygon in Polygons)
			{
				if(polygon.ContainsPoint(p))
				{
				   return polygon;
				}
			}
			Console.WriteLine("Found null polygon.");
			return null;
		}

		/// <summary>
		/// Resets all pathfinding values saved in the nodes list
		/// </summary>
		public void ResetPathfindingData ()
		{
			foreach (NavNode n in Nodes)
			{
				n.ResetPathfindingData();
			}
		}

		public void ClearTempNodes ()
		{
			Console.WriteLine("TEMP NODES CONTAINS " + tempNodes.Count);
			foreach (NavNode node in Nodes)
			{
				node.Links.RemoveAll(x => tempNodes.Contains(x));
			}

			Nodes.RemoveAll(x => tempNodes.Contains(x));
			tempNodes.Clear();
		}

		public void ReportNavmesh()
		{
			// Let's test and see!
			Console.WriteLine ("==============");
			Console.WriteLine ("NAVMESH REPORT");
			Console.WriteLine ("==============");
			Console.WriteLine ("\n");

			for (int i = 0; i < Polygons.Count; i++) 
			{
				Console.WriteLine("----------\nPolygon " + i + "\n----------");
				foreach(Point p in Polygons[i].Vertices)
				{
					Console.WriteLine("\tX: " + p.X + "  :  " + p.Y);
				}

				Console.WriteLine("\n");
			}

			for (int i = 0; i < Links.Count; i++) 
			{
				Console.WriteLine("----------\nLink " + i + "\n----------");
				Console.WriteLine("\tLinks " + Links[i].LinkedPolygons.Count + " Polygons.\n");
				foreach(Point p in Links[i].Points)
				{
					Console.WriteLine("\tX: " + p.X + "  :  " + p.Y);
				}

				Console.WriteLine("\n");
			}
				Console.WriteLine("==========================");
				Console.WriteLine("Polygons Created: " + Polygons.Count);
				Console.WriteLine("Links Created: " + Links.Count);

				int ptotal = 0;
				foreach(Polygon p in Polygons)
				{
					foreach(Point po in p.Vertices)
						ptotal++;
				}
				foreach(PolygonLink p in Links)
				{
					foreach(Point po in p.Points)
						ptotal++;
				}

				Console.WriteLine("Total points: " + ptotal);
		}

		public List<NavNode> GetPath (Point start, Point end)
		{
			Console.WriteLine("Begin GETPATH");
			// Add the start node and link it
			NavNode startNode = new NavNode (start);
			Polygon startPolygon = ContainingPolygon(start);
			startNode.ParentPolygon.Add(startPolygon);
			foreach (NavNode n in Nodes.FindAll(x => x.ParentPolygon.Contains(startPolygon)))
			{
				Console.WriteLine("Linked node to start node.");
				NavNode.LinkNodes(n, startNode);
				startNode.AddLink(n);
			}
			Nodes.Add(startNode);
			tempNodes.Add(startNode);

			// Add the end node and link it
			NavNode endNode = new NavNode (end);
			Polygon endPolygon = ContainingPolygon(end);
			endNode.ParentPolygon.Add(endPolygon);
			foreach (NavNode n in Nodes.FindAll(x => x.ParentPolygon.Contains(endPolygon)))
			{
				Console.WriteLine("Linked node to end node.");
				NavNode.LinkNodes(n, endNode);
			}
			Nodes.Add(endNode);
			tempNodes.Add(endNode);

			// Find the path
			List<NavNode> path = aStar.GetPath(startNode, endNode);

			// Clear temp nodes
			ClearTempNodes();

			// Return the path
			return path;
		}
	}
}

