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
		public string Name { get; private set; }

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
		public List<Entrance> Entrances;

		private LuaFunction enterEvent;
		private LuaFunction exitEvent;

		public Room (string roomname)
		{
			// Load the rooms datafile
			Lua lua = Eglantine.Lua;

			Name = roomname;

			// Set up the room's textures and layers.
			ParseLayers(lua, roomname);

			// Build the room's navmesh
			ParseNavmesh(lua, roomname);

			// Set up all the objects and interactable objects 
			ParseInteractables(lua, roomname);
			ParseTriggerAreas(lua, roomname);
			ParseEntrances(lua, roomname);

			enterEvent = (LuaFunction)lua["rooms." + roomname + ".onEnter"];
			exitEvent = (LuaFunction)lua["rooms." + roomname + ".onExit"];

			// Finally, tell the gamestate that the room exists
			GameState.Instance.RegisterRoom(this);
		}

		#region Lua Parsing Functions

		public void ParseLayers (Lua lua, string roomname)
		{
			List<RoomLayer> layers = new List<RoomLayer> ();
			LuaTable layersTable = lua.GetTable ("rooms." + roomname + ".Layers");

			// Parse each layer into an appropriate object for the engine to handle
			for (int i = 0; i < layersTable.Keys.Count; i++)
			{
				layers.Add(new RoomLayer ((LuaTable)layersTable[i+1]));
			}

			Background = new List<RoomLayer>();
			Foreground = new List<RoomLayer>();
			Midground = new List<RoomLayer>();

			// Sort the layers into their respective lists
			foreach (RoomLayer rl in layers)
			{
				switch(rl.Type)
				{
					case(RoomLayerDrawType.Background):
						Background.Add(rl);
						break;
					case(RoomLayerDrawType.Foreground):
						Foreground.Add(rl);
						break;
					case(RoomLayerDrawType.Midground):
						Midground.Add(rl);
						break;
					default: 
						break;
				}
			}

			// Finally, sort the lists by draw priority
			Background.Sort((x, y) => (y.Depth.CompareTo(x.Depth)));
			Foreground.Sort((x, y) => (y.Depth.CompareTo(x.Depth)));
			Midground.Sort((x, y) => (y.YCutoff.CompareTo(x.YCutoff)));
		}

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
			if(triggers == null)
				return;

			LuaTable currentTrigger;

			for (int i = 0; i < triggers.Keys.Count; i++)
			{
				// Set the active trigger to the iterated trigger value
				currentTrigger = (LuaTable)triggers[i + 1];

				// Build the triggering rectangle
				if(currentTrigger["Area"] != null)
				{
					Rectangle triggerRect = new Rectangle((int)(double)currentTrigger["X"], (int)(double)currentTrigger["Y"], (int)(double)currentTrigger["Width"], (int)(double)currentTrigger["Height"]);

					// Add the triggered event
					TriggerAreas.Add(new TriggerArea((string)currentTrigger["Name"], triggerRect, (LuaFunction)currentTrigger["OnEnter"], (bool)currentTrigger["Enabled"]));
				}
				else if(currentTrigger["Polygon"] != null)
				{
					Polygon poly = new Polygon((LuaTable)currentTrigger["Polygon"]);

					// Add the triggered event
					TriggerAreas.Add(new TriggerArea((string)currentTrigger["Name"], poly, (LuaFunction)currentTrigger["OnEnter"], (bool)currentTrigger["Enabled"]));
				}				
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

				// Build the interactable point
				currentProperty = (LuaTable)currentInteractable["InteractPoint"];
				Vector2 point = new Vector2((int)(double)currentProperty["X"], (int)(double)currentProperty["Y"]);

				//if is drawn...
				//	set the texture...
				if((bool)currentInteractable["Drawn"])
				{
					Texture = ContentLoader.Instance.Load<Texture2D>((string)currentInteractable["Texture"]);
				}

				// Add the events

				bool draw = (bool)currentInteractable["Drawn"];
				Texture2D texture = null;
				if(draw) { texture = ContentLoader.Instance.Load<Texture2D>((string)currentInteractable["Texture"]); } 

				float yCutoff = 0;
				if(currentInteractable["YCutoff"] != null) 
					yCutoff = (float)(double)currentInteractable["YCutoff"];

				// Build the clickable area
				if((LuaTable)currentInteractable["Area"] != null)
				{
					currentProperty = (LuaTable)currentInteractable["Area"];
					Rectangle rect = new Rectangle((int)(double)currentProperty["X"], (int)(double)currentProperty["Y"], (int)(double)currentProperty["Width"], (int)(double)currentProperty["Height"]);
					Interactables.Add(new Interactable((string)currentInteractable["Name"], rect, point, (LuaFunction)currentInteractable["OnInteract"], (LuaFunction)currentInteractable["OnLook"], (bool)currentInteractable["Enabled"], draw, texture, yCutoff));
				}
				else if((LuaTable)currentInteractable["Polygon"] != null)
				{
					Vector2 drawPos = Vector2.Zero;
					if(currentInteractable["DrawAt"] != null)
						drawPos = new Vector2((float)(double)currentInteractable["DrawAt.X"], (float)(double)currentInteractable["DrawAt.Y"]);

					Polygon poly = new Polygon((LuaTable)currentInteractable["Polygon"]);
					Interactables.Add(new Interactable((string)currentInteractable["Name"], poly, point, (LuaFunction)currentInteractable["OnInteract"], (LuaFunction)currentInteractable["OnLook"], (bool)currentInteractable["Enabled"], draw, texture, drawPos, yCutoff));
				}
			}
		}

		public void ParseEntrances (Lua lua, string roomname)
		{
			Entrances = new List<Entrance> ();

			LuaTable entrances = lua.GetTable ("rooms." + roomname + ".Entrances");

			LuaTable currentTable;

			for (int i = 0; i < entrances.Keys.Count; i++)
			{
				currentTable = (LuaTable)entrances[i + 1];
				float x = (float)(double)currentTable["X"];
				float y = (float)(double)currentTable["Y"];
				string name = (string)currentTable["Name"];

				Entrances.Add(new Entrance(this, new Vector2(x, y), name));
			}
		}

		#endregion


		public void Update (GameTime gameTime)
		{
			foreach (TriggerArea ta in TriggerAreas.FindAll(x => x.Active))
			{
				ta.Update (gameTime);
			}

			if (!AdventureScreen.Instance.InputDisabled && !AdventureScreen.Instance.MouseInGui)
			{
				foreach (Interactable i in Interactables.FindAll(x => x.Active))
				{
					i.Update(gameTime);
				}
			}

			// Should give the room layers an update here...
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			foreach (RoomLayer rl in Background)
			{
				rl.Draw(spriteBatch);
			}
		}

		public void OnEnterRoom()
		{
			if(enterEvent != null)
				enterEvent.Call ();
			else
				Console.WriteLine ("Enter event null");
		}

		public void OnExitRoom()
		{
			if(exitEvent != null)
				exitEvent.Call ();
		}
	}
}