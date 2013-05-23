using System;
using LuaInterface;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	// TriggerAreas are triggers that are activated by the player's position.
	public class TriggerArea : Trigger
	{

		public TriggerArea (Rectangle area, LuaFunction gameEvent)
		{
			Area = area;
			Event = gameEvent;
		}

		public override void Update(GameTime gameTime)
		{
			// This is so temporary it's not even funny.
			Vector2 playerPos = Eglantine.pather.Position;

			if(VectorInArea(playerPos))
				Event.Call();
		}

		public bool VectorInArea(Vector2 point)
		{
			if(point.X >= Area.X && point.X <= Area.X + Area.Width && 
			   point.Y >= Area.Y && point.Y <= Area.Y + Area.Height)
				return true;
			else
				return false;
		}
	}
}

