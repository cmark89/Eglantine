using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public class DocumentScreenGUI
	{
		// Change this if this should fade.
		public Color DocumentColor = Color.White;

		private const int ARROW_BUTTON_OFFSET = 35;
		private const int CLOSE_BUTTON_OFFSET_X = 25;
		private const int CLOSE_BUTTON_OFFSET_Y = -18;

		private static Texture2D guiTexture;

		private static Rectangle cornerRect;
		private static Rectangle horizontalBarRect;
		private static Rectangle verticalBarRect;
		private static Rectangle backgroundSrcRect;
		private static Rectangle arrowRect;
		private static Rectangle closeRect;

		private Rectangle documentRect;
		private Rectangle backgroundRect;
		private Texture2D document;

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

		private Vector2 leftArrowButton;
		private Vector2 rightArrowButton;
		private Vector2 closeButton;
		private Color leftArrowColor;
		private Color rightArrowColor;
		private Color closeColor;

		private bool drawLeftArrow = false;
		private bool drawRightArrow = false;


		public DocumentScreenGUI ()
		{
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
		}

		public void SetPage (Texture2D page, int currentPageNumber, int numberOfPages)
		{
			document = page;
			documentRect = GetTextureRectangle();

			drawLeftArrow = (numberOfPages > 1 && currentPageNumber > 1);
			drawRightArrow = (currentPageNumber < numberOfPages);

			BuildFrame();
		}

		// Get the area filled by the document
		private Rectangle GetTextureRectangle()
		{
			int screenHeight = Eglantine.GAME_HEIGHT;
			int screenWidth = Eglantine.GAME_WIDTH;

			int startX = (int)((screenWidth - document.Width) / 2f);
			int startY = (int)((screenHeight- document.Height) / 2f);

			return new Rectangle(startX, startY, document.Width, document.Height);
		}


		private void BuildFrame()
		{
			// First, figure out how many times the frames should tile
			horizontalBars = (documentRect.Width / 48) + 1;
			verticalBars = (documentRect.Height / 48) + 1;

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

			// Now, decide where we'll draw the buttons
			// The arrow gems should be draw right in the middle, off to the right and left
			leftArrowButton = new Vector2(backgroundRect.X - 24 - ARROW_BUTTON_OFFSET, backgroundRect.Y + (backgroundRect.Height / 2) - 24);
			rightArrowButton = new Vector2(backgroundRect.X + backgroundRect.Width - 24 + ARROW_BUTTON_OFFSET, backgroundRect.Y + (backgroundRect.Height / 2) - 24);

			// The close button should be drawn in the upper right and displaced diagonally upwards
			closeButton = new Vector2(topRightCorner.X + CLOSE_BUTTON_OFFSET_X, topRightCorner.Y + CLOSE_BUTTON_OFFSET_Y);
		}



		private Rectangle GetBackgroundRectangle()
		{
			int width = horizontalBars * 48;
			int height = verticalBars * 48;

			int deltaWidth = width - documentRect.Width;
			int deltaHeight = height - documentRect.Height;

			int x = documentRect.X - (deltaWidth / 2);
			int y = documentRect.Y - (deltaHeight / 2);

			return new Rectangle(x, y, width, height);
		}


		public void Draw (SpriteBatch spriteBatch)
		{
			// If there's no document, then don't draw anything.
			if (document == null)
				return;

			// First, draw the background layer
			spriteBatch.Draw (guiTexture, sourceRectangle: backgroundSrcRect, drawRectangle: backgroundRect, color: DocumentColor);

			// Now, draw the actual document
			spriteBatch.Draw (document, drawRectangle: documentRect, color: DocumentColor);

			// Next, draw the borders
			// Horizontal first
			for (int i = 0; i < horizontalBars; i++)
			{
				spriteBatch.Draw (guiTexture, sourceRectangle: horizontalBarRect, position: new Vector2 (topBorderStart.X + 48 * i, topBorderStart.Y), color: DocumentColor);
				spriteBatch.Draw (guiTexture, sourceRectangle: horizontalBarRect, position: new Vector2 (bottomBorderStart.X + 48 * i, bottomBorderStart.Y), color: DocumentColor);
			}
			// And then vertical
			for (int i = 0; i < verticalBars; i++)
			{
				spriteBatch.Draw (guiTexture, sourceRectangle: verticalBarRect, position: new Vector2 (leftBorderStart.X, topBorderStart.Y + 48 * i), color: DocumentColor);
				spriteBatch.Draw (guiTexture, sourceRectangle: verticalBarRect, position: new Vector2 (rightBorderStart.X, rightBorderStart.Y + 48 * i), color: DocumentColor);
			}

			// Now, draw the corners
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: topLeftCorner, color: DocumentColor);
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: bottomLeftCorner, color: DocumentColor);
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: topRightCorner, color: DocumentColor);
			spriteBatch.Draw (guiTexture, sourceRectangle: cornerRect, position: bottomRightCorner, color: DocumentColor);

			// Finally, draw the buttons if they are necessary.
			if (drawLeftArrow)
			{
				spriteBatch.Draw(guiTexture, sourceRectangle: arrowRect, position: leftArrowButton, effect: SpriteEffects.FlipHorizontally, color: leftArrowColor);
			}
			if (drawRightArrow)
			{
				spriteBatch.Draw(guiTexture, sourceRectangle: arrowRect, position: rightArrowButton, color: rightArrowColor);
			}

			spriteBatch.Draw(guiTexture, sourceRectangle: closeRect, position: closeButton, color: closeColor);
		}

		public void Update (GameTime gameTime)
		{
			// Reset the colors to their inactive state.
			leftArrowColor = Color.Lerp(Color.Gray, DocumentColor, .5f);
			rightArrowColor = Color.Lerp(Color.Gray, DocumentColor, .5f);
			closeColor = Color.Lerp(Color.Gray, DocumentColor, .5f);

			if(DocumentScreen.Instance.ReceivingInput)
			{
				bool clicked = MouseManager.LeftClickUp;

				// If player cursor is over the left arrow...
				if (drawLeftArrow && Vector2.Distance (MouseManager.Position, leftArrowButton + new Vector2 (24, 24)) < 23)
				{
					leftArrowColor = DocumentColor;
					if(clicked)
						DocumentScreen.Instance.GoToPreviousPage();
				}
				// If the player cursor is over the right arrow...
				else if (drawRightArrow && Vector2.Distance (MouseManager.Position, rightArrowButton + new Vector2 (24, 24)) < 23)
				{
					rightArrowColor = DocumentColor;
					if(clicked)
						DocumentScreen.Instance.GoToNextPage();
				}
				// If the player cursor is over the close button
				else if (Vector2.Distance (MouseManager.Position, closeButton + new Vector2 (24, 24)) < 23)
				{
					closeColor = DocumentColor;
					if(clicked)
						DocumentScreen.Instance.Close();
				}
			}
		}
	}
}

