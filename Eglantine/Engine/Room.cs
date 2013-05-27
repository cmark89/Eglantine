using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine.Pathfinding;
using LuaInterface;

namespace Eglantine.Engine
{
	public class Room
	{
		// The rooms' texture.  This will later be split into multiple layers.
		public Texture2D Texture { get; private set; }

		// Background layers are always drawn behind the player.
		public List<RoomLayer> Background { get; private set; }

		// Midground layers are drawn behind or in front of the player depending on Y-axis depth
		public List<RoomLayer> Midground { get; private set; }

		// Foreground layers are drawn in front of the player
		public List<RoomLayer> Foreground { get; private set; }

		// The navmesh that guides the player's movement
		public Navmesh Navmesh { get; set; }

		public List<Interactable> Interactables;
		public List<TriggerArea> TriggerAreas;
		public List<Point> Entrances;

		public Room (string roomname)
		{
			// Load the rooms datafile
			Lua lua = Eglantine.Lua;
			lua.DoFile ("Data/rooms.lua");

			// Set up the room's textures and layers.

			// Build the room's navmesh
			ParseNavmesh(lua, roomname);

			// Set up all the objects and interactable objects 
			ParseInteractables(lua, roomname);
			ParseTriggerAreas(lua, roomname);
		}

		#region Lua Parsing Functions

		public void ParseNavmesh(Lua lua, string roomname)
		{
			LuaTable navmeshTable = lua.GetTable ("rooms." + roomname + ".Navmesh");
			if (navmeshTable != null)
			{
				Navmesh = new Navmesh(navmeshTable);
			} else
			{
				Console.WriteLine("Error!  Could not find LuaTable rooms." + roomname + ".Navmesh.");
			}
		}

		public void ParseTriggerAreas (Lua lua, string roomname)
		{
			TriggerAreas = new List<TriggerArea> ();

			LuaTable triggers = lua.GetTable ("rooms." + roomname + ".Triggers");
			LuaTable currentTrigger;

			for (int i = 0; i < triggers.Keys.Count; i++)
			{
				// Set the active trigger to the iterated trigger value
				currentTrigger = (LuaTable)triggers[i + 1];

				// Build the triggering rectangle
				Rectangle triggerRect = new Rectangle((int)(double)currentTrigger["X"], (int)(double)currentTrigger["Y"], (int)(double)currentTrigger["Width"], (int)(double)currentTrigger["Height"]);

				// Add the triggered event
				TriggerAreas.Add(new TriggerArea(triggerRect, (LuaFunction)currentTrigger["OnEnter"]));
			}
		}

		public void ParseInteractables (Lua lua, string roomname)
		{
			Interactables = new List<Interactable> ();

			LuaTable interactables = lua.GetTable ("rooms." + roomname + ".Interactables");
			LuaTable currentInteractable;
			LuaTable currentProperty;

			for (int i = 0; i < interactables.Keys.Count; i++)
			{
				// Set the active object to the iterated interactable value
				currentInteractable = (LuaTable)interactables[i + 1];

				// Build the clickable area
				currentProperty = (LuaTable)currentInteractable["Area"];
				Rectangle clickRect = new Rectangle((int)(double)currentProperty["X"], (int)(double)currentProperty["Y"], (int)(double)currentProperty["Width"], (int)(double)currentProperty["Height"]);

				// Build the interactable point
				currentProperty = (LuaTable)currentInteractable["InteractPoint"];
				Vector2 point = new Vector2((int)(double)currentProperty["X"], (int)(double)currentProperty["Y"]);

				//if is drawn...
				//	set the texture...

				// Add the event 
				Interactables.Add(new Interactable(clickRect, point, (LuaFunction)currentInteractable["OnInteract"]));
			}
		}

		#endregion


		public void Update (GameTime gameTime)
		{
			foreach (TriggerArea ta in TriggerAreas)
			{
				ta.Update (gameTime);
			}

			foreach (Interactable i in Interactables)
			{
				i.Update(gameTime);
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Vector2.Zero, Color.White);

			// Draw each layer

			// Draw each item.
		}
	}
}