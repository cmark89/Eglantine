using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public class SafeScreenGUI
	{
		// Change this if this should fade.
		public Color ScreenColor = Color.White;

		private const int CLOSE_BUTTON_OFFSET_X = 25;
		private const int CLOSE_BUTTON_OFFSET_Y = -18;

		private static Texture2D guiTexture;

		private static Rectangle cornerRect;
		private static Rectangle horizontalBarRect;
		private static Rectangle verticalBarRect;
		private static Rectangle backgroundSrcRect;
		private static Rectangle arrowRect;
		private static Rectangle closeRect;

		private Texture2D puzzle;
		private Rectangle puzzleRect;
		private Rectangle backgroundRect;

		private int horizontalBars;
		private int verticalBars;
		private Vector2 topBorderStart;
		private Vector2 bottomBorderStart;
		private Vector2 leftBorderStart;
		private Vector2 rightBorderStart;
		private Vector2 topLeftCorner;
		private Vector2 topRightCorner;
		private Vector2 bottomLeftCorner;
		private Vector2 bottomRightCorner;

		private Vector2 closeButton;
		private Color closeColor;

		
		public SafeScreenGUI (Texture2D bgTexture)
		{
			puzzle = bgTexture;
			Setup();
		}

		private void Setup ()
		{
			if (guiTexture == null)
			{
				guiTexture = ContentLoader.Instance.Load<Texture2D> ("Graphics/DocumentFrame");

				cornerRect = new Rectangle(0,0,48,48);
				horizontalBarRect = new Rectangle(48,0,48,48);
				verticalBarRect = new Rectangle(96,0,48,48);
				backgroundSrcRect = new Rectangle(0,48,96,96);
				arrowRect = new Rectangle(96, 48, 48, 48);
				closeRect = new Rectangle(96, 96, 48, 48);
			}

			puzzleRect = GetTextureRectangle();
			BuildFrame();
		}

		// Get the area filled by the document
		private Rectangle GetTextureRectangle()
		{
			int screenHeight = Eglantine.GAME_HEIGHT;
			int screenWidth = Eglantine.GAME_WIDTH;

			int startX = (int)((screenWidth - puzzle.Width) / 2f);
			int startY = (int)((screenHeight- puzzle.Height) / 2f);

			return new Rectangle(startX, startY, puzzle.Width, puzzle.Height);
		}


		private void BuildFrame()
		{
			// First, figure out how many times the frames should tile
			horizontalBars = (puzzleRect.Width / 48) + 1;
			verticalBars = (puzzleRect.Height / 48) + 1;

			// Then, find where the background should be drawn
			backgroundRect = GetBackgroundRectangle();

			// Next, find where all the borders should begin.
			// They must be offset by 1/2 their height or width relative to the center
			topBorderStart = new Vector2(backgroundRect.X, backgroundRect.Y - 24);
			bottomBorderStart = new Vector2(backgroundRect.X, backgroundRect.Y + backgroundRect.Height - 24);
			leftBorderStart = new Vector2(backgroundRect.X - 24, backgroundRect.Y);
			rightBorderStart = new Vector2(backgroundRect.X + backgroundRect.Width - 24, backgroundRect.Y);

			// Now, determine where to put the corner graphics
			topLeftCorner = new Vector2(backgroundRect.X - 24, backgroundRect.Y - 24);
			topRightCorner = new Vector2(backgroundRect.X + backgroundRect.Width - 24, backgroundRect.Y - 24);
			bottomLeftCorner = new Vector2(backgroundRect.X - 24, backgroundRect.Y + backgroundRect.Height - 24);
			bottomRightCorner = new Vector2(backgroundRect.X + backgroundRect.Width - 24, backgroundRect.Y + backgroundRect.Height - 24);

			// The close button should be drawn in the upper right and displaced diagonally upwards
			closeButton = new Vector2(topRightCorner.X + CLOSE_BUTTON_OFFSET_X, topRightCorner.Y + CLOSE_BUTTON_OFFSET_Y);
		}



		private Rectangle GetBackgroundRectangle()
		{
			int width = horizontalBars * 48;
			int height = verticalBars * 48;

			int deltaWidth = width - puzzleRect.Width;
			int deltaHeight = height - puzzleRect.Height;

			int x = puzzleRect.X - (deltaWidth / 2);
			int y = puzzleRect.Y - (deltaHeight / 2);

			return new Rectangle(x, y, width, height);
		}

		public void DrawBackground(SpriteBatch spriteBatch)
		{
			// If there's no puzzle, then don't draw anything.
			if (puzzle == null)
				return;

			// First, draw the background layer
			spriteBatch.Draw (guiTexture, sourceRectangle: backgroundSrcRect, drawRectangle: backgroundRect, color: ScreenColor);
			spriteBatch.Draw (puzzle, drawRectangle: puzzleRect, color: ScreenColor);
		}

		public void DrawFrame (SpriteBatch spriteBatch)
		{
			// If there's no puzzle, then don't draw anything.
			if (puzzle == null)
				return;

			// Draw the borders
			// Horizontal first
			for (int i = 0; i < horizontalBars; i++)
			{
				spriteBatch.Draw (guiTexture, sourceRectangle: horizontalBarRect, position: new Vector2 (topBorderStart.X + 48 * i, topBorderStart.Y), color: ScreenColor);
				spriteBatch.Draw (guiTexture, sourceRectangle: horizontalBarRect, position: new Vector2 (bottomBorderStart.X + 48 * i, bottomBorderStart.Y), color: ScreenColor);
			}
			// And then vertical
			for (int i = 0; i < verticalBars; i++)
			{
				spriteBatch.Draw (guiTexture, sourceRectangle: verticalBarRect, position: new Vector2 (leftBorderStart.X, topBorderStart.Y + 48 * i), color: ScreenColor);
				spriteBatch.Draw (guiTexture, sourceRectangle: verticalBarRect, position: new Vector2 (rightBorderStart.X, rightBorderStart.Y + 48 * i), color: ScreenColor);
			}

			// Now, draw the corners
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: topLeftCorner, color: ScreenColor);
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: bottomLeftCorner, color: ScreenColor);
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: topRightCorner, color: ScreenColor);
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: bottomRightCorner, color: ScreenColor);

			// Finally, draw the close button
			spriteBatch.Draw(guiTexture, sourceRectangle: closeRect, position: closeButton, color: closeColor);
		}

		public void Update (GameTime gameTime)
		{
			closeColor = Color.Lerp(Color.Gray, ScreenColor, .5f);

			if(SafeScreen.Instance.ReceivingInput)
			{
				bool clicked = MouseManager.LeftClickUp;

				// If the player cursor is over the close button
				if (Vector2.Distance (MouseManager.Position, closeButton + new Vector2 (24, 24)) < 23)
				{
					closeColor = ScreenColor;
					if(clicked)
						SafeScreen.Instance.Close();
				}
			}
		}

		public Vector2 PuzzleStartPosition()
		{
			return new Vector2(puzzleRect.X, puzzleRect.Y);
		}

		public Rectangle GetOpenButtonRect()
		{
			return new Rectangle(backgroundRect.Width/2 - 64, backgroundRect.Height-36, 128, 72);
		}
	}
}

