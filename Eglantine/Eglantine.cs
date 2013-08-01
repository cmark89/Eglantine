#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using LuaInterface;
using Eglantine.Engine;
using Eglantine.Engine.Pathfinding;

#endregion

namespace Eglantine
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Eglantine : Game
	{
		// Temporarily lowered for development purposes
		public const int GAME_WIDTH = 1024;
		public const int GAME_HEIGHT = 700;


		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// Bad practice, but make this static for testing purposes.
		// Probably put this into a singleton
		public static Lua Lua;
		Scene currentScene;


		public Eglantine ()
		{
			IsMouseVisible = true;
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	     
			ContentLoader.Initialize(Content);
			graphics.PreferredBackBufferWidth = GAME_WIDTH;
			graphics.PreferredBackBufferHeight = GAME_HEIGHT;
			graphics.IsFullScreen = false;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// Temporary...Make this a LuaManager class or a wrapper or something.
			Lua = new Lua();

			// Load lua setup here.
			Console.WriteLine("Load setup.lua...");
			Lua.DoFile("Data/setup.lua");
			Console.WriteLine("setup.lua should have loaded.");

			EventManager.Initialize();
			AudioManager.Instance.Initialize();

			ChangeScene(new GameScene(GameState.NewGameState()));

			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// FPS check
			//Console.Clear();
			//Console.WriteLine(1f / (float)gameTime.ElapsedGameTime.TotalSeconds);

			// Update mouse input.
			MouseManager.Update(gameTime);
			KeyboardManager.Update(gameTime);

			currentScene.Update(gameTime);

			// For the love of GOD find a way to split this between screens, this is broken as it is
			double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
			//Lua.DoString("updateCoroutines(" + deltaTime + ")");


			base.Update (gameTime);

		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			//Console.WriteLine(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
			graphics.GraphicsDevice.Clear (Color.Black);

			spriteBatch.Begin ();
			currentScene.Draw(spriteBatch);
			spriteBatch.End ();
            
			base.Draw (gameTime);
		}

		public void ChangeScene(Scene newScene)
		{
			if(currentScene != null)
				currentScene.Unload();

			currentScene = newScene;
		}
	}
}

