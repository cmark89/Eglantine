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
			T loadedAsset = Content.Load<T> (path);
			return loadedAsset;
		}
	}
}

