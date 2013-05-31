using System;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public class Entrance
	{
		// The room that contains this entrance.
		public Room ParentRoom { get; private set; }

		// The name of this entrance.  Ex: "Painting", "Left Door"
		public string Name { get; private set; }

		// The point where the player appears when they come to this entrance.
		public Vector2 Point { get; private set; }

		public Entrance (Room room, Vector2 point, string name)
		{
			ParentRoom = room;
			Name = name;
			Point = point;
		}
	}
}

