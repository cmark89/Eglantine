using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine
{
	public class DocumentScreen
	{
		// Singletonize me, cap'n!
		private static DocumentScreen _instance;
		public static DocumentScreen Instance
		{
			get
			{
				if(_instance == null)
					_instance = new DocumentScreen();

				return _instance;
			}
		}

		public DocumentScreen ()
		{

		}
	}
}

