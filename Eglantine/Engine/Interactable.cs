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

		public Interactable(Rectangle area, Vector2 interactPoint, LuaFunction gameEvent, bool drawn = false, Texture2D texture = null)
		{
			Area = area;
			InteractPoint = interactPoint;
			Event = gameEvent;
			IsDrawn = drawn;

			if(IsDrawn)
				Texture = texture;
		}

		public override void Update(GameTime gameTime)
		{
			MouseState mouse = Mouse.GetState();
			if(mouse.X >= Area.X && mouse.X <= Area.X + Area.Width &&
			   mouse.Y >= Area.Y && mouse.Y <= Area.Y + Area.Height &&
			   MouseManager.LeftClickUp)
				OnInteract();
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
			Event.Call();
		}
	}
}

