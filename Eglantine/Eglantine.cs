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
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		MouseState lastMouseState;

		// Bad practice, but make this static for testing purposes.
		// Probably put this into a singleton
		public static Lua Lua;

		Scene currentScene;


		// Testing stuff here...

		Texture2D TestRoom;
		Navmesh testnavmesh;
		public static TestPather pather = new TestPather(new Vector2(800, 600));
		Room testRoom;

		public Eglantine ()
		{
			IsMouseVisible = true;
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	     
			ContentLoader.Initialize(Content);
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
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

			// Load all lua scripts here.
			Lua.DoFile("Data/setup.lua");
			Lua.DoFile("Data/rooms.lua");

			EventManager.Initialize();

			testRoom = new Room("testroom");
			testnavmesh = testRoom.Navmesh;

			// TODO: Add your initialization logic here
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
			TestRoom = Content.Load<Texture2D>("testroom");
			pather.LoadContent(Content);


			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// Update mouse input.
			MouseManager.Update(gameTime);

			if (MouseManager.LeftClickDown && testnavmesh.ContainingPolygon(MouseManager.Position) != null)
			{
				pather.nextWaypoint = null;
				pather.Waypoints = testnavmesh.GetPath(pather.Position, new Vector2(MouseManager.Position));
			}

			currentScene.Update(gameTime);

			testRoom.Update(gameTime);
			pather.Update(gameTime);

			// TODO: Add your update logic here			
			base.Update (gameTime);

		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			spriteBatch.Begin ();

			currentScene.Draw(spriteBatch);
			//spriteBatch.Draw (TestRoom, Vector2.Zero, Color.White);
			testRoom.Draw(spriteBatch);

			pather.Draw(spriteBatch);
			spriteBatch.End ();
            
			base.Draw (gameTime);
		}

		public void SetScene(Scene newScene)
		{
			if(currentScene != null)
				currentScene.Unload();

			currentScene = newScene;
		}
	}
}

