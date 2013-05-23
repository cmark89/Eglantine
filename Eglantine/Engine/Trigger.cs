using System;
using LuaInterface;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public abstract class Trigger
	{
		public Rectangle Area { get; protected set; }
		public LuaFunction Event { get; protected set; }

		public abstract void Update(GameTime gameTime);
	}
}

