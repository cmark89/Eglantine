using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Eglantine
{
	public static class MouseManager
	{
		private static MouseState _lastFrameState;
		private static MouseState _thisFrameState;

		public static void Update(GameTime gameTime)
		{
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
	}
}

