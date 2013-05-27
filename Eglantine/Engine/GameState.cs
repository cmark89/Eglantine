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


		#endregion
	}
}

