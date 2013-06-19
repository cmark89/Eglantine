using System;
using LuaInterface;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public abstract class Trigger
	{
		public string Name { get; protected set; }
		public bool Active { get; protected set; }
		public Rectangle Area { get; protected set; }
		public LuaFunction Event { get; protected set; }

		public abstract void Update(GameTime gameTime);

		public void Enable()
		{
			Active = true;
		}

		public void Disable()
		{
			Active = false;
		}
	}
}

