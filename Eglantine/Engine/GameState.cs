using System;
using System.Collections.Generic;

namespace Eglantine.Engine
{
	public sealed class GameState
	{
		// Stores the items the player has collected.
		List<Item> PlayerItems;

		// Stores the rooms as they currently exist in the game.
		List<Room> Rooms;



		// GameState is a singleton.
		private static GameState _instance;
		public static GameState Instance {
			get {
				if (_instance == null)
					_instance = new GameState ();

				return _instance;
			}
		}

		// Load a gamestate from file in order to resume the game
		public static void LoadState()
		{
		}

		// Save a gamestate to file
		public static void SaveState()
		{
		}
	}
}

