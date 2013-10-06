using System;
using Eglantine.Engine.Pathfinding;
using Microsoft.Xna.Framework;
using ObjectivelyRadical.Scheduler;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

namespace Eglantine.Engine
{
	[Serializable]
	public abstract class Trigger
	{
		public string Name { get; protected set; }
		public bool Active { get; protected set; }
		public Rectangle Area { get; protected set; }
		public Polygon PolygonArea { get; protected set; }
		public TriggerShape Shape { get; protected set; }
		[NonSerialized]
		private Script _event;
		public Script Event { 
			get { return _event; }
			protected set { _event = value; }
		}

		public abstract void Update(GameTime gameTime);

		public void Enable()
		{
			Active = true;
		}

		public void Disable()
		{
			Active = false;
		}

		public bool VectorInArea (Vector2 point)
		{
			if (Shape == Trigger.TriggerShape.Rectangle)
			{
				return (point.X >= Area.X && point.X <= Area.X + Area.Width && 
					point.Y >= Area.Y && point.Y <= Area.Y + Area.Height);
			} else
			{
				return (PolygonArea.ContainsPoint(point));
			}
		}

		public enum TriggerShape
		{
			Rectangle,
			Polygon
		}
	}
}

