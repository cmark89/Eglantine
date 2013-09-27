using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ObjectivelyRadical.Scheduler;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

namespace Eglantine.Engine
{
	// Interactables are triggers that are initiated by the player.
	[Serializable]
	public class Interactable : Trigger
	{
		public Vector2 InteractPoint { get; private set; }
		public bool IsDrawn { get; private set; }
		public Vector2 DrawPosition { get; private set; }
		public bool BlocksMovement { get; private set; }

		[NonSerialized]
		private Texture2D _texture;
		public Texture2D Texture 
		{
			get { return _texture; }
			private set { _texture = value; }
		}
		private string _TextureName;

		private Color drawColor = Color.White;
		private bool isLerpingColor = false;

		private Color startColor;
		private Color endColor;
		private float colorLerpDuration;
		private float colorLerpTime;

		MouseInteractMode mouseMode;

		// The YCutoff is the Y value beyond which the player will be drawn on top of the object; a YCutoff of 0 means that 
		// the player will always be drawn on top.  Remember to accomodate for the player origin.
		public float YCutoff { get; private set; }
		private Room thisRoom;

		// Handles events for looking at objects
		[NonSerialized]
		private LuaFunction _lookEvent;
		public LuaFunction LookEvent 
		{ 
			get { return _lookEvent; }
			private set { _lookEvent = value; }  
		}


		public Interactable(string name, Rectangle area, Vector2 interactPoint, LuaFunction gameEvent, LuaFunction lookEvent, bool enabled, Room parentRoom, bool drawn = false, Texture2D texture = null, float yCutoff = 0, bool blockMovement = false, string mouse = "Normal")
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
			thisRoom = parentRoom;
			BlocksMovement = blockMovement;

			SetMouseMode(mouse);

			if(IsDrawn)
				Texture = texture;
		}

		public Interactable(string name, Polygon area, Vector2 interactPoint, LuaFunction gameEvent, LuaFunction lookEvent, bool enabled, Room parentRoom, bool drawn = false, Texture2D texture = null, Vector2? drawPos = null, float yCutoff = 0, bool blockMovement = false, string mouse = "Normal")
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
			thisRoom = parentRoom;
			BlocksMovement = blockMovement;

			SetMouseMode(mouse);

			if(IsDrawn)
				Texture = texture;
		}

		public void SetMouseMode (string mouseType)
		{
			switch (mouseType)
			{
			case ("Normal"):
			case("Plain"):
				mouseMode = MouseInteractMode.Normal;
				break;
			case("Grab"):
				mouseMode = MouseInteractMode.Grab;
				break;
			case("Leave"):
				mouseMode = MouseInteractMode.Leave;
				break;
			case("Hot"):
				mouseMode = MouseInteractMode.Hot;
				break;
			}
		}

		public bool IsHighlighted ()
		{
			return(AdventureScreen.Instance.HighlightedTrigger == this);
		}

		public override void Update (GameTime gameTime)
		{
			if (AdventureScreen.Instance.ReceivingInput && IsHighlighted() && !AdventureScreen.Instance.InputDisabled && !AdventureScreen.Instance.MouseInGui)
			{
				// Set the mouse manager's mouse mode to match this item's
				MouseManager.MouseMode = mouseMode;

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
				Scheduler.Execute(Event);
		}

		public void OnLook ()
		{
			if (LookEvent != null)
				Scheduler.Execute(LookEvent);
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

		public void PrepareForSerialization()
		{
			if(Texture != null)
				_TextureName = Texture.Name;
		}

		public void LoadFromSerialization ()
		{
			if (_TextureName != null)
				Texture = ContentLoader.Instance.LoadTexture2D (_TextureName);
			LuaTable thisRoomTable = (LuaTable)GameScene.Lua.GetTable ("rooms." + thisRoom.Name + ".Interactables");

			int thisIndex = 1;
			LuaTable tempTable;
			while(thisIndex < thisRoomTable.Keys.Count)
			{
				tempTable = (LuaTable)thisRoomTable[thisIndex];
				if((string)tempTable["Name"] == Name)
					break;

				thisIndex++;
			}

			LuaTable thisInteractable = (LuaTable)thisRoomTable[thisIndex];

			/// We now have the table of interactables.  
			/// Now we have to loop over each and check if its name == Name
			/// When we find that interactable, we hook up the events from it.
			Event = (LuaFunction)thisInteractable["OnInteract"];
			LookEvent = (LuaFunction)thisInteractable["OnLook"];

			Event = (Script)GameScene.Lua.GetFunction(typeof(Script), "rooms." + thisRoom.Name + ".Interactables." + Name + ".OnInteract");
			LookEvent = (Script)GameScene.Lua.GetFunction(typeof(Script), "rooms." + thisRoom.Name + ".Interactables." + Name + ".OnLook");
		}
	}
}

