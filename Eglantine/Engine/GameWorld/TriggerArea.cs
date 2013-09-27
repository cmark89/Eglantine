using System;
using Microsoft.Xna.Framework;
using ObjectivelyRadical.Scheduler;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

namespace Eglantine.Engine
{
	// TriggerAreas are triggers that are activated by the player's position.
	[Serializable]
	public class TriggerArea : Trigger
	{
		private Room thisRoom;

		public TriggerArea (string name, Rectangle area, Script gameEvent, bool active, Room parentRoom)
		{
			Name = name;
			Area = area;
			Shape = Trigger.TriggerShape.Rectangle;
			Event = gameEvent;
			Active = active;
			thisRoom = parentRoom;
		}

		public TriggerArea (string name, Polygon poly, Script gameEvent, bool active, Room parentRoom)
		{
			Name = name;
			PolygonArea = poly;
			Shape = Trigger.TriggerShape.Polygon;
			Event = gameEvent;
			Active = active;
			thisRoom = parentRoom;
		}

		public override void Update(GameTime gameTime)
		{
			// Call the event if the player is within the trigger area.
			if(VectorInArea(Player.Instance.Position))
				Scheduler.Execute(Event);
		}

		public void LoadFromSerialization()
		{
			Event = (Script)GameScene.Lua.GetFunction(typeof(Script), "rooms." + thisRoom.Name + ".Interactables." + Name + ".OnEnter");
		}

	}
}

