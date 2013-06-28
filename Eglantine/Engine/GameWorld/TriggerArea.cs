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
			Shape = Trigger.TriggerShape.Rectangle;
			Event = gameEvent;
			Active = active;
		}

		public TriggerArea (string name, Polygon poly, LuaFunction gameEvent, bool active)
		{
			Name = name;
			PolygonArea = poly;
			Shape = Trigger.TriggerShape.Polygon;
			Event = gameEvent;
			Active = active;
		}

		public override void Update(GameTime gameTime)
		{
			// Call the event if the player is within the trigger area.
			if(VectorInArea(Player.Instance.Position))
				Event.Call();
		}
	}
}

