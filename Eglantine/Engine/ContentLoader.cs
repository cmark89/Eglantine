using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Eglantine.Engine
{
	/// <summary>
	/// Singleton wrapper for ContentManager.
	/// </summary>
	public class ContentLoader
	{
		ContentManager Content;

		private static ContentLoader _instance;
		public static ContentLoader Instance
		{
			get
			{
				if(_instance == null)
					_instance = new ContentLoader();

				return _instance;
			}
		}

		public static void Initialize (ContentManager newContent)
		{
			if(_instance == null)
				_instance = new ContentLoader();

			_instance.Content = newContent;
		}

		public T Load<T> (string path)
		{
			return Content.Load<T> (path);
		}

		public Texture2D LoadTexture2D (string path)
		{
			Texture2D texture = Content.Load<Texture2D>(path);
			texture.Name = path;
			return texture;
		}
	}
}

