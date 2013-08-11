using System;
using System.Collections.Generic;
using Eglantine.Engine;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine
{
	public class SafeScreen : Screen
	{
		// Singletonize me, cap'n!
		private static SafeScreen _instance;
		public static SafeScreen Instance
		{
			get
			{
				if(_instance == null)
					_instance = new SafeScreen(null);

				return _instance;
			}
		}

		#region Textures and constant values
		private static Texture2D SafeTexture;
		private static Texture2D InsideSafeTexture;
		private static Texture2D KeyTexture;
		private static Texture2D FontTexture;
		private static Texture2D OpenButtonTexture;
		private static Texture2D CloseButtonTexture;



		#endregion

		public SafeState SafeState { get; protected set; }

		private Vector2 puzzleStart;

		private SafeScreenGUI window;
		private SafeScreenState screenState;

		private Rectangle openButtonRect;
		private Color openButtonColor;

		private Rectangle keyRect;

		private List<SafeButton> buttons;

		public SafeScreen (SafeState puzzleState)
		{
			_instance = this;
			SafeState = puzzleState;
			screenState = SafeScreenState.Closed;
			Initialize();
		}

		public override void Initialize ()
		{
			// Textures haven't been set yet...initialize!
			if (SafeTexture == null)
			{
				// Set all this stuff up, dingus
				SafeTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/safe");
				InsideSafeTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/insidesafe");
				KeyTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/keyinsafe");

				OpenButtonTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleBoxOpenButton");
				CloseButtonTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleBoxCloseButton");

				FontTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/LCDFont");
			}

			window = new SafeScreenGUI(SafeTexture);
			openButtonRect = window.GetOpenButtonRect();
			puzzleStart = window.PuzzleStartPosition();
			keyRect = OffsetRect(new Rectangle(261, 447, 230, 54), puzzleStart);


			// Initialize the buttons
			SetupButtons();
		}

		public void SetupButtons()
		{
			buttons = new List<SafeButton>();

			buttons.Add(new SafeButton(1, puzzleStart + new Vector2(39,196), 0, new Keys[] { Keys.D1 }));
			buttons.Add(new SafeButton(2, puzzleStart + new Vector2(139,196), 1, new Keys[] { Keys.D2 }));
			buttons.Add(new SafeButton(3, puzzleStart + new Vector2(239,196), 2, new Keys[] { Keys.D3 }));
			buttons.Add(new SafeButton(4, puzzleStart + new Vector2(39,281), 3, new Keys[] { Keys.D4, Keys.Q }));
			buttons.Add(new SafeButton(5, puzzleStart + new Vector2(139,281), 4, new Keys[] { Keys.D5, Keys.W }));
			buttons.Add(new SafeButton(6, puzzleStart + new Vector2(239,281), 5, new Keys[] { Keys.D6, Keys.E }));
			buttons.Add(new SafeButton(7, puzzleStart + new Vector2(39,366), 6, new Keys[] { Keys.D7, Keys.A }));
			buttons.Add(new SafeButton(8, puzzleStart + new Vector2(139,366), 7, new Keys[] { Keys.D8, Keys.S }));
			buttons.Add(new SafeButton(9, puzzleStart + new Vector2(239,366), 8, new Keys[] { Keys.D9, Keys.D }));
			buttons.Add(new SafeButton(0, puzzleStart + new Vector2(139,451), 9, new Keys[] { Keys.D0, Keys.X }));
			buttons.Add(new SafeButton(10, puzzleStart + new Vector2(39,451), 10, new Keys[] { Keys.Escape, Keys.Z }));
			buttons.Add(new SafeButton(11, puzzleStart + new Vector2(239,451), 11, new Keys[] { Keys.Enter, Keys.C }));
		}


		public override void Update (GameTime gameTime)
		{
			if (window != null)
			{
				window.Update (gameTime);
			}

			if (screenState == SafeScreenState.Open || SafeState.PuzzleSolved)
			{
				openButtonColor = Color.DimGray;
			}

			HandleInput();
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			if (window != null)
			{
				window.DrawBackground (spriteBatch);

				if(screenState == SafeScreenState.Closed)
					DrawSafe(spriteBatch);
				else
					DrawInsideSafe(spriteBatch);

				window.DrawFrame(spriteBatch);

				DrawOpenButton(spriteBatch);
			}
		}

		public void DrawSafe (SpriteBatch spriteBatch)
		{
			// Draw the actual puzzle components here...
			foreach (SafeButton b in buttons)
				b.Draw (spriteBatch);

			// Draw the LCD 
			Vector2 start = puzzleStart + new Vector2(50, 79);
			for (int i = 0; i < SafeState.CurrentGuess.Length; i++)
			{
				int thisNum = 0;
				Int32.TryParse(SafeState.CurrentGuess[i].ToString(), out thisNum);
				spriteBatch.Draw(FontTexture, sourceRectangle: GetLCDSourceRectangle(thisNum), position: new Vector2(start.X + 50 * i, start.Y));
			}
		}


		public void DrawOpenButton (SpriteBatch spriteBatch)
		{
			// Draw the button to open the box if the puzzle has been solved
			if (screenState == SafeScreenState.Closed && SafeState.PuzzleSolved)
			{
				spriteBatch.Draw(OpenButtonTexture, drawRectangle: OffsetRect(openButtonRect, puzzleStart), color: openButtonColor);
			}
			else if (screenState == SafeScreenState.Open)
			{
				spriteBatch.Draw(CloseButtonTexture, drawRectangle: OffsetRect(openButtonRect, puzzleStart), color: openButtonColor);
			}
		}


		public void DrawInsideSafe (SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (InsideSafeTexture, position: puzzleStart);

			if (!GameState.Instance.PlayerHasItem ("Key"))
			{
				spriteBatch.Draw (KeyTexture, drawRectangle: keyRect);
			}
		}

		public void HandleInput ()
		{
			bool openButtonDrawn = false;
			if (SafeState.PuzzleSolved || screenState == SafeScreenState.Open)
				openButtonDrawn = true;

			if (openButtonDrawn && MouseManager.MouseInRect (OffsetRect(openButtonRect, puzzleStart)))
			{
				openButtonColor = Color.White;
				if(MouseManager.LeftClickUp)
					OpenSafe();
			}

			if (screenState == SafeScreenState.Closed)
			{
				// Handle button presses
				foreach(SafeButton b in buttons)
				{
					b.Update();
				}
			}
			else
			{
				// Handle input for the --inside-- of the box
				if(!GameState.Instance.PlayerHasItem("Key") && MouseManager.MouseInRect(keyRect) && MouseManager.LeftClickUp)
				{
					EventManager.Instance.PlaySound("Extend");
					EventManager.Instance.GainItem("Key");
				}
			}
		}

		public void Close()
		{
			FlaggedForRemoval = true;
			EventManager.Instance.SendSignal("Safe closed");
		}

		public Vector2 OffsetRectPosition(Rectangle rect, Vector2 offset)
		{
			return new Vector2(rect.X + offset.X, rect.Y + offset.Y);
		}

		public Rectangle OffsetRect(Rectangle rect, Vector2 offset)
		{
			return new Rectangle((int)(rect.X + offset.X), (int)(rect.Y + offset.Y), rect.Width, rect.Height);
		}

		public void OpenSafe ()
		{
			// Toggle the state.
			if (screenState == SafeScreenState.Closed)
			{
				// Play open sound.
				EventManager.Instance.PlaySound ("safeopen");
				screenState = SafeScreenState.Open;
			}
			else
			{
				// Play close sound.
				EventManager.Instance.PlaySound ("safeclose");
				screenState = SafeScreenState.Closed;
			}
		}

		private Rectangle GetLCDSourceRectangle(int index)
		{
			int col = index % 5;
			int row = index / 5;

			return new Rectangle(col*70, row*70, 70, 70);
		}

		private enum SafeScreenState
		{
			Closed,
			Open
		}

		private class SafeButton
		{
			private static Texture2D keyTexture;

			Keys[] inputKey;
			Rectangle sourceRect;
			Rectangle pressedRect;
			Vector2 position;
			Rectangle trueRect;
			bool pressed;
			int value;

			public SafeButton(int val, Vector2 pos, int index, Keys[] input)
			{
				position = pos;
				value = val;
				inputKey = input;

				sourceRect = new Rectangle(0, 85 * index, 100, 85);
				pressedRect = new Rectangle(100, 85 * index, 100, 85);

				trueRect = new Rectangle((int)position.X, (int)position.Y, 100, 85);

				if(keyTexture == null)
					keyTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/KeypadButtons");
			}

			public void Update ()
			{
				pressed = false;

				foreach (Keys k in inputKey)
				{
					if (KeyboardManager.ButtonDown (k) || (MouseManager.LeftButtonIsDown && MouseManager.MouseInRect(trueRect)))
						pressed = true;

					if (KeyboardManager.ButtonPressUp (k) || (MouseManager.LeftClickUp && MouseManager.MouseInRect(trueRect)))
					{
						Console.WriteLine("Pressed key " + value);
						if(value < 10)
						{
							SafeScreen.Instance.SafeState.AddNumber (value);
							EventManager.Instance.PlaySound ("safekeypad");
						}
						else if(value == 10)
							SafeScreen.Instance.SafeState.ClearInput();
						else if (value == 11)
							SafeScreen.Instance.SafeState.CheckIfSolved();

						break;
					}
				}
			}

			public void Draw(SpriteBatch spriteBatch)
			{
				if(pressed)
					spriteBatch.Draw(keyTexture, position: position, sourceRectangle: pressedRect);
				else
					spriteBatch.Draw(keyTexture, position: position, sourceRectangle: sourceRect);
			}
		}
	}
}

