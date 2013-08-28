using System;
using System.Collections.Generic;
using Eglantine.Engine;
using LuaInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine
{
	public class MainMenuScene : Scene
	{
		private static MainMenuScene _instance;
		public static MainMenuScene Instance 
		{
			get { return _instance; }
		}

		Lua lua;
		Dictionary<string, TitleElement> graphics;
		MainMenuPhase phase;

		public MainMenuScene ()
		{
			Initialize();
		}



		// Whether just loading the game or returning to it...
		public override void Initialize()
		{
			phase = MainMenuPhase.Loading;
			_instance = this;
			
			lua = new Lua();
			lua.DoFile ("Data/scheduler.lua");
			lua.DoFile ("Data/Events/MainMenu_events.lua");

			// Load the graphics to be used by the menu
			LoadContent();

			phase = MainMenuPhase.SplashScreen;
		}

		public void LoadContent ()
		{
			if (graphics == null)
			{
				graphics = new Dictionary<string, TitleElement>();
				Texture2D texture;

				texture = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/ObjectivelyRadicalSplash");
				graphics.Add ("splash", new TitleElement(texture, CenterElement (texture)));

				texture = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/TitleBackground");
				graphics.Add ("background", new TitleElement(texture));

				texture = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/TitleText");
				graphics.Add ("title", new TitleElement(texture));

				// Add graphics for the menu
			}
		}

		public override void Update (GameTime gameTime)
		{
			foreach (TitleElement te in graphics)
			{
				te.Update (gameTime);
			}
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			foreach (TitleElement te in graphics)
			{
				te.Draw (spriteBatch);
			}

			// Draw menu and text and whatnot here
		}

		public override void Unload()
		{
		}

		public Vector2 CenterElement(Texture2D tex)
		{
			float X = (Eglantine.GAME_WIDTH - tex.Width) / 2;
			float Y = (Eglantine.GAME_HEIGHT - tex.Width) / 2;

			return new Vector2(X, Y);
		}

		public void NextPhase()
		{
			phase = (MainMenuPhase)((int)phase + 1);
		}

		public void FadeInElement(string name, float time)
		{
			if(graphics.ContainsKey (name))
				graphics[name].LerpColor (Color.White, time);
		}

		public void FadeOutElement(string name, float time)
		{
			if(graphics.ContainsKey (name))
				graphics[name].LerpColor (Color.Transparent, time);
		}

		public class TitleElement
		{
			Texture2D texture;
			bool lerpingColor;
			Color currentColor;
			Vector2 position;
			Color startColor;
			Color targetColor;
			float time;
			float lerpDuration;

			public TitleElement(Texture2D tex, Vector2? pos = null)
			{
				texture = tex;

				if(pos == null) position = Vector2.Zero;
				else position = (Vector2)pos;
			}

			public void Update (GameTime gameTime)
			{
				if (lerpingColor)
				{
					time += (float)gameTime.ElapsedGameTime.TotalSeconds;

					currentColor = Color.Lerp (startColor, targetColor, time/lerpDuration);

					if(time > lerpDuration)
					{
						lerpingColor = false;
						currentColor = targetColor;
					}
				}
			}

			public void Draw(SpriteBatch spriteBatch)
			{
				spriteBatch.Draw(texture, position: position, color: currentColor);
			}

			public void LerpColor(Color color, float duration)
			{
				startColor = currentColor;
				targetColor = color;
				lerpDuration = duration;
				time = 0;

				lerpingColor = true;
			}
		}

		private enum MainMenuPhase
		{
			Loading,
			SplashScreen,
			Intro,
			Menu
		}
	}
}

