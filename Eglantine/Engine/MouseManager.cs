using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eglantine
{
	public static class MouseManager
	{
		public static MouseInteractMode MouseMode = MouseInteractMode.Normal;

		private static MouseState _lastFrameState;
		private static MouseState _thisFrameState;

		private static Texture2D drawTexture;


		// Textures to be drawn for the mouse
		private static Texture2D normalTexture;
		private static Texture2D hotTexture;
		private static Texture2D grabTexture;	
		private static Texture2D leaveTexture;

		public static void Initialize ()
		{
			// Setup textures if they have not been loaded
			if (normalTexture == null)
			{
				normalTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/Mouse/PlainPointer");
				hotTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/Mouse/HotPointer");
				grabTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/Mouse/GrabPointer");
				leaveTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/Mouse/LeavePointer");
			}
		}


		public static void Update(GameTime gameTime)
		{
			// Reset this before the game has a chance to change it
			MouseMode = MouseInteractMode.Normal;

			_lastFrameState = _thisFrameState;
			_thisFrameState = Mouse.GetState();
		}

		public static float X 
		{ 
			get { return _thisFrameState.X; } 
		}

		public static float Y 
		{ 
			get { return _thisFrameState.Y; } 
		}

		public static Vector2 Position 
		{ 
			get { return new Vector2(_thisFrameState.X, _thisFrameState.Y); } 
		}

		public static bool LeftClickDown
		{
			get { return (_lastFrameState.LeftButton ==  ButtonState.Released && _thisFrameState.LeftButton == ButtonState.Pressed); }
		}

		public static bool LeftClickUp
		{
			get { return (_lastFrameState.LeftButton ==  ButtonState.Pressed && _thisFrameState.LeftButton == ButtonState.Released); }
		}

		public static bool LeftButtonIsDown
		{
			get { return (_thisFrameState.LeftButton == ButtonState.Pressed); }
		}

		public static bool RightClickDown
		{
			get { return (_lastFrameState.RightButton ==  ButtonState.Released && _thisFrameState.RightButton == ButtonState.Pressed); }
		}

		public static bool RightClickUp
		{
			get { return (_lastFrameState.RightButton ==  ButtonState.Pressed && _thisFrameState.RightButton == ButtonState.Released); }
		}

		public static bool RightButtonIsDown
		{
			get { return (_thisFrameState.RightButton == ButtonState.Pressed); }
		}

		public static bool MouseInRect(Rectangle rect)
		{
			return(_thisFrameState.X >= rect.X && 
			       _thisFrameState.X <= rect.X + rect.Width && 
			       _thisFrameState.Y >= rect.Y && 
			       _thisFrameState.Y <= rect.Y + rect.Height);
		}

		public static void DrawMouse (SpriteBatch spriteBatch)
		{
			switch (MouseMode)
			{
			case (MouseInteractMode.Normal):
				drawTexture = normalTexture;
				break;
			case (MouseInteractMode.Hot):
				drawTexture = hotTexture;
				break;
			case (MouseInteractMode.Grab):
				drawTexture = grabTexture;
				break;
			case (MouseInteractMode.Leave):
				drawTexture = leaveTexture;
				break;
			default:
				break;
			}

			spriteBatch.Draw (drawTexture, position: MouseManager.Position, color: Color.White);
		}
	}

	public enum MouseInteractMode
	{
		Normal,
		Hot,
		Grab,
		Leave
	}
}

