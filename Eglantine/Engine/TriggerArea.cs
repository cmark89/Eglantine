using System;
using LuaInterface;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	// TriggerAreas are triggers that are activated by the player's position.
	public class TriggerArea : Trigger
	{

		public TriggerArea (string name, Rectangle area, LuaFunction gameEvent, bool active)
		{
			Name = name;
			Area = area;
			Event = gameEvent;
			Active = active;
		}

		public override void Update(GameTime gameTime)
		{
			// Call the event if the player is within the trigger area.
			if(VectorInArea(Player.Instance.Position))
				Event.Call();
		}

		public bool VectorInArea(Vector2 point)
		{
			return (point.X >= Area.X && point.X <= Area.X + Area.Width && 
			        point.Y >= Area.Y && point.Y <= Area.Y + Area.Height);
		}
	}
}

