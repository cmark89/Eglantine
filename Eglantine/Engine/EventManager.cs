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
			Console.WriteLine(message);
		}

		// Forces the player to move to the given point.
		public void MovePlayer(double x, double y)
		{
			Console.WriteLine("Move player to " + x + ":" + y);
		}

		public void GainItem(string itemName)
		{
			GameState.Instance.GainItem(new Item(itemName));
		}
	}
}

