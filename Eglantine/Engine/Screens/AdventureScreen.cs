using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Eglantine.Engine
{
	public class AdventureScreen : Screen
	{
		Player Player;	
		GUI Gui;

		Room CurrentRoom
		{ 
			get { return GameState.Instance.CurrentRoom; }
		}

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

			if (ReceivingInput)
			{
				// Don't let the player move around if they're interacting with the GUI
				if(!Gui.MouseInGUI)
				{
					// Process all player input here

					// Check where the mouse is and what mouse icon to display

					// If the player's mouse is in the walkable area...
					if(MouseManager.LeftClickDown && CurrentRoom.Navmesh.ContainingPolygon(MouseManager.Position) != null)
					{
						MovePlayer(MouseManager.Position);
					}
				}

				// Only do this if the screen is taking input!
				Gui.Update(gameTime);

			}

			// Now the other updates go here anyways.
			CurrentRoom.Update(gameTime);

			// Update the player.
			Player.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the background layers
			foreach(RoomLayer rl in CurrentRoom.Background)
				rl.Draw(spriteBatch);

			// Draw all midground layers that are behind the player
			foreach(RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff < Player.Position.Y))
				rl.Draw(spriteBatch);

			// Draw the player
			Player.Draw(spriteBatch);

			// Draw all midground layers that are in front of the player
			foreach(RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff >= Player.Position.Y))
				rl.Draw(spriteBatch);

			// Draw the foreground layers
			foreach(RoomLayer rl in CurrentRoom.Foreground)
				rl.Draw(spriteBatch);

			Gui.Draw(spriteBatch);
		}

		// This method is static to ensure that the EventManager is able to force the player to move.
		public void MovePlayer (Vector2 targetPoint, bool uninteruptable = false)
		{
			Player.SetPath (CurrentRoom.Navmesh.GetPath (Player.Position, targetPoint));

			if (uninteruptable)
			{
				// Disable the ability to give the player orders until they have reached their destination.
			}
		}
	}
}