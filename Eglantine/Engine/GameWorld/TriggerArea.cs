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
		private string tablePath;

		public TriggerArea (string name, Rectangle area, Script gameEvent, bool active, Room parentRoom, int index)
		{
			Name = name;
			Area = area;
			Shape = Trigger.TriggerShape.Rectangle;
			Event = gameEvent;
			Active = active;
			thisRoom = parentRoom;

			SetTablePath(index);
		}

		public TriggerArea (string name, Polygon poly, Script gameEvent, bool active, Room parentRoom, int index)
		{
			Name = name;
			PolygonArea = poly;
			Shape = Trigger.TriggerShape.Polygon;
			Event = gameEvent;
			Active = active;
			thisRoom = parentRoom;

			SetTablePath(index);
		}

		private void SetTablePath(int index)
		{
			tablePath = "rooms." + thisRoom.Name + ".Triggers[" + index + "]";
		}

		public override void Update(GameTime gameTime)
		{
			// Call the event if the player is within the trigger area.
			if(VectorInArea(Player.Instance.Position) && Event != null)
				Scheduler.Execute(Event);
		}

		public void LoadFromSerialization()
		{
			Event = (Script)GameScene.Lua.GetFunction(typeof(Script), "rooms." + thisRoom.Name + ".Triggers." + Name + ".OnEnter");
		}

	}
}

