using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Eglantine
{
	public abstract class Scene
	{
		public abstract void Initialize();
		public abstract void Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch spriteBatch);
		public abstract void Unload();
	}
}

