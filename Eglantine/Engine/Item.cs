using System;
using LuaInterface;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public delegate void ItemEvent();

	public class Item
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public Texture2D Texture { get; private set; }

		public ItemType Type { get; private set; }

		private LuaFunction OnAcquire;
		private LuaFunction OnInspect;
		private LuaFunction OnUse;

		public Item (string name)
		{
			ParseItem((LuaTable)Eglantine.Lua["items."+name]);
		}

		public void ParseItem (LuaTable itemTable)
		{
			Name = (string)itemTable ["Name"];
			Description = (string)itemTable ["Description"];
			Texture = ContentLoader.Instance.Load<Texture2D>((string)(itemTable["Texture"]));

			string type = (string)itemTable ["Type"];
			switch (type)
			{
				case("Immediate"):
					Type = ItemType.Immediate;
					break;
				case("Active"):
					Type = ItemType.Active;
					break;
				case("Unusable"):
					Type = ItemType.Unusable;
					break;
				default:
					break;
			}

			OnAcquire = (LuaFunction)itemTable["OnAcquire"];
			OnInspect = (LuaFunction)itemTable["OnInspect"];
			OnUse = (LuaFunction)itemTable["OnUse"];
		}

		public void Inspect ()
		{
			OnInspect.Call();
		}

		public void OnAquire()
		{
			OnAcquire.Call();
		}

		public void Use ()
		{
			switch(Type)
			{
				case ItemType.Immediate:
					OnUse.Call();
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

