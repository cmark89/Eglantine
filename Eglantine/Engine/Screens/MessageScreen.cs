using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Eglantine.Engine
{
	public class MessageScreen : Screen
	{
		MessageScreenState _state = MessageScreenState.None;
		MessageScreenState State
		{
			get { return _state; }
			set 
			{ 
				_state = value; 
				MessageManager.Instance.MessageState = _state;
			}
		}

		string MessageText;

		// Constants for text display purposes.
		const int WINDOW_X_MARGIN = 14;
		const int WINDOW_Y_MARGIN = 14;

		const float FADE_IN_TIME = .6f;
		const float FADE_OUT_TIME = .3f;
		const float INITIAL_Y_OFFSET = 35f;

		Color textDisplayColor = Color.DarkRed;

		double transitionTime = 0D;
		Color drawColor;
		Color textColor;

		int WINDOW_HEIGHT
		{
			get { return MessageManager.Instance.MessageWindowTexture.Height; }
		}
		int WINDOW_WIDTH
		{
			get { return MessageManager.Instance.MessageWindowTexture.Width; }
		}

		Vector2 anchorPoint;

		public MessageScreen(string text)
		{
			MessageText = FitText(text);
			anchorPoint = new Vector2(0, Eglantine.GAME_HEIGHT - WINDOW_HEIGHT + INITIAL_Y_OFFSET);
			State = MessageScreenState.Appearing;
		}

		public override void Initialize()
		{
			// Make sure the text is going to fit properly.  
		}

		public override void Update (GameTime gameTime)
		{
			// If the message is shown and the screen is on top of the screen stack
			if (ReceivingInput)
			{
				// For now, only recognize left clicking.
				if (MouseManager.LeftClickUp && State == MessageScreenState.Shown)
				{
					AdvanceMessage ();
				}
			}

			if (State == MessageScreenState.Appearing)
			{
				transitionTime += gameTime.ElapsedGameTime.TotalSeconds;
				anchorPoint.Y -= ((float)gameTime.ElapsedGameTime.TotalSeconds / FADE_IN_TIME) * INITIAL_Y_OFFSET;
				drawColor = Color.Lerp(Color.Transparent, Color.White, (float)(transitionTime/FADE_IN_TIME));
				textColor = Color.Lerp(Color.Transparent, textDisplayColor, (float)(transitionTime/FADE_IN_TIME));

				if(transitionTime > FADE_IN_TIME)
				{
					transitionTime = 0;
					State = MessageScreenState.Shown;
					anchorPoint.Y = Eglantine.GAME_HEIGHT - WINDOW_HEIGHT;
				}
			}
			if (State == MessageScreenState.Disappearing)
			{
				transitionTime += gameTime.ElapsedGameTime.TotalSeconds;
				anchorPoint.Y += ((float)gameTime.ElapsedGameTime.TotalSeconds / FADE_OUT_TIME) * INITIAL_Y_OFFSET;
				drawColor = Color.Lerp(Color.White, Color.Transparent, (float)(transitionTime/FADE_OUT_TIME));
				textColor = Color.Lerp(textDisplayColor, Color.Transparent, (float)(transitionTime/FADE_IN_TIME));

				if(transitionTime > FADE_OUT_TIME)
				{
					transitionTime = 0;
					State = MessageScreenState.None;
					FlaggedForRemoval = true;
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Console.Clear();
			spriteBatch.Draw(MessageManager.Instance.MessageWindowTexture, anchorPoint, drawColor);
			spriteBatch.DrawString(MessageManager.Instance.MessageFont, MessageText, new Vector2(anchorPoint.X + WINDOW_X_MARGIN, anchorPoint.Y + WINDOW_Y_MARGIN), textColor);
		}

		public string FitText (string oldMessage)
		{
			string[] tempStrings = oldMessage.Split (' ', '\n');
			string newMessage = "";
			float currentX = WINDOW_X_MARGIN;

			foreach (string s in tempStrings)
			{
				float wordWidth = MessageManager.Instance.MessageFont.MeasureString(s + " ").X;
				currentX += wordWidth;

				if(currentX < WINDOW_WIDTH - (WINDOW_X_MARGIN * 2))
				{
					newMessage = String.Concat(newMessage, s, " ");
				}
				else
				{
					newMessage = String.Concat(newMessage, "\n", s, " ");
					currentX = WINDOW_X_MARGIN + wordWidth;
				}
			}

			return newMessage;
		}

		public void AdvanceMessage ()
		{
			// If there are more messages to display
			if (MessageManager.Instance.MessageQueue.Count > 0)
			{
				// Animate this later, but for now this is fine.
				MessageText = FitText(MessageManager.Instance.PopNextMessage());
			}
			// Otherwise, it's time to disappear.
			else
			{
				State = MessageScreenState.Disappearing;
			}
		}
	}
}