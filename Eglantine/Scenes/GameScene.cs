using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine;


namespace Eglantine
{
	public class GameScene : Scene
	{
		// Semi-singleton?
		private static GameScene _instance;
		public static GameScene Instance
		{
			get
			{
				if(_instance != null)
					return _instance;
				else
					return null;
			}
		}



		// Stores a list of the screens that are active
		public List<Screen> GameScreens;

		// Stores the player character
		public Player Player;

		// Stores the GameState
		public GameState GameState { get; private set; }


		// Create a new GameScene around a given GameState intance
		public GameScene (GameState newGameState)
		{
			_instance = this;
			GameState = newGameState;
			Initialize();
		}


		public override void Initialize()
		{
			GameScreens = new List<Screen>();

			//MessageManager.Instance.Initialize();
			AdventureScreen advScreen = new AdventureScreen();
			advScreen.Initialize();
			GameScreens.Add(advScreen);
		}

		public override void Update(GameTime gameTime)
		{
			RemoveFinishedScreens();

			for(int i = 0; i < GameScreens.Count; i++)
			{
				// Loop through each screen and ensure that only the "top" screen can receive input
				if(GameScreens[GameScreens.Count - 1] == GameScreens[i])
					GameScreens[i].ReceivingInput = true;
				else
					GameScreens[i].ReceivingInput = false;

				GameScreens[i].Update(gameTime);
			}
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			// Draw all GameScreens from the bottom up.
			foreach (Screen s in GameScreens)
			{
				s.Draw(spriteBatch);
			}
			//for(int i = GameScreens.Count; i > 0; i--)
				//GameScreens[i-1].Draw(spriteBatch);

			// Draw the message queue
		}

		public override void Unload()
		{

		}

		// Pushes a new screen onto the list
		public void AddScreen(Screen screen)
		{
			GameScreens.Add(screen);
		}

		public void RemoveFinishedScreens ()
		{
			foreach (Screen s in GameScreens.FindAll(x => x.FlaggedForRemoval))
			{
				GameScreens.Remove(s);
			}
		}
	}
}

