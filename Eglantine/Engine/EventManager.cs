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
			AdventureScreen.Instance.MovePlayer(new Vector2((float)x, (float)y), true);
		}

		// Forces the player to move to the given point.
		public void MovePlayerTo(string name)
		{
			if(GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == name) != null)
			{
				Vector2 pos = GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == name).InteractPoint;
				AdventureScreen.Instance.MovePlayer(new Vector2(pos.X, pos.Y), true);
			}

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
			Console.WriteLine("Enable " + objectName);
			GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == objectName).Enable();
		}

		public void DisableInteractable (string objectName)
		{
			Console.WriteLine("Disable " + objectName);
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

		public void EnableInput ()
		{
			AdventureScreen.Instance.EnableInput();
		}

		public void DisableInput ()
		{
			AdventureScreen.Instance.DisableInput();
		}


		// Returns true if the item is currently active (being clicked with)
		public bool UsingItem(string itemName)
		{
			if(AdventureScreen.Instance.LoadedItem != null)
				return (AdventureScreen.Instance.LoadedItem.Name == itemName);
			else
				return false;
		}

		// This sends a signal to Lua, allowing coroutines to know what's happening.
		public void SendSignal(string signal)
		{
			Console.WriteLine("Send signal: " + signal);
			Eglantine.Lua.DoString("sendSignal(\"" + signal + "\")");
		}

		// Play a sound effect
		public void PlaySound(string soundName, float volume = 1f, float pitch = 0f, float pan = 0f)
		{
			AudioManager.Instance.PlaySoundEffect(soundName, volume, pitch, pan);
		}

		// Begin playing a song
		public void PlaySong(string songName, float volume = 1f, bool loop = true, float pitch = 0f, float pan = 0f)
		{
			AudioManager.Instance.PlaySong(songName, volume, loop, pitch, pan);
		}
	}
}

