using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine;
using LuaInterface;


namespace Eglantine
{
	public class GameScene : Scene
	{
		// Semi-singleton?
		public static Lua Lua { get; private set; }
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

		public bool SavingAllowed { get; private set; }
		private bool isNewGame;
		private bool setupComplete = false;


		// Creates a new GameScene and creates a new GameState
		public GameScene()
		{
			isNewGame = true;
			_instance = this;
			GameState = new GameState();
			Initialize ();
		}

		// Create a new GameScene around a given GameState intance
		public GameScene (GameState newGameState)
		{
			isNewGame = false;
			_instance = this;
			GameState = newGameState;
			Initialize();
		}


		public override void Initialize ()
		{
			// Set the loading screen on 
			Eglantine.LoadingScreenShown = true;

			// Make sure the lua side knows about this GameState
			//Eglantine.Lua.DoString("loadGameState()");
			Lua = new Lua ();
			Console.WriteLine ("GameScene : load gameSetup.lua...");
			Lua.DoFile ("Data/gameSetup.lua");
			Console.WriteLine ("gameSetup.lua should have loaded.");
			GameScene.Lua.DoString ("loadGameState()");
			GameScene.Lua.DoString ("loadEventManager()");

			// Set up the Event Manager
			EventManager.Initialize ();

			//Create the list of game screens
			GameScreens = new List<Screen> ();

			if (isNewGame)
			{
				Console.WriteLine ("Setup new GameState");
				// Setup the new game state if it needs to be setup
				GameState.InitializeNewGame ();
			}
			else
			{
				Console.WriteLine ("LOAD ALL OBJECTS FROM SERIALIZATION");
				GameState.LoadObjectsFromSerialization();
			}

			//MessageManager.Instance.Initialize();
			AdventureScreen advScreen = new AdventureScreen();
			advScreen.Initialize();
			GameScreens.Add(advScreen);

			SavingAllowed = true;
			setupComplete = true;
			Eglantine.LoadingScreenShown = false;

			if(!isNewGame)
			{
				// If the game was loaded, run OnLoad for the current room 
				GameState.Instance.CurrentRoom.OnLoadRoom ();
			}
		}

		public override void Update (GameTime gameTime)
		{
			if (!setupComplete)
			{
				// Update loading text?
				return;
			}

			for(int i = 0; i < GameScreens.Count; i++)
			{
				// Loop through each screen and ensure that only the "top" screen can receive input
				if(GameScreens[GameScreens.Count - 1] == GameScreens[i])
					GameScreens[i].ReceivingInput = true;
				else
					GameScreens[i].ReceivingInput = false;

				GameScreens[i].Update(gameTime);
			}

			RemoveFinishedScreens();
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			if (!setupComplete)
			{
				// Draw the loading screen
				return;
			}

			// Draw all GameScreens from the bottom up.
			foreach (Screen s in GameScreens)
			{
				s.Draw(spriteBatch);
			}
			//for(int i = GameScreens.Count; i > 0; i--)
				//GameScreens[i-1].Draw(spriteBatch);
		}

		public override void Unload()
		{
		}

		// Pushes a new screen onto the list
		public void AddScreen(Screen screen)
		{
			GameScreens.Add(screen);
			AdventureScreen.Instance.OneFrameInputDisable();
		}

		public void RemoveFinishedScreens ()
		{
			foreach (Screen s in GameScreens.FindAll(x => x.FlaggedForRemoval))
			{
				GameScreens.Remove(s);
			}
		}

		public void EnableSaving()
		{
			SavingAllowed = true;
		}

		public void DisableSaving()
		{
			SavingAllowed = false;
		}
	}
}

