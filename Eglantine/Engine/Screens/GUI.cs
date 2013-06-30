using System;
using Eglantine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public class GUI
	{
		// How fast the GUI will scroll when hovered over
		private const float GUI_SCROLL_SPEED = 150f;

		// How much of the GUI is visible when hidden (measured from the top)
		private const float GUI_HIDDEN_HEIGHT = 20;

		// Constants for determining where to draw the items
		private static readonly Vector2 ITEM_DRAW_START = new Vector2(12,39);
		private static readonly Vector2 ITEM_DRAW_OFFSET = new Vector2(53,0);

		private GuiState State;
		private Texture2D Texture;
		private Vector2 Position;

		public bool MouseInGUI
		{
			get { return MouseManager.Y > Position.Y; }
		}

		public GUI ()
		{
			Initialize();
		}

		public void Initialize()
		{
			State = GuiState.Hiding;
			Texture = ContentLoader.Instance.Load<Texture2D>("phGui");
			Position = new Vector2(0, Eglantine.GAME_HEIGHT - GUI_HIDDEN_HEIGHT);
		}

		public void Update (GameTime gameTime)
		{
			// This only gets called if AdventureScreen is taking input.

			// If the mouse is below the threshold for showing the GUI
			if ((State == GuiState.Hiding || State == GuiState.Disappearing) && MouseManager.Y >= Position.Y)
			{
				State = GuiState.Appearing;
			} else if ((State == GuiState.Shown || State == GuiState.Appearing) && MouseManager.Y < Position.Y)
			{
				State = GuiState.Disappearing;
			}

			if (State == GuiState.Appearing)
			{
				Position -= new Vector2 (0, GUI_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
				ClampGuiPosition ();
			} else if (State == GuiState.Disappearing)
			{
				Position += new Vector2 (0, GUI_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
				ClampGuiPosition ();
			}

			if (!AdventureScreen.Instance.InputDisabled)
			{
				// Process mouse input
				for(int i = 0; i < GameState.Instance.PlayerItems.Count; i++)
				{
					if(MouseManager.MouseInRect(GetItemRect(i)))
					{
						// Use the item if left clicked...
						if(MouseManager.LeftClickUp)
							GameState.Instance.PlayerItems[i].Use();

						// Or, inspect the item if right clicked...
						else if(MouseManager.RightClickUp)
							GameState.Instance.PlayerItems[i].Inspect();
					}
				}
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (Texture, Position, Color.White);
			Item thisItem;

			// Draw items, but only do this if any of them are visible necessary.
			if (Eglantine.GAME_HEIGHT - Position.Y > ITEM_DRAW_OFFSET.Y)
			{
				for(int i = 0; i < GameState.Instance.PlayerItems.Count; i++)
				{
					thisItem = GameState.Instance.PlayerItems[i];
					spriteBatch.Draw(thisItem.Texture, Position + ITEM_DRAW_START + (ITEM_DRAW_OFFSET * i), Color.White);
				}
			}
		}

		public void ClampGuiPosition ()
		{
			if (State == GuiState.Appearing && Position.Y < Eglantine.GAME_HEIGHT - Texture.Height)
			{
				Position = new Vector2(Position.X, Eglantine.GAME_HEIGHT - Texture.Height);
				State = GuiState.Shown;
			} else if (State == GuiState.Disappearing && Position.Y > Eglantine.GAME_HEIGHT - GUI_HIDDEN_HEIGHT)
			{
				Position = new Vector2(Position.X, Eglantine.GAME_HEIGHT - GUI_HIDDEN_HEIGHT);
				State = GuiState.Hiding;
			}
		}

		public Rectangle GetItemRect(int i)
		{
			float x = Position.X + ITEM_DRAW_START.X + (ITEM_DRAW_OFFSET.X * i);
			float y = Position.Y + ITEM_DRAW_START.Y + (ITEM_DRAW_OFFSET.Y * i);

			return new Rectangle((int)x, (int)y, 48, 48);
		}
	}

	public enum GuiState
	{
		Hiding,
		Appearing,
		Shown,
		Disappearing
	}
}

