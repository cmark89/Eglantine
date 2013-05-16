using System;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public delegate void GameEvent();

	public abstract class Trigger
	{
		public Rectangle Area { get; protected set; }
		public GameEvent Event { get; protected set; }

		public abstract void Update(GameTime gameTime);
	}
}

