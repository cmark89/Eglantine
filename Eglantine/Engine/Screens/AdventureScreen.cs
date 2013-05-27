using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Eglantine.Engine
{
	public class AdventureScreen : Screen
	{
		public Room CurrentRoom { get; private set; }
		// Player Player;
		// GameScene
		// GameState
		

		public override void Initialize()
		{
			// Set up the adventure screen here.
		}

		public override void Update (GameTime gameTime)
		{
			if (ReceivingInput)
			{
				// Process all player input here
			}

			// Now the other updates go here anyways.
			CurrentRoom.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the background layers
			foreach(RoomLayer rl in CurrentRoom.Background)
				rl.Draw(spriteBatch);

			// Draw all midground layers that are in front of the player
			//foreach(RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff < Player.Y)) rl.Draw(spriteBatch);


			// Draw the player
			//Player.Draw(spriteBatch);

			// Draw all midground layers that are behind the player
			//foreach(RoomLayer rl in CurrentRoom.Midground.FindAll(x => x.YCutoff >= Player.Y)) rl.Draw(spriteBatch);

			// Draw the foreground layers
			foreach(RoomLayer rl in CurrentRoom.Foreground)
				rl.Draw(spriteBatch);
		}
	}
}