using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Eglantine.Engine
{
	public abstract class Screen
	{
		public ScreenUpdateType UpdateType { get; protected set; }
		public bool ReceivingInput { get; set; }

		public abstract void Initialize();
		public abstract void Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch spriteBatch);

		//public bool MouseInScreen()
		//{
			//return (mouse is inside screen)
		//}
	}

	public enum ScreenUpdateType
	{
		OnTop,
		Always
	}
}