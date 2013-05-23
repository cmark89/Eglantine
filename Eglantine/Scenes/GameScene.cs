using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine;


namespace Eglantine
{
	public class GameScene : Scene
	{
		// Stores a list of the screens that are active
		public List<Screen> GameScreens;

		// Stores the player character
		public Player Player;

		// Stores the GameState
		public GameState GameState { get; private set; }





		public GameScene ()
		{
		}


		public override void Initialize()
		{
			GameScreens = new List<Screen>();
			GameScreens.Add(new AdventureScreen());
		}

		public override void Update(GameTime gameTime)
		{
			foreach(Screen s in GameScreens)
			{
				// Loop through each screen and ensure that only the "top" screen can receive input
				if(GameScreens[GameScreens.Count - 1] == s)
					s.ReceivingInput = true;
				else
					s.ReceivingInput = false;

				s.Update(gameTime);
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw all GameScreens from the bottom up.
			for(int i = GameScreens.Count - 1; i > 0; i++)
				GameScreens[i].Draw(spriteBatch);

			// Draw the message queue
		}

		public override void Unload()
		{

		}
	}
}

