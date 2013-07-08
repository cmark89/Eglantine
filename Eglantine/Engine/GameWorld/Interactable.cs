using System;
using LuaInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	// Interactables are triggers that are initiated by the player.
	public class Interactable : Trigger
	{
		public Vector2 InteractPoint { get; private set; }
		public bool IsDrawn { get; private set; }
		public Vector2 DrawPosition { get; private set; }
		public Texture2D Texture { get; private set; }

		// Handles events for looking at objects
		public LuaFunction LookEvent { get; private set; }

		public Interactable(string name, Rectangle area, Vector2 interactPoint, LuaFunction gameEvent, LuaFunction lookEvent, bool enabled, bool drawn = false, Texture2D texture = null)
		{
			Name = name;
			Area = area;
			Shape = Trigger.TriggerShape.Rectangle;
			InteractPoint = interactPoint;
			Event = gameEvent;
			LookEvent = lookEvent;
			IsDrawn = drawn;
			Active = enabled;
			DrawPosition = new Vector2(Area.X, Area.Y);

			if(IsDrawn)
				Texture = texture;
		}

		public Interactable(string name, Polygon area, Vector2 interactPoint, LuaFunction gameEvent, LuaFunction lookEvent, bool enabled, bool drawn = false, Texture2D texture = null, Vector2? drawPos = null)
		{
			Name = name;
			PolygonArea = area;
			Shape = Trigger.TriggerShape.Polygon;
			InteractPoint = interactPoint;
			Event = gameEvent;
			LookEvent = lookEvent;
			IsDrawn = drawn;
			Active = enabled;
			DrawPosition = (Vector2)drawPos;

			if(IsDrawn)
				Texture = texture;
		}

		public bool IsHighlighted ()
		{
			return(AdventureScreen.Instance.HighlightedTrigger == this);
		}

		public override void Update (GameTime gameTime)
		{
			if (AdventureScreen.Instance.ReceivingInput && IsHighlighted())
			{
				if(VectorInArea(MouseManager.Position) && MouseManager.LeftClickUp)
					OnInteract();
				else if(VectorInArea(MouseManager.Position) && MouseManager.RightClickUp)
					OnLook();
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			if (IsDrawn)
			{
				// Update this to draw at a more accurate position
				spriteBatch.Draw(Texture, DrawPosition, Color.White);
			}
		}

		public void OnInteract()
		{
			if(Event != null)
				Event.Call();
		}

		public void OnLook ()
		{
			if (LookEvent != null)
				LookEvent.Call ();
		}
	}
}

