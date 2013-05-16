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

		// Create a navmesh from a luatable
		public Navmesh (LuaTable nav)
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
	}
}

