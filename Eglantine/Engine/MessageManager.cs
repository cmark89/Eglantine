using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public class MessageManager
	{
		private static MessageManager _instance;
		public static MessageManager Instance
		{
			get
			{
				if(_instance == null)
					_instance = new MessageManager();

				return _instance;
			}
		}

		// Store the current state of the message window
		public MessageScreenState MessageState = MessageScreenState.None;

		// List of messages to be shown
		public List<string> MessageQueue { get; private set; }

		// Content
		public Texture2D MessageWindowTexture { get; private set;}
		public SpriteFont MessageFont { get; private set; }


		public MessageManager ()
		{
			Initialize();
		}

		public void Initialize ()
		{
			MessageQueue = new List<string>();

			// Load the content
			MessageWindowTexture = ContentLoader.Instance.Load<Texture2D>("messagewindow");
			//MessageFont = ContentLoader.Instance.Load<SpriteFont>("Fonts/MessageFont");
			MessageFont = ContentLoader.Instance.Load<SpriteFont>("Fonts/MessageFont");
		}

		public void ShowMessage(string message)
		{
			MessageQueue.Add(message);

			if(MessageState == MessageScreenState.None)
			{
				if(GameScene.Instance != null)
					GameScene.Instance.AddScreen(new MessageScreen(PopNextMessage()));
			}
		}

		public string PopNextMessage ()
		{
			if (MessageQueue.Count > 0)
			{
				string s = MessageQueue[0];
				MessageQueue.Remove(s);
				return s;
			}
			else
				return null;
		}
	}

	public enum MessageScreenState
	{
		None,
		Appearing,
		Shown,
		Disappearing
	}
}

