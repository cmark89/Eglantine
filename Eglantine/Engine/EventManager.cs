using System;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public sealed class EventManager
	{
		// EventManager is a singleton.
		private static EventManager _instance;
		public static EventManager Instance
		{
			get
			{
				if(_instance == null)
					_instance = new EventManager();

				return _instance;
			}
		}

		public static void Initialize ()
		{
			_instance = new EventManager();
			Eglantine.Lua.DoString("loadEventManager()");
		}

		// Queues a message into the message display manager
		public void ShowMessage(string message)
		{
			Console.WriteLine("SHOW MESSAGE: " + message);
			MessageManager.Instance.ShowMessage(message);
		}

		// Forces the player to move to the given point.
		public void MovePlayer(double x, double y)
		{
			AdventureScreen.Instance.MovePlayer(new Vector2((float)x, (float)y));
		}

		// Gives the player the named item
		public void GainItem(string itemName)
		{
			GameState.Instance.GainItem(new Item(itemName));
		}

		// Moves the player to room "roomName" and entrance "entranceName"
		public void ChangeRoom(string roomName, string entranceName)
		{
			GameState.Instance.ChangeRoom(roomName, entranceName);
			Player.Instance.StopMoving();
		}
	}
}

