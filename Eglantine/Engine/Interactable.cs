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
		public Texture2D Texture { get; private set; }

		// Handles events for looking at objects
		public LuaFunction LookEvent { get; private set; }

		public Interactable(Rectangle area, Vector2 interactPoint, LuaFunction gameEvent, LuaFunction lookEvent, bool drawn = false, Texture2D texture = null)
		{
			Area = area;
			InteractPoint = interactPoint;
			Event = gameEvent;
			LookEvent = lookEvent;
			IsDrawn = drawn;

			if(IsDrawn)
				Texture = texture;
		}

		public override void Update (GameTime gameTime)
		{
			if (AdventureScreen.Instance.ReceivingInput)
			{
				if(MouseManager.MouseInRect(Area) && MouseManager.LeftClickUp)
					OnInteract();
				else if(MouseManager.MouseInRect(Area) && MouseManager.RightClickUp)
					OnLook();
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			if (IsDrawn)
			{
				// Update this to draw at a more accurate position
				spriteBatch.Draw(Texture, new Vector2(Area.X, Area.Y), Color.White);
			}
		}

		public void OnInteract()
		{
			if(Event != null)
				Event.Call();
		}

		public void OnLook()
		{
			if(LookEvent != null)
				LookEvent.Call();
		}
	}
}

