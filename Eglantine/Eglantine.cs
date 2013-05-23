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
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
			graphics.IsFullScreen = false;

			testRoom = new Room("testroom");
			testnavmesh = testRoom.Navmesh;

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
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
			bool clicked = (Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released);
			if (clicked && testnavmesh.ContainingPolygon(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)) != null)
			{
				pather.nextWaypoint = null;
				pather.Waypoints = testnavmesh.GetPath(pather.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
			}
			lastMouseState = Mouse.GetState();

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
			spriteBatch.Draw (TestRoom, Vector2.Zero, Color.White);

			pather.Draw(spriteBatch);
			spriteBatch.End ();
		
			//TODO: Add your drawing code here
            
			base.Draw (gameTime);
		}
	}
}

