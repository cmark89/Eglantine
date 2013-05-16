using System;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public delegate void ItemEvent();

	public class Item
	{
		public Texture2D Texture { get; private set; }
		public ItemType Type { get; private set; }

		private ItemEvent onAcquire;
		private ItemEvent onUse;


		public Item ()
		{
		}

		public void OnAquire()
		{
			onAcquire();
		}

		public void OnUse ()
		{
			switch(Type)
			{
				case ItemType.Immediate:
					onUse();
					break;
				case ItemType.Active:
					// Set the game scene's loaded item to this item.
					break;
				default:
					break;
			}
		}
	}

	public enum ItemType
	{
		Active,		// Can be used on other items in the scene
		Immediate,	// Instant use, usable on itself
		Unusable	// Has no obvious use
	}
}

