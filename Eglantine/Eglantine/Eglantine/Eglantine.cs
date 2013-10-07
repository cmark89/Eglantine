#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;
using Eglantine.Engine;
using Eglantine.Engine.Pathfinding;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

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

		private static Eglantine _thisGame;
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D loadingScreen;

		public static bool LoadingScreenShown = false;

		// Bad practice, but make this static for testing purposes.
		// Probably put this into a singleton
		public static Lua MainLua;
		static Scene currentScene;


		public Eglantine ()
		{
			_thisGame = this;

			IsMouseVisible = false;
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
			// Initialize the core Lua instance
			MainLua = new Lua();

			// Load lua setup here.
			Console.WriteLine("Load mainSetup.lua...");
			MainLua.DoFile("Data/mainSetup.lua");
			Console.WriteLine("mainSetup.lua should have loaded.");

			loadingScreen = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/LoadingScreen");

			//EventManager.Initialize();
			AudioManager.Instance.Initialize();
			SaveManager.Initialize ();
			MouseManager.Initialize ();
			Scheduler.Initialize();

			ChangeScene(new MainMenuScene());
			//ChangeScene(new GameScene());

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
			AudioManager.Instance.Update (gameTime);

			if(SaveManager.Shown)
				SaveManager.Update (gameTime);

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
			if(LoadingScreenShown)
				spriteBatch.Draw (loadingScreen, position: Vector2.Zero, color: Color.White);
			else
				currentScene.Draw(spriteBatch);

			// This is kind of hackish
			if(SaveManager.Shown)
				SaveManager.Draw (spriteBatch);

			MouseManager.DrawMouse(spriteBatch);

			spriteBatch.End ();
            
			base.Draw (gameTime);
		}

		public static void ChangeScene(Scene newScene)
		{
			Scene previousScene = currentScene;
			currentScene = newScene;

			if(previousScene != null)
				previousScene.Unload();
		}

		public static void ExitGame()
		{
			_thisGame.Exit ();
		}

		public static void UnloadAllGameData()
		{
			GameState.Clear();
			Player.Clear();
			GameScene.Clear();
			AdventureScreen.Clear();
		}
	}

    public static class ExtensionMethods
    {
        // Overload for calling Draw() with named parameters
        /// <summary>
        /// This is a MonoGame Extension method for calling Draw() using named parameters.  It is not available in the standard XNA Framework.
        /// </summary>
        /// <param name='texture'>
        /// The Texture2D to draw.  Required.
        /// </param>
        /// <param name='position'>
        /// The position to draw at.  If left empty, the method will draw at drawRectangle instead.
        /// </param>
        /// <param name='drawRectangle'>
        /// The rectangle to draw at.  If left empty, the method will draw at position instead.
        /// </param>
        /// <param name='sourceRectangle'>
        /// The source rectangle of the texture.  Default is null
        /// </param>
        /// <param name='origin'>
        /// Origin of the texture.  Default is Vector2.Zero
        /// </param>
        /// <param name='rotation'>
        /// Rotation of the texture.  Default is 0f
        /// </param>
        /// <param name='scale'>
        /// The scale of the texture as a Vector2.  Default is Vector2.One
        /// </param>
        /// <param name='color'>
        /// Color of the texture.  Default is Color.White
        /// </param>
        /// <param name='effect'>
        /// SpriteEffect to draw with.  Default is SpriteEffects.None
        /// </param>
        /// <param name='depth'>
        /// Draw depth.  Default is 0f.
        /// </param>
        public static void Draw(this SpriteBatch sb, Texture2D texture,
                Vector2? position = null,
                Rectangle? drawRectangle = null,
                Rectangle? sourceRectangle = null,
                Vector2? origin = null,
                float rotation = 0f,
                Vector2? scale = null,
                Color? color = null,
                SpriteEffects effect = SpriteEffects.None,
                float depth = 0f)
        {

            // Assign default values to null parameters here, as they are not compile-time constants
            if (!color.HasValue)
                color = Color.White;
            if (!origin.HasValue)
                origin = Vector2.Zero;
            if (!scale.HasValue)
                scale = Vector2.One;

            // If both drawRectangle and position are null, or if both have been assigned a value, raise an error
            if ((drawRectangle.HasValue) == (position.HasValue))
            {
                throw new InvalidOperationException("Expected drawRectangle or position, but received neither or both.");
            }
            else if (position != null)
            {
                // Call Draw() using position
                sb.Draw(texture, (Vector2)position, sourceRectangle, (Color)color, rotation, (Vector2)origin, (Vector2)scale, effect, depth);
            }
            else
            {
                // Call Draw() using drawRectangle
                sb.Draw(texture, (Rectangle)drawRectangle, sourceRectangle, (Color)color, rotation, (Vector2)origin, effect, depth);
            }
        } 
    }
}

