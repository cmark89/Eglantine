using System;
using LuaInterface;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization.Formatters.Binary;

namespace Eglantine.Engine
{
	[Serializable]
	public sealed class GameState
	{
		#region Member Variables
		// GameState is a singleton.

		[NonSerialized]
		private static GameState _instance;
		public static GameState Instance {
			get {
				if (_instance == null)
					_instance = new GameState ();

				return _instance;
			}
		}

		// This is temporary.
		private const string START_ROOM = "Foyer";

		// Stores the items the player has collected.
		public List<Item> PlayerItems { get; private set; }
		public List<Document> Documents { get; private set; }

		// Stores the rooms as they currently exist in the game.
		public List<Room> Rooms { get; private set; }
		public Room CurrentRoom { get; private set; }

		// This is stored here only during serialization to load the player position.
		public Vector2 PlayerPosition;
		#endregion


		#region GameState Variables
		public bool PaintingOpened = false;
		public bool PhotoTaken = false;
		public bool KitchenWindowBroken = false;
		public PuzzleboxState PuzzleboxState { get; private set; }
		public SafeState SafeState { get; private set; }

		#endregion

		#region Setup, Creation and Loading
		public GameState ()
		{
			Console.WriteLine ("Creating new GameState...");
		}

		public bool PlayerHasItem (string itemName)
		{
			return (PlayerItems.FindAll(x => x.Name == itemName).Count > 0);
		}


		// Sets up the GameState for a new game.
		public void InitializeNewGame ()
		{
			// Initialize lists here
			PlayerItems = new List<Item> ();
			Rooms = new List<Room> ();
			Documents = new List<Document>();
			PuzzleboxState = new PuzzleboxState();
			SafeState = new SafeState();

			//LuaTable tempTable;
			// Populate the list of rooms
			LuaTable tempTable = Eglantine.Lua.GetTable("requiredRooms");

			for (int i = 0; i < tempTable.Keys.Count; i++)
			{
				Console.WriteLine("Add room \"" + (string)tempTable[i+1] +"\"");
				Rooms.Add(new Room((string)tempTable[i+1]));
			}

			Console.WriteLine("All rooms added.");

			// Set the current room to the first room.
			CurrentRoom = Rooms.Find (x => x.Name == START_ROOM);

			// Set the player's position to the first entrance in the first room.
			Player.Instance.SetPosition(CurrentRoom.Entrances[0].Point);

			// Initialize all other progress related values here...
		}


		public static GameState NewGameState() 
		{
			_instance = new GameState();
			// Make sure the lua side knows about this GameState
			Eglantine.Lua.DoString("loadGameState()");
			GameState.Instance.InitializeNewGame();
			return _instance;
		}

		// Load a gamestate from file in order to resume the game
		public static GameState LoadState (string targetFile)
		{
			GameState loadedState = Serializer.Deserialize<GameState> (targetFile);
			if (loadedState != null)
			{
				_instance = loadedState;

				// Load player's position from file
				Player.Instance.SetPosition(loadedState.PlayerPosition);

				// Load coroutines
			}

			return loadedState;
		}

		// Save a gamestate to file
		public static void SaveState(string targetFile)
		{
			// Prepare to save player position to file
			Instance.PlayerPosition = Player.Instance.Position;

			Serializer.Serialize<GameState>(targetFile, Instance);
		}

		#endregion
		#region Instance methods
		public void RegisterRoom(Room room)
		{
			if(!Rooms.Contains(room))
				Rooms.Add(room);
		}

		public void GainItem(Item newItem)
		{
			PlayerItems.Add(newItem);
			newItem.OnAquire();
		}

		public void DestroyItem (string item)
		{
			if(AdventureScreen.Instance.LoadedItem != null && AdventureScreen.Instance.LoadedItem.Name == item)
				AdventureScreen.Instance.SetActiveItem(null);

			if(PlayerItems.FindAll(x => x.Name == item).Count > 0)
				PlayerItems.Remove (PlayerItems.Find (x => x.Name == item));
		}

		public void AddDocument (Document doc)
		{
			Console.WriteLine("Add " + doc.Name + " to the document list.");
			Documents.Add(doc);
		}

		// Changes the room to the room with the given name.
		public void SetRoom (string roomName)
		{
			CurrentRoom = Rooms.Find(x => x.Name == roomName);
		}

		public void ChangeRoom (string targetRoomName, string targetEntranceName)
		{
			if(CurrentRoom != null)
				CurrentRoom.OnExitRoom();

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

			if(CurrentRoom != null)
				CurrentRoom.OnEnterRoom();
		}


		#endregion
	}
}

