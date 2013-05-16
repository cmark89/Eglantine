#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using LuaInterface;
using Eglantine.Engine.Pathfinding;

#endregion

namespace Eglantine
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Eglantine : Game
	{
		Texture2D title;
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Eglantine ()
		{
			IsMouseVisible = true;
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	     
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
			graphics.IsFullScreen = false;

			Lua lua = new Lua();
			lua.DoFile("Data/rooms.lua");

			new Navmesh(lua.GetTable("rooms.testroom.Navmesh"));

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
			title = Content.Load<Texture2D>("testTitle");

			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
				Exit ();
			}
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
			spriteBatch.Draw (title, Vector2.Zero, Color.White);
			spriteBatch.End ();
		
			//TODO: Add your drawing code here
            
			base.Draw (gameTime);
		}
	}
}

