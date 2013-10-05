using System;
using Eglantine.Engine;
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
	public class AdventureScreen : Screen
	{
		// This almost implements a singleton pattern in order to prevent using a static method for moving the player.
		// The Instance accessor does not create the instance if it does not exist, however.
		private static AdventureScreen _instance;
		public static AdventureScreen Instance
		{
			get
			{
				if(_instance != null)
					return _instance;
				else
					return null;
			}
		}

		Room CurrentRoom
		{ 
			get { return GameState.Instance.CurrentRoom; }
		}

		public bool InputDisabled { get; private set; }

		Player Player;	
		GUI Gui;
		int clearLoadedItemIn = -1;
		public Item LoadedItem { get; private set; }
		public Trigger HighlightedTrigger { get; private set;}

		private bool loadingFinished = false;

		public bool MouseInGui 
		{
			get { return Gui.MouseInGUI; } 
		}

		private bool paused = false;


		public override void Initialize()
		{
			_instance = this;
			// Set up the adventure screen here.

			// Initialize the player.
			Player = Player.Instance;
			Gui = new GUI();
			//Player.Setup();

			//Console.WriteLine("Load setup.lua...");
			//Eglantine.Lua.DoFile("Data/setup.lua");
			//Console.WriteLine("setup.lua should have loaded.");
		}

		public override void Update (GameTime gameTime)
		{
			if (!loadingFinished)
			{
				loadingFinished = true;
				return;
			}

			// Because of script delay, we need 2 frames to clear the item.
			if (clearLoadedItemIn > 0)
			{
				clearLoadedItemIn--;
				if(clearLoadedItemIn == 0)
				{
					LoadedItem = null;
					clearLoadedItemIn = -1;
				}
			}

			HighlightedTrigger = null;

			if (ReceivingInput && !InputDisabled)
			{
				// Don't let the player move around if they're interacting with the GUI
				if (!Gui.MouseInGUI)
				{
					// See what object, if any, the player is hovering over.
					for (int i = CurrentRoom.Interactables.Count; i > 0; i--)
					{
						Trigger thisTrigger = CurrentRoom.Interactables [i - 1];
						if (thisTrigger.Active && thisTrigger.VectorInArea (MouseManager.Position))
						{
							// Set the mouse to be cool
							HighlightedTrigger = thisTrigger;
							break;
						}
					}

					// Process all player input here

					// Check where the mouse is and what mouse icon to display

					// If the player's mouse is in the walkable area...
					if (LoadedItem == null)
					{
						if (MouseManager.LeftClickUp && CurrentRoom.Navmesh.ContainingPolygon (MouseManager.Position) != null && CurrentRoom.PointIsWalkable (MouseManager.Position))
						{
							MovePlayer (MouseManager.Position);
						}
					}

					
					if (MouseManager.RightClickUp && LoadedItem != null)
					{
						SetActiveItem (null);
					}
				}

				// Call the menu!
				if (KeyboardManager.ButtonPressUp (Microsoft.Xna.Framework.Input.Keys.Escape))
				{
					GameScene.Instance.AddScreen (new MenuScreen ());
				}
			}

			if (ReceivingInput)
			{
				Gui.Update (gameTime);

				Scheduler.Update(gameTime.ElapsedGameTime.TotalSeconds);
			}

			if (!ReceivingInput && LoadedItem != null)
			{
				// Clear the loaded item if this screen loses focus
				LoadedItem = null;
			}

			// Update the current room
			if(CurrentRoom != null)
				CurrentRoom.Update (gameTime);

			// If nothing is being hovered over, set the mouse to its normal graphic
			if (HighlightedTrigger == null && !MouseInGui)
			{
				MouseManager.MouseMode = MouseInteractMode.Normal;
			}

			if (ReceivingInput)
			{
				// Update the player.
				if(Player != null)
					Player.Update (gameTime);

				// Update the play time.
				GameState.Instance.AddGameTime (gameTime.ElapsedGameTime.TotalSeconds);
			}



			// Finally, clear the current item if a left click was registered and the mouse is not in the GUI
			if(MouseManager.LeftClickUp && !MouseInGui)
				clearLoadedItemIn = 2;
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			// Draw the background layers
			foreach (RoomLayer rl in CurrentRoom.Background)
				rl.Draw (spriteBatch);

			// Draw all midground layers that are behind the player
			foreach (RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff < Player.Position.Y))
				rl.Draw (spriteBatch);

			foreach (Interactable i in CurrentRoom.Interactables.FindAll(x => x.Active && x.IsDrawn && x.YCutoff <= Player.Position.Y))
			{
				i.Draw (spriteBatch);
			}

			// Draw the player
			Player.Draw (spriteBatch);

			// Draw all midground layers that are in front of the player
			foreach (RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff >= Player.Position.Y))
				rl.Draw (spriteBatch);

			foreach (Interactable i in CurrentRoom.Interactables.FindAll(x => x.Active && x.IsDrawn && x.YCutoff > Player.Position.Y))
			{
				i.Draw (spriteBatch);
			}

			// Draw the foreground layers
			foreach (RoomLayer rl in CurrentRoom.Foreground)
				rl.Draw (spriteBatch);

			Gui.Draw (spriteBatch);


			// Draw the current used item if it exists
			if (LoadedItem != null)
			{
				spriteBatch.Draw (LoadedItem.Texture, position: MouseManager.Position, color: Color.Gold);
			}
		}

		// This method is static to ensure that the EventManager is able to force the player to move.
		public void MovePlayer (Vector2 targetPoint, bool uninteruptable = false)
		{
			if (uninteruptable)
			{
				DisableInput();
			}

			Player.SetPath (CurrentRoom.Navmesh.GetPath (Player.Position, targetPoint), uninteruptable);
		}

		public void DisableInput ()
		{
			InputDisabled = true;
		}

		public void EnableInput()
		{
			InputDisabled = false;
		}

		public void SetActiveItem(Item i)
		{
			LoadedItem = i;
		}

		public void OneFrameInputDisable()
		{
			loadingFinished = false;
		}

		public static void Clear()
		{
			_instance = null;
		}
	}
}