using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public sealed class GameState
	{
		// GameState is a singleton.
		private static GameState _instance;
		public static GameState Instance {
			get {
				if (_instance == null)
					_instance = new GameState ();

				return _instance;
			}
		}

		// Stores the items the player has collected.
		public List<Item> PlayerItems { get; private set; }

		// Stores the rooms as they currently exist in the game.
		public List<Room> Rooms { get; private set; }
		public Room CurrentRoom { get; private set; }

		// This is stored here only during serialization to load the player position.
		public Vector2 PlayerPosition;


		public GameState ()
		{
			Console.WriteLine ("Creating new GameState...");

			// Initialize lists here
			PlayerItems = new List<Item> ();
			Rooms = new List<Room> ();

			// Populate the list of rooms
			for (int i = 1; i < Eglantine.Lua.GetTable("requiredRooms").Keys.Count + 1; i++)
			{
				Rooms.Add(new Room(Eglantine.Lua.GetString("requiredRooms[" + i + "]")));
			}

			// Set the current room to the first room.
			CurrentRoom = Rooms[0];

			// Set the player's position to the first entrance in the first room.
			Player.Instance.SetPosition(CurrentRoom.Entrances[0].Point);

			// Initialize all other progress related values here...
		}


		public static GameState NewGameState() 
		{
			_instance = new GameState();
			return _instance;
		}

		// Load a gamestate from file in order to resume the game
		public static GameState LoadState()
		{
			// Load player's position from file
			Player.Instance.SetPosition(PlayerPosition);
		}

		// Save a gamestate to file
		public static void SaveState()
		{
			// Prepare to save player position to file
			PlayerPosition = Player.Instance.Position;
		}


		#region Instance methods
		public void RegisterRoom(Room room)
		{
			if(!Rooms.Contains(room))
				Rooms.Add(room);
		}

		public void GainItem(Item newItem)
		{
			PlayerItems.Add(newItem);
		}

		// Changes the room to the room with the given name.
		public void SetRoom (string roomName)
		{
			CurrentRoom = Rooms.Find(x => x.Name == roomName);
		}

		public void ChangeRoom (string targetRoomName, string targetEntranceName)
		{
			Room targetRoom = Rooms.Find (x => x.Name == targetRoomName);

			if (targetRoom == null)
			{
				Console.WriteLine ("Error!  Could not find Room: " + targetRoomName);
				return;
			}

			Entrance targetEntrance = targetRoom.Entrances.Find (x => x.Name == targetEntranceName);

			if (targetEntrance == null)
			{
				Console.WriteLine("Error!  Could not find Entrance: " + targetEntranceName + " in Room: " + targetRoomName);
				return;
			}

			CurrentRoom = targetRoom;
			Player.Instance.SetPosition(targetEntrance.Point);
			// Cancel all standing player orders 
		}

		#endregion
	}
}

