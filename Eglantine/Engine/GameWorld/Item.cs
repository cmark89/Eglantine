using System;
using Microsoft.Xna.Framework.Graphics;
using ObjectivelyRadical.Scheduler;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

namespace Eglantine.Engine
{
	public delegate void ItemEvent();

	[Serializable]
	public class Item
	{
		public string Name { get; private set; }
		public string Description { get; private set; }

		[NonSerialized]
		private Texture2D _texture;
		public Texture2D Texture 
		{
			get { return _texture; }
			private set { _texture = value; }
		}
		private string _TextureName;

		public ItemType Type { get; private set; }

		[NonSerialized]
		private Script OnAcquire;
		[NonSerialized]
		private Script OnUse;

		public Item (string name)
		{
			LuaTable itemTable = (LuaTable)GameScene.Lua["items."+name];
			ParseItem(itemTable);
		}

		public void ParseItem (LuaTable itemTable)
		{
			Name = (string)itemTable ["Name"];

			Texture = ContentLoader.Instance.LoadTexture2D((string)(itemTable["Texture"]));

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
			OnUse = (LuaFunction)itemTable["OnUse"];
		}

		public void Inspect ()
		{
			//if(OnInspect != null)
				//OnInspect.Call();

			Scheduler.Execute(delegate() { EventManager.Instance.ShowMessage(Description); });
		}

		public void OnAquire ()
		{
			EventManager.Instance.SendSignal (Name + " found");

			// If there is a document that shares a name with this item...
			LuaTable docTable = GameScene.Lua.GetTable ("documents");
			if ((LuaTable)docTable[Name] != null)
			{
				Console.WriteLine("Adding " + Name + " to document list.");
				// ...add it to the global document list.
				GameState.Instance.AddDocument(new Document(Name));
			}

			if(OnAcquire != null)
				Scheduler.Execute(OnAcquire);
		}

		public void Use ()
		{
			switch(Type)
			{
				case ItemType.Immediate:
					Console.WriteLine("Use this immediate item!");
					Scheduler.Execute(OnUse);
					break;
				case ItemType.Active:
					// Set the game scene's loaded item to this item.
					AdventureScreen.Instance.SetActiveItem(this);
					break;
				default:
					break;
			}
		}

		public void SetType(ItemType it)
		{
			Type = it;
		}

		public void PrepareForSerialization()
		{
			_TextureName = Texture.Name;
		}

		public void LoadFromSerialization()
		{
			if(_TextureName != null)
				Texture = ContentLoader.Instance.LoadTexture2D(_TextureName);

			LuaTable itemTable = (LuaTable)GameScene.Lua["items."+Name];
			OnAcquire = (LuaFunction)itemTable["OnAcquire"];
			OnUse = (LuaFunction)itemTable["OnUse"];
		}
	}

	public enum ItemType
	{
		Active,		// Can be used on other items in the scene
		Immediate,	// Instant use, usable on itself
		Unusable	// Has no obvious use
	}

	public enum ItemID
	{
		Scissors = 1,
		Crowbar = 2,

		Journal = 4,
		Notes = 8,
		Blueprints = 16,
		Letter = 32,
		Photograph = 64,
		FoldedNote = 128,

		Puzzlebox = 256,
		Puzzlekey = 512,
		Coin = 1024,
		Key = 2048,

		Eglantine = 4096

	}
}

