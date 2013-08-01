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

		private Color drawColor = Color.White;
		private bool isLerpingColor = false;

		private Color startColor;
		private Color endColor;
		private float colorLerpDuration;
		private float colorLerpTime;

		// The YCutoff is the Y value beyond which the player will be drawn on top of the object; a YCutoff of 0 means that 
		// the player will always be drawn on top.  Remember to accomodate for the player origin.
		public float YCutoff { get; private set; }

		// Handles events for looking at objects
		public LuaFunction LookEvent { get; private set; }

		public Interactable(string name, Rectangle area, Vector2 interactPoint, LuaFunction gameEvent, LuaFunction lookEvent, bool enabled, bool drawn = false, Texture2D texture = null, float yCutoff = 0)
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
			YCutoff = yCutoff;

			if(IsDrawn)
				Texture = texture;
		}

		public Interactable(string name, Polygon area, Vector2 interactPoint, LuaFunction gameEvent, LuaFunction lookEvent, bool enabled, bool drawn = false, Texture2D texture = null, Vector2? drawPos = null, float yCutoff = 0)
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
			YCutoff = yCutoff;

			if(IsDrawn)
				Texture = texture;
		}

		public bool IsHighlighted ()
		{
			return(AdventureScreen.Instance.HighlightedTrigger == this);
		}

		public override void Update (GameTime gameTime)
		{
			if (AdventureScreen.Instance.ReceivingInput && IsHighlighted ())
			{
				if (VectorInArea (MouseManager.Position) && MouseManager.LeftClickUp)
					OnInteract ();
				else if (VectorInArea (MouseManager.Position) && MouseManager.RightClickUp)
					OnLook ();
			}

			if (isLerpingColor)
			{
				colorLerpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
				drawColor = Color.Lerp (startColor, endColor, colorLerpTime/colorLerpDuration);

				if(colorLerpTime > colorLerpDuration)
				{
					drawColor = endColor;
					isLerpingColor = false;
				}
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			if (IsDrawn)
			{
				// Update this to draw at a more accurate position
				spriteBatch.Draw(Texture, DrawPosition, drawColor);
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

		public void SetColor(Color color)
		{
			drawColor = color;
		}

		public void LerpColor(Color toColor, float duration)
		{
			if(!Active)
				return;

			startColor = drawColor;
			endColor = toColor;
			colorLerpDuration = duration;
			colorLerpTime = 0f;

			isLerpingColor = true;
		}
	}
}

