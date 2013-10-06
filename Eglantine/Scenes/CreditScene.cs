using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public class CreditScene : Scene
	{
		Texture2D credits;
		Vector2 pos;
		const float creditsTime = 48.5f;
		float scrollspeed;
		double time;

		public CreditScene ()
		{
			Initialize();
		}

		public override void Initialize()
		{
			credits = ContentLoader.Instance.LoadTexture2D("Graphics/Client/Credits");

			pos = new Vector2((Eglantine.GAME_WIDTH / 2f) - (credits.Width / 2), Eglantine.GAME_HEIGHT);
			scrollspeed = (credits.Height + Eglantine.GAME_HEIGHT) / creditsTime;
			EventManager.Instance.PlaySong("Toxic Night - Party All Night Mix", .5f, false, 0f, 0f);
		}

		public override void Update (GameTime gameTime)
		{
			time += gameTime.ElapsedGameTime.TotalSeconds;
			pos.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * scrollspeed;

			if (time > creditsTime)
			{
				AudioManager.Instance.StopMusic();
				Eglantine.ChangeScene(new MainMenuScene());
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(credits, pos, Color.White);
		}

		public override void Unload()
		{
		}
		

	}
}

