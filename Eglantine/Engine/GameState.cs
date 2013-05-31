using System;
using System.Collections.Generic;

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



		// Load a gamestate from file in order to resume the game
		public static void LoadState()
		{
		}

		// Save a gamestate to file
		public static void SaveState()
		{
		}


		#region Instance methods
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
			Player.Instance.Position = targetEntrance.Point;
			// Cancel all standing player orders 
		}

		#endregion
	}
}

