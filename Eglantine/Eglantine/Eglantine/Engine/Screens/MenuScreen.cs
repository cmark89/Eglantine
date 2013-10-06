using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine;

namespace Eglantine
{
	public class MenuScreen : Screen
	{
		private const int Y_PADDING = 10;

		private bool loadFinished = false;
		private bool drawn;

		// Textures to load for the menu
		private static Texture2D ResumeButton;
		private static Texture2D SaveButton;
		private static Texture2D LoadButton;
		private static Texture2D ExitButton;

		private static Rectangle resumeRect;
		private static Rectangle saveRect;
		private static Rectangle loadRect;
		private static Rectangle exitRect;

		public MenuScreen ()
		{
			Initialize();
		}

		public override void Initialize()
		{
			LoadContent();

			Point screenOrigin = new Point(Eglantine.GAME_WIDTH/2, Eglantine.GAME_HEIGHT/2);
			int xOffset = (int)(ResumeButton.Width / 2);
			int width = ResumeButton.Width;
			int height = ResumeButton.Height;

			resumeRect = new Rectangle(screenOrigin.X - xOffset, screenOrigin.Y - (height * 2) - (Y_PADDING * 2), width, height);
			saveRect = new Rectangle(screenOrigin.X - xOffset, screenOrigin.Y - height - Y_PADDING, width, height);
			loadRect = new Rectangle(screenOrigin.X - xOffset, screenOrigin.Y, width, height);
			exitRect = new Rectangle(screenOrigin.X - xOffset, screenOrigin.Y + height + Y_PADDING, width, height);

			drawn = true;
		}

		public void LoadContent ()
		{
			if (ResumeButton == null)
			{
				ResumeButton = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/ResumeButton");
				SaveButton = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/SaveButton");
				LoadButton = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/LoadButton");
				ExitButton = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/ExitButton");
			}
		}

		public override void Update (GameTime gameTime)
		{
			// Handle input here
			if (ReceivingInput && drawn)
			{
				if(KeyboardManager.ButtonPressUp(Microsoft.Xna.Framework.Input.Keys.Escape) && loadFinished)
					Resume ();


				if(resumeRect.Contains((int)MouseManager.X, (int)MouseManager.Y))
				{
					MouseManager.MouseMode = MouseInteractMode.Hot;
					if (MouseManager.LeftClickUp) Resume ();
				}
				if(saveRect.Contains((int)MouseManager.X, (int)MouseManager.Y) && GameScene.Instance.SavingAllowed)
				{
					MouseManager.MouseMode = MouseInteractMode.Hot;
					if (MouseManager.LeftClickUp) Save ();
				}
				if(loadRect.Contains((int)MouseManager.X, (int)MouseManager.Y))
				{
					MouseManager.MouseMode = MouseInteractMode.Hot;
					if (MouseManager.LeftClickUp) Load ();
				}
				if(exitRect.Contains((int)MouseManager.X, (int)MouseManager.Y))
				{
					MouseManager.MouseMode = MouseInteractMode.Hot;
					if (MouseManager.LeftClickUp) Exit ();
				}
			}

			if(!loadFinished)
				loadFinished = true;
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			if (drawn)
			{
				Color drawColor = Color.Gray;
				
				if(resumeRect.Contains((int)MouseManager.X, (int)MouseManager.Y))
					drawColor = Color.White;
				else
					drawColor = Color.Gray;
				
				spriteBatch.Draw (ResumeButton, drawRectangle: resumeRect, color: drawColor);
				
				if(saveRect.Contains((int)MouseManager.X, (int)MouseManager.Y) && GameScene.Instance.SavingAllowed)
					drawColor = Color.White;
				else
					drawColor = Color.Gray;
				
				spriteBatch.Draw (SaveButton, drawRectangle: saveRect, color: drawColor);
				
				if(loadRect.Contains((int)MouseManager.X, (int)MouseManager.Y))
					drawColor = Color.White;
				else
					drawColor = Color.Gray;
				
				spriteBatch.Draw (LoadButton, drawRectangle: loadRect, color: drawColor);
				
				if(exitRect.Contains((int)MouseManager.X, (int)MouseManager.Y))
					drawColor = Color.White;
				else
					drawColor = Color.Gray;
				
				spriteBatch.Draw (ExitButton, drawRectangle: exitRect, color: drawColor);
			}
		}

		public void Resume()
		{
			// Resume the game by closing the window
			FlaggedForRemoval = true;
		}

		public void Save()
		{
			// Save the game
			GameState.SaveState ();

			// Possibly resume after saving
			Resume ();
		}

		public void Load()
		{
			// Block input for this menu so the save viewer can run correctly
			drawn = false;
			ReceivingInput = false;
			SaveManager.ToggleLoadScreen(OnLoadScreenClose);
		}

		public void OnLoadScreenClose ()
		{
			Console.WriteLine ("Callback reached");
			drawn = true;
			ReceivingInput = true;
			loadFinished = false;
		}

		public void Exit()
		{
			// Exit the game!
			Eglantine.ExitGame ();
		}
	}
}

