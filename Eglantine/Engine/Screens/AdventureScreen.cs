using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Eglantine.Engine
{
	public class AdventureScreen : Screen
	{
		Player Player;		
		Room CurrentRoom
		{ 
			get { return GameState.Instance.CurrentRoom; }
		}

		public override void Initialize()
		{
			// Set up the adventure screen here.

			// Initialize the player.
			Player = Player.Instance;
			//Player.Setup();
		}

		public override void Update (GameTime gameTime)
		{
			if (ReceivingInput)
			{
				// Process all player input here
			}

			// Now the other updates go here anyways.
			CurrentRoom.Update(gameTime);

			// Update the player.
			Player.Update();
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
		}
	}
}