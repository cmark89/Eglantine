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

			if(State == GuiState.Appearing)
			{
				Position -= new Vector2(0, GUI_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
				ClampGuiPosition();
			}
			else if(State == GuiState.Disappearing)
			{
				Position += new Vector2(0, GUI_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds);
				ClampGuiPosition();
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (Texture, Position, Color.White);

			// Draw items.
			for (int i = 0; i < GameState.Instance.PlayerItems.Count; i++)
			{
				//i.Draw(spriteBatch);
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
	}

	public enum GuiState
	{
		Hiding,
		Appearing,
		Shown,
		Disappearing
	}
}

