using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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
		public Item LoadedItem { get; private set; }
		public Trigger HighlightedTrigger { get; private set;}

		// Used to force vertices to draw
		private bool drawingVertices = false;


		public override void Initialize()
		{
			_instance = this;
			// Set up the adventure screen here.

			// Initialize the player.
			Player = Player.Instance;
			Gui = new GUI();
			//Player.Setup();
		}

		public override void Update (GameTime gameTime)
		{

			if (ReceivingInput && !InputDisabled)
			{
				// Don't let the player move around if they're interacting with the GUI
				if(!Gui.MouseInGUI)
				{
					// See what object, if any, the player is hovering over.
					for(int i = CurrentRoom.Interactables.Count; i > 0; i--)
					{
						Trigger thisTrigger = CurrentRoom.Interactables[i-1];
						if(thisTrigger.Active && thisTrigger.VectorInArea(MouseManager.Position))
						{
							HighlightedTrigger = thisTrigger;
							break;
						}
					}

					// Process all player input here

					// Check where the mouse is and what mouse icon to display

					// If the player's mouse is in the walkable area...
					if(MouseManager.LeftClickDown && CurrentRoom.Navmesh.ContainingPolygon(MouseManager.Position) != null)
					{
						MovePlayer(MouseManager.Position);
					}
					if(MouseManager.RightClickDown && CurrentRoom.Navmesh.ContainingPolygon(MouseManager.Position) != null && LoadedItem != null)
					{
						SetActiveItem(null);
					}
				}
			}

			if(ReceivingInput)
				Gui.Update(gameTime);

			// Now the other updates go here anyways.
			CurrentRoom.Update(gameTime);

			// Update the player.
			Player.Update(gameTime);
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			// Draw the background layers
			foreach (RoomLayer rl in CurrentRoom.Background)
				rl.Draw (spriteBatch);

			// Draw all midground layers that are behind the player
			foreach (RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff < Player.Position.Y))
				rl.Draw (spriteBatch);

			// Draw the player
			Player.Draw (spriteBatch);

			// Draw all midground layers that are in front of the player
			foreach (RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff >= Player.Position.Y))
				rl.Draw (spriteBatch);

			foreach (Interactable i in CurrentRoom.Interactables.FindAll(x => x.Active && x.IsDrawn))
			{
				i.Draw (spriteBatch);
			}

			// Draw the foreground layers
			foreach (RoomLayer rl in CurrentRoom.Foreground)
				rl.Draw (spriteBatch);

			if (drawingVertices)
			{
				CurrentRoom.Navmesh.Draw(spriteBatch);
			}

			Gui.Draw(spriteBatch);
		}

		// This method is static to ensure that the EventManager is able to force the player to move.
		public void MovePlayer (Vector2 targetPoint, bool uninteruptable = false)
		{
			Player.SetPath (CurrentRoom.Navmesh.GetPath (Player.Position, targetPoint), uninteruptable);

			if (uninteruptable)
			{
				DisableInput();
			}
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
	}
}