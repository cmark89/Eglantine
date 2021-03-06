using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ObjectivelyRadical.Scheduler;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

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
				{
					Console.WriteLine("EVENT MANAGER IS NULL!");
					_instance = new EventManager();
				}

				return _instance;
			}
		}

		public static void Initialize ()
		{
			_instance = new EventManager();
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

		// Changes the document's nth page to the texture pointed to by 'newPage'
		public void ChangeDocumentPage (string documentName, int pageNumber, string newPage)
		{
			Document thisDoc = GameState.Instance.Documents.Find (x => x.Name == documentName);
			if (thisDoc != null)
			{
				thisDoc.ChangePage(pageNumber, newPage);
			}
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

		public void FadeInteractable (string objectName, float time, float fadeAmount)
		{
			Color color = new Color(fadeAmount, fadeAmount, fadeAmount, fadeAmount);
			GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == objectName).LerpColor (color, time);
		}

		public void FadeInInteractable (string objectName, float time)
		{
			GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == objectName).LerpColor (Color.White, time);
		}

		public void FadeOutInteractable (string objectName, float time)
		{
			GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == objectName).LerpColor (Color.Transparent, time);
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
			Scheduler.SendSignal("Changed rooms");
		}

		public void StopPlayer()
		{
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

		public bool PlayerHasItem(string itemName)
		{
			return GameState.Instance.PlayerHasItem(itemName);
		}

		public bool PlayerInRoom(string roomName)
		{
			return GameState.Instance.CurrentRoom.Name == roomName;
		}

		// Returns the Active state of the named interactable
		public bool InteractableIsActive (string objectName)
		{
			return GameState.Instance.CurrentRoom.Interactables.Find(x => x.Name == objectName).Active;
		}

		// This sends a signal to Lua, allowing coroutines to know what's happening.
		public void SendSignal(string signal)
		{
			Console.WriteLine("Send signal: " + signal);
			Scheduler.SendSignal(signal);
		}

		// Play a sound effect
		public void PlaySound(string soundName, float volume = 1f, float pitch = 0f, float pan = 0f)
		{
			AudioManager.Instance.PlaySoundEffect(soundName, volume, pitch, pan);
		}

		// Play a looping sound effect
		public void PlaySoundLooping(string soundName, float volume = 1f, float pitch = 0f, float pan = 0f)
		{
			AudioManager.Instance.PlayLoopingSoundEffect(soundName, volume, pitch, pan);
		}

		public void FadeMusic (float targetVolume, float duration)
		{
			AudioManager.Instance.FadeMusic (targetVolume, duration);
		}

		public void StopMusic()
		{
			AudioManager.Instance.StopMusic();
		}

		public void StopLoopingSoundEffects()
		{
			AudioManager.Instance.StopLoopingSoundEffects();
		}

		public void StopSoundEffect (string name)
		{
			AudioManager.Instance.StopSoundEffect(name);
		}

		// Begin playing a song
		public void PlaySong(string songName, float volume = 1f, bool loop = true, float pitch = 0f, float pan = 0f)
		{
			AudioManager.Instance.PlaySong(songName, volume, loop, pitch, pan);
		}

		// Bring up a document screen
		public void ViewDocument (string docName)
		{
			Document namedDoc = GameState.Instance.Documents.Find (x => x.Name == docName);

			if (namedDoc != null) 
			{
				Console.WriteLine(namedDoc.Name + " exists!  Create document screen.");
				GameScene.Instance.AddScreen(new DocumentScreen(namedDoc));
			} 
			else 
			{
				Console.WriteLine(docName + " not in GameState's document list.");
			}
		}

		public void OpenPuzzlebox()
		{
			Console.WriteLine("Open the puzzlebox screen!");
			GameScene.Instance.AddScreen(new PuzzleboxScreen(GameState.Instance.PuzzleboxState));
		}

		public void OpenSafe()
		{
			Console.WriteLine("Open the safe screen!");
			GameScene.Instance.AddScreen(new SafeScreen(GameState.Instance.SafeState));
		}

		public void OpenTV()
		{
			Console.WriteLine("Open the TV screen!");
			GameScene.Instance.AddScreen(new TVScreen());
		}

		public void SetTVImage (string path)
		{
			GameState.Instance.TVImage = ContentLoader.Instance.LoadTexture2D (path);
		}
		
		public void InsertPuzzleboxKey()
		{
			GameState.Instance.PuzzleboxState.KeyInserted = true;
		}

		public void EnableSaving()
		{
			GameScene.Instance.EnableSaving();
		}

		public void DisableSaving()
		{
			GameScene.Instance.DisableSaving();
		}

		// This is changes the usage type of an item in the player's inventory.
		public void SetItemType (string itemName, string newType)
		{
			ItemType type = ItemType.Unusable;

			switch (newType)
			{
			case "Unusable":
				type = ItemType.Unusable;
				break;
			case "Immediate":
				type = ItemType.Immediate;
				break;
			case "Active":
				type = ItemType.Active;
				break;
			default:
				break;
			}

			List<Item> items = GameState.Instance.PlayerItems.FindAll (x => x.Name == itemName);
			if (items.Count > 0)
			{
				foreach(Item i in items)
					i.SetType(type);
			}
		}

		#region StoryScene events

		public void SetStoryImage(string imagePath, float r = 0, float g = 0, float b = 0, float a = 0)
		{
			Console.WriteLine ("Set the story image!");
			Color newColor = new Color(r,g,b,a);

			Texture2D image = ContentLoader.Instance.LoadTexture2D(imagePath);
			StoryScene.Instance.SetImage (image, newColor);
		}

		public void LerpStoryColor(float r, float g, float b, float a, float duration)
		{
			StoryScene.Instance.LerpColor (new Color(r,g,b,a), duration);
		}

		public void ShowStoryMessage(string message, float speed = .07f)
		{
			Console.WriteLine ("EVENT: SHOW STORY MESSAGE.");
			StoryScene.Instance.ShowMessage(message, speed);
		}

		public void PlayStorySequence(string scriptName)
		{
			Eglantine.ChangeScene (new StoryScene(scriptName));
		}

		#endregion

		#region Main Menu Events

		public void MainMenuFadeIn(string name, float duration)
		{
			MainMenuScene.Instance.FadeInElement (name, duration);
		}

		public void MainMenuFadeOut(string name, float duration)
		{
			MainMenuScene.Instance.FadeOutElement (name, duration);
		}

		public void NextMenuPhase()
		{
			MainMenuScene.Instance.NextPhase();
		}

		public void ShowMainMenu()
		{
			MainMenuScene.Instance.ShowMenu();
		}

		public void MainMenuHideElement(string name)
		{
			MainMenuScene.Instance.HideElement (name);
		}

		public void NewGame ()
		{
			Eglantine.ChangeScene (new GameScene ());
		}

		public void LockMenuInput()
		{
			MainMenuScene.Instance.MenuInput = false;
		}

		public void RollCredits ()
		{
			AudioManager.Instance.StopMusic();
			Eglantine.ChangeScene(new CreditScene());
		}

		public void PlayFootprintSound()
		{
			AudioManager.Instance.PlaySoundEffect(GameState.Instance.CurrentRoom.GetFootprintSound(GameState.Instance.PlayerPosition), .35f, 0f, 0f);
		}

		public void SetFacing(Facing dir)
		{
			Player.Instance.SetFacing(dir);
		}

		public void IdleAnimation()
		{
			Player.Instance.Sprite.PlayAnimation("Idle" + Player.Instance.CurrentFacing.ToString());
		}

		#endregion
	}
}

