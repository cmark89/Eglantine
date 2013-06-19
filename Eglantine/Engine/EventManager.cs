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
			Item newItem = new Item(itemName);
			GameState.Instance.GainItem(newItem);
		}

		// Destroys the named item
		public void DestroyItem (string itemName)
		{
			GameState.Instance.DestroyItem(itemName);
		}

		// Enable and disable triggers / interactable objects
		public void EnableInteractable (string objectName)
		{
			GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == objectName).Enable();
		}

		public void DisableInteractable (string objectName)
		{
			GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == objectName).Disable();
		}

		public void EnableTrigger (string objectName)
		{
			GameState.Instance.CurrentRoom.TriggerAreas.Find(x => x.Name == objectName).Enable();
		}

		public void DisableTrigger (string objectName)
		{
			GameState.Instance.CurrentRoom.TriggerAreas.Find(x => x.Name == objectName).Disable();
		}

		// Moves the player to room "roomName" and entrance "entranceName"
		public void ChangeRoom(string roomName, string entranceName)
		{
			GameState.Instance.ChangeRoom(roomName, entranceName);
			Player.Instance.StopMoving();
		}


		// Returns true if the item is currently active (being clicked with)
		public bool UsingItem(string itemName)
		{
			Console.WriteLine("check...");
			return (AdventureScreen.Instance.LoadedItem.Name == itemName);
		}
	}
}

