using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine;
using ObjectivelyRadical.Scheduler;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

namespace Eglantine
{
	public class StoryScene : Scene
	{
		private const int IMAGE_START_X = 0;
		private const int IMAGE_START_Y = 0;

		private const int TEXT_START_X = 24;
		private const int TEXT_START_Y = 550;
		private const int TEXT_WIDTH = 976;
		private const int TEXT_HEIGHT = 350;

		Script sceneScript;
		string sceneName;

		protected static SpriteFont storyFont;
		Texture2D currentImage;
		Color imageColor;

		private Lua lua;

		bool lerpingColor = false;
		Color startColor;
		Color endColor;
		float lerpTime;
		float lerpDuration;

		private bool unloaded = false;

		List<StoryMessage> messageQueue;
		private StoryMessage currentMessage 
		{ 
			get { return messageQueue [0]; } 
		}

		private static StoryScene _instance;
		public static StoryScene Instance 
		{
			get { return _instance; }
		}

		public StoryScene (string scriptName)
		{
			sceneName = scriptName;
			Initialize ();
		}

		public override void Initialize ()
		{
			_instance = this;

			lua = new Lua();
			lua.DoFile ("Data/storySceneScripts.lua");
			sceneScript = (Script)lua.GetFunction(typeof(Script), "scripts." + sceneName);
			messageQueue = new List<StoryMessage>();

			if (storyFont == null)
			{
				// Load assets...
				storyFont = ContentLoader.Instance.Load<SpriteFont>("Fonts/Dustismo18");

			}

			
			Scheduler.Execute(sceneScript);
		}

		public override void Update (GameTime gameTime)
		{
			// This is to catch an edge case
			if (unloaded)
				return;

			Scheduler.Update(gameTime.ElapsedGameTime.TotalSeconds);

			// If a message is shown...
			if (messageQueue.Count > 0)
			{
				// Update the current message
				currentMessage.Update (gameTime);

				// Check for input advancing the message
				if(KeyboardManager.ButtonPressUp (Microsoft.Xna.Framework.Input.Keys.Space))
				{
					if(currentMessage.WholeMessageShown)
					{
						Console.WriteLine ("Go to next message!");
						AdvanceMessageQueue();
					}
					else
					{
						Console.WriteLine ("Show entire message.");
						currentMessage.ShowAll ();
					}
				}

			}

			if(lerpingColor)
				UpdateColorLerp(gameTime);
		}

		public void UpdateColorLerp (GameTime gameTime)
		{
			lerpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
			imageColor = Color.Lerp (startColor, endColor, lerpTime / lerpDuration);

			if (lerpTime >= lerpDuration)
			{
				imageColor = endColor;

				// Disable the lerp
				lerpingColor = false;
			}
		}

		public void AdvanceMessageQueue ()
		{
			messageQueue.RemoveAt (0);

			if (messageQueue.Count > 0)
			{
				SendSignal ("Message queue advanced");
			}
			else
			{
				Console.WriteLine ("MESSAGES READ");
				SendSignal ("Messages read");
			}
		}

		public void SendSignal(string signal)
		{
			Scheduler.SendSignal(signal);
		}

		public void ShowMessage (string message, float textSpeed = .07f)
		{
			messageQueue.Add (new StoryMessage(message, textSpeed));
		}

		public void SetImage(Texture2D texture, Color? color = null)
		{
			currentImage = texture;

			if(color != null)
				imageColor = (Color)color;
		}

		public void LerpColor(Color end, float duration)
		{
			startColor = imageColor;
			endColor = end;
			lerpDuration = duration;
			lerpTime = 0;

			lerpingColor = true;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// Edge case
			if(unloaded)
				return;

			// Set the color to black.
			spriteBatch.GraphicsDevice.Clear (Color.Black);
			if(currentImage != null)
				spriteBatch.Draw (currentImage, position: new Vector2(IMAGE_START_X, IMAGE_START_Y), color: imageColor);
			if(messageQueue.Count > 0)
				spriteBatch.DrawString (storyFont, messageQueue[0].ShownMessage, new Vector2(TEXT_START_X, TEXT_START_Y), Color.White);
		}



		public override void Unload()
		{
			unloaded = true;
		}

		private class StoryMessage
		{
			private string messageBuffer;
			private float speed;
			private float elapsedTime;

			public string ShownMessage = "";
			public bool WholeMessageShown 
			{
				get { return (messageBuffer.Length <= 0); }
			}

			public StoryMessage(string message, float textSpeed = .07f)
			{
				// Parse the message so it will fit into the alloted space.
				messageBuffer = ParseMessage(message);
				speed = textSpeed;
			}

			public string ParseMessage (string message)
			{
				string[] tempStrings = message.Split (' ', '\n');
				string newMessage = "";
				float currentX = TEXT_START_X;
				
				foreach (string s in tempStrings)
				{
					float wordWidth = StoryScene.storyFont.MeasureString(s + " ").X;
					currentX += wordWidth;
					
					if(currentX < TEXT_WIDTH - (TEXT_START_X * 2))
					{
						newMessage = String.Concat(newMessage, s, " ");
					}
					else
					{
						newMessage = String.Concat(newMessage, "\n", s, " ");
						currentX = TEXT_START_X + wordWidth;
					}
				}
				
				return newMessage;
			}


			public void Update (GameTime gameTime)
			{
				if (messageBuffer.Length <= 0)
					return;

				elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (elapsedTime >= speed)
				{
					elapsedTime = 0;
					ShowNextLetter();
				}
			}

			public void ShowNextLetter()
			{
				char nextLetter = messageBuffer[0];
				messageBuffer = messageBuffer.Remove (0,1);
				ShownMessage = String.Concat (ShownMessage, nextLetter);
			}

			public void ShowAll ()
			{
				ShownMessage = String.Concat (ShownMessage, messageBuffer);
				messageBuffer = "";
			}
		}
	}
}

