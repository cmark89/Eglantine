using System;
using LuaInterface;
using Eglantine.Engine.Pathfinding;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public abstract class Trigger
	{
		public string Name { get; protected set; }
		public bool Active { get; protected set; }
		public Rectangle Area { get; protected set; }
		public Polygon PolygonArea { get; protected set; }
		public TriggerShape Shape { get; protected set; }
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

