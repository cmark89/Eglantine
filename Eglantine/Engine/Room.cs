using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine.Pathfinding;

namespace Eglantine.Engine
{
	public class Room
	{
		// The rooms' texture.  This will later be split into multiple layers.
		public Texture2D Texture { get; private set; }

		// The navmesh that guides the player's movement
		public Navmesh Navmesh { get; set; }

		public List<Interactable> Interactables;
		public List<TriggerArea> TriggerAreas;
		public List<Point> Entrances;

		public Room ()
		{
		}
	}
}