using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	// Interactables are triggers that are initiated by the player.
	public class Interactable : Trigger
	{
		public bool IsDrawn { get; private set; }
		public Texture2D Texture { get; private set; }

		public Interactable()
		{
		}

		public override void Update(GameTime gameTime)
		{
			// Handle player interaction if the containing screen is active
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the object if it has a texture
		}

		public void OnInteract()
		{
			Event();
		}
	}
}

