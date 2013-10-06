using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eglantine
{
	public static class KeyboardManager
	{
		public static MouseInteractMode MouseMode = MouseInteractMode.Normal;

		private static KeyboardState _lastFrameState;
		private static KeyboardState _thisFrameState;

		public static void Update(GameTime gameTime)
		{
			_lastFrameState = _thisFrameState;
			_thisFrameState = Keyboard.GetState();
		}

		public static bool ButtonPressUp(Keys key)
		{
			return (_lastFrameState.IsKeyDown(key) && _thisFrameState.IsKeyUp(key));
		}

		public static bool ButtonDown(Keys key)
		{
			return (_thisFrameState.IsKeyDown(key));
		}
	}
}

