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
			CurrentRoom.Draw(spriteBatch);

			// Draw the player in the room.
		}
	}
}