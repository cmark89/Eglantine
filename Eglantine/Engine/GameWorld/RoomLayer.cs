using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LuaInterface;
using Eglantine.Engine;

namespace Eglantine
{
	[Serializable]
	public class RoomLayer
	{
		#region Fields
		// This is used for explicit changes that may be required
		public string Name { get; private set;}

		// Stores a list of textures and positions.  Normally this will only draw one, except in the 
		// case of scrolling backgrounds.
		public List<TextureWrapper> Textures { get; private set; }

		// The color to draw the room in
		public Color Color { get; private set; }

		public bool Scrolling { get; private set; }
		// This is how much the layer should scroll every second
		public Vector2 Scroll { get; private set; }

		// The type of layer; this is used by the parent Room for sorting after 
		// building of all layers is completed.
		public RoomLayerDrawType Type { get; private set; }

		//----TBA-----
		// Texture used for lighting
		public Texture2D Lightmask { get; private set; }

		// How deep the layer should be drawn
		// This can be used to override the initial placement order.
		// Lower numbers are drawn first.
		public float Depth { get; private set; }

		// This is the Y point beyond which the layer will be drawn on top of the player
		public float YCutoff { get; private set;}

		#endregion
		// This is used with scrolling background to keep track of multiple instances of the texture
		public class TextureWrapper
		{
			public Texture2D Texture;
			public Vector2 Position;

			public TextureWrapper(Texture2D tex, Vector2 vec)
			{
				Texture = tex;
				Position = vec;
			}
		}

		// Constructs a room layer from a layer described in a LuaTable
		public RoomLayer (LuaTable layerTable)
		{
			Textures = new List<TextureWrapper>();
			Name = (string)layerTable["Name"];
			Textures.Add(new TextureWrapper(ContentLoader.Instance.Load<Texture2D>((string)layerTable["Texture"]), Vector2.Zero));
			LuaTable colorTable = (LuaTable)layerTable["Color"];
			Color = new Color((float)(double)colorTable[1], (float)(double)colorTable[2], (float)(double)colorTable[3], (float)(double)colorTable[4]);
			//Scroll = new Vector2((float)(double)layerTable["Scroll.X"], (float)(double)layerTable["Scroll.Y"]);

			switch((string)layerTable["Type"])
			{
				case("Background"):
					Type = RoomLayerDrawType.Background;
					break;
				case("Foreground"):
					Type = RoomLayerDrawType.Foreground;
					break;
				case("Midground"):
					Type = RoomLayerDrawType.Midground;
					break;
				default:
					break;
			}

			if(Scroll.X > 0 || Scroll.Y > 0)
				Scrolling = true;
			else
				Scrolling = false;

			// More to come...
		}

		public void Update (GameTime gameTime)
		{
			if (Scrolling)
			{
				foreach(TextureWrapper tw in Textures)
				{
					tw.Position += Scroll * (float)gameTime.ElapsedGameTime.TotalSeconds;
				}

				// Check to see if a new TextureWrapper is needed to cover empty space
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach(TextureWrapper tw in Textures)
				spriteBatch.Draw(tw.Texture, tw.Position, Color);
		}
	}

	public enum RoomLayerDrawType
	{
		Background,
		Midground,
		Foreground
	}
}

